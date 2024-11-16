// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;
using System.Text.Json;
using ConsoleAppFramework;
using FFSourceGen;
using FFSourceGen.Models;

var app = ConsoleApp.Create();
app.Add<AppCommands>();
app.Run(args);

/// <summary>
/// App Commands.
/// </summary>
#pragma warning disable SA1649 // File name should match first type name
public partial class AppCommands
#pragma warning restore SA1649 // File name should match first type name
{
    private string baseNamespace { get; set; } = "FishyFlip.Lexicon";
    private string basePath { get; set; }

    public static List<PropertyGeneration> AllProperties { get; } = new();

    /// <summary>
    /// Generate lexicon source code.
    /// </summary>
    /// <param name="lexiconPath">Path to lexicon files.</param>
    /// <param name="outputDir">-o, Output directory.</param>
    /// <param name="baseNamespace">-n, Base Namespace.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    [Command("generate")]
    public async Task GenerateAsync([Argument] string lexiconPath, string? outputDir = default, string? baseNamespace = default, CancellationToken cancellationToken = default)
    {
        this.baseNamespace = baseNamespace ??= this.baseNamespace;
        outputDir ??= AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine($"Lexicon Path: {lexiconPath}");
        Console.WriteLine($"Output Directory: {outputDir}");
        var files = Directory.EnumerateFiles(lexiconPath, "*.json", SearchOption.AllDirectories).ToList();
        Console.WriteLine($"Found {files.Count()} files.");

        this.basePath = Path.Combine(outputDir, "Lexicon");
        if (Directory.Exists(this.basePath))
        {
            Directory.Delete(this.basePath, true);
        }

        Directory.CreateDirectory(this.basePath);
        var classList = new List<ClassGeneration>();
        foreach (var jsonFile in files)
        {
            var filename = Path.GetFileNameWithoutExtension(jsonFile);
            var jsonType = filename switch
            {
                "defs" => JsonType.Defs,
                _ => JsonType.Other,
            };

            classList.AddRange(await this.ProcessFile(jsonFile, jsonType));
        }

        AllProperties.AddRange(classList.SelectMany(n => n.Properties));

        foreach (var test in AllProperties)
        {
            test.UpdateType();
        }

        foreach (var cls in classList)
        {
            await this.GenerateModelFile(cls);
        }

        var enumList = classList.SelectMany(n => n.EnumProperties).ToList();

        await this.GenerateJsonSerializerContextFile(classList, enumList);
        await this.GenerateJsonConverterClassFile(classList);

        var atRecordSource = this.GenerateATObjectSource(this.baseNamespace);
        var atRecordPath = Path.Combine(this.basePath, "ATObject.g.cs");
        await File.WriteAllTextAsync(atRecordPath, atRecordSource);
    }

    private async Task<List<ClassGeneration>> ProcessFile(string defJsonPath, JsonType jsonType)
    {
        var defJsonText = await File.ReadAllTextAsync(defJsonPath);
        var schemaDocument = JsonSerializer.Deserialize<SchemaDocument>(defJsonText, SourceGenerationContext.Default.SchemaDocument);
        if (schemaDocument == null)
        {
            throw new Exception($"Failed to deserialize {defJsonPath}.");
        }

        var path = Path.Combine(schemaDocument.Id.Split('.').Take(schemaDocument.Id.Split('.').Length - 1).Select(n => n.ToPascalCase()).ToArray());
        var classList = new List<ClassGeneration>();
        foreach (var definition in schemaDocument.Defs)
        {
            var classGeneration = new ClassGeneration(schemaDocument, definition.Key, definition.Value, path);
            Console.WriteLine(classGeneration.ToString());
            foreach (var enumProp in classGeneration.EnumProperties)
            {
                Console.WriteLine(enumProp.ToString());
            }

            classList.Add(classGeneration);
        }

        return classList;
    }

    private void GenerateUsingsList(StringBuilder sb, ClassGeneration cls)
    {
        var properties = cls.Properties.Select(n => n.CSharpNamespace).Where(n => n != cls.CSharpNamespace).Distinct();
        foreach (var item in properties)
        {
            sb.AppendLine($"using {this.baseNamespace}.{item};");
        }

        if (properties.Any())
        {
            sb.AppendLine();
        }
    }

    private async Task GenerateJsonConverterClassFile(List<ClassGeneration> classes)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{this.baseNamespace}");
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// ATProtocol Message Json Converter.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public class ATProtocolJsonConverter : JsonConverter<ATObject?>");
        sb.AppendLine("    {");
        sb.AppendLine($"        public override ATObject? Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (reader.TokenType == JsonTokenType.StartObject)");
        sb.AppendLine("            {");
        sb.AppendLine("                using (JsonDocument doc = JsonDocument.ParseValue(ref reader))");
        sb.AppendLine("                {");
        sb.AppendLine("                    var rawText = doc.RootElement.GetRawText();");
        sb.AppendLine("                    if (doc.RootElement.TryGetProperty(\"$type\", out JsonElement typeElement))");
        sb.AppendLine("                    {");
        sb.AppendLine($"                       switch (typeElement.GetString())");
        sb.AppendLine("                        {");
        foreach (var cls in classes)
        {
            sb.AppendLine($"                            case \"{cls.Id}\":");
            sb.AppendLine($"                                return {cls.CSharpNamespace}.{cls.ClassName}.FromJson(rawText);");
            sb.AppendLine("                                break;");
        }
        sb.AppendLine("                        }");
        sb.AppendLine("                    }");
        sb.AppendLine("                }");
        sb.AppendLine("            }");
        sb.AppendLine("            return null;");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine($"        public override void Write(Utf8JsonWriter writer, ATObject? value, JsonSerializerOptions options)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (value is ATObject record)");
        sb.AppendLine("            {");
        sb.AppendLine("                JsonSerializer.Serialize(writer, record.ToJson());");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, "ATProtocolJsonConverter.g.cs");
        await File.WriteAllTextAsync(outputPath, sb.ToString());
    }

    private async Task GenerateJsonSerializerContextFile(List<ClassGeneration> classes, List<EnumProperties> enums)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, this.baseNamespace);
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// ATProtocol Message Source Generation Context.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    [JsonSourceGenerationOptions(");
        sb.AppendLine($"        WriteIndented = true,");
        sb.AppendLine($"        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,");
        sb.AppendLine($"        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]");
        foreach (var cls in classes)
        {
            var typeName = $"{this.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}";
            var typeInfoPropertyName = $"{cls.CSharpNamespace}.{cls.ClassName}".Replace(".", string.Empty);
            sb.AppendLine($"    [JsonSerializable(typeof({typeName}), TypeInfoPropertyName = \"{typeInfoPropertyName}\")]");
        }

        foreach (var enumProp in enums)
        {
            var typeName = $"{this.baseNamespace}.{enumProp.CSharpNamespace}.{enumProp.ClassName}";
            var typeInfoPropertyName = $"{enumProp.CSharpNamespace}.{enumProp.ClassName}".Replace(".", string.Empty);
            sb.AppendLine($"    [JsonSerializable(typeof({typeName}), TypeInfoPropertyName = \"{typeInfoPropertyName}\")]");
        }
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketCommit))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketCommitType))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketEvent))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketRecord))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATDid))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATError))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATHandle))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATIdentifier))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATUri))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.Blob))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.Facet))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.FacetFeature))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.FacetIndex))]");
        sb.AppendLine($"    [JsonSerializable(typeof(List<ATObject>))]");
        sb.AppendLine($"    internal partial class SourceGenerationContext : JsonSerializerContext");
        sb.AppendLine("    {");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, "SourceGenerationContext.g.cs");
        await File.WriteAllTextAsync(outputPath, sb.ToString());
    }

    private async Task GenerateModelFile(ClassGeneration cls)
    {
        Console.WriteLine($"Generating Class: {cls.Id}, {cls.CSharpNamespace}, {cls.ClassName}");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateUsingsList(sb, cls);
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{cls.CSharpNamespace}");
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, cls.Definition);

        sb.AppendLine($"    public partial class {cls.ClassName} : ATObject");
        sb.AppendLine("    {");

        this.GenerateEmptyClassConstructor(sb, cls.ClassName);
        this.GenerateCBorObjectClassConstructor(sb, cls);
        foreach (var property in cls.Properties)
        {
            this.GenerateProperty(sb, property, cls);
        }

        this.GenerateTypeProperty(sb, cls.Id);

        sb.AppendLine();
        this.GenerateToJsonWithSourceGenerator(sb, cls);

        sb.AppendLine();
        this.GenerateFromJsonWithSourceGenerator(sb, cls);

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, cls.Path);
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{cls.ClassName}.g.cs");
        await File.WriteAllTextAsync(classPath, sb.ToString());

        foreach (var enumProp in cls.EnumProperties)
        {
            await this.GenerateEnum(enumProp);
        }
    }

    private void GenerateToJsonWithSourceGenerator(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine($"        public override string ToJson()");
        sb.AppendLine("        {");
        sb.AppendLine($"            return JsonSerializer.Serialize<{cls.CSharpNamespace}.{cls.ClassName}>(this, (JsonTypeInfo<{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{$"{cls.CSharpNamespace}".Replace(".", string.Empty)}{cls.ClassName})!;");
        sb.AppendLine("        }");
    }

    private void GenerateFromJsonWithSourceGenerator(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine($"        public static {cls.ClassName} FromJson(string json)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return JsonSerializer.Deserialize<{cls.CSharpNamespace}.{cls.ClassName}>(json, (JsonTypeInfo<{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{$"{cls.CSharpNamespace}".Replace(".", string.Empty)}{cls.ClassName})!;");
        sb.AppendLine("        }");
    }

    private void GenerateCBorObjectClassConstructor(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine();
        sb.AppendLine("        /// <summary>");
        sb.AppendLine($"        /// Initializes a new instance of the <see cref=\"{cls.ClassName}\"/> class.");
        sb.AppendLine("        /// </summary>");
        sb.AppendLine($"        public {cls.ClassName}(CBORObject obj)");
        sb.AppendLine("        {");
        foreach (var property in cls.Properties)
        {
            sb.AppendLine($"            {property.CBorProperty}");
        }
        sb.AppendLine("        }");
        sb.AppendLine();
    }

    private void GenerateEmptyClassConstructor(StringBuilder sb, string className)
    {
        sb.AppendLine();
        sb.AppendLine("        /// <summary>");
        sb.AppendLine($"        /// Initializes a new instance of the <see cref=\"{className}\"/> class.");
        sb.AppendLine("        /// </summary>");
        sb.AppendLine($"        public {className}()");
        sb.AppendLine("        {");
        sb.AppendLine("        }");
        sb.AppendLine();
    }

    private (string jsonSerName, string cs) GenerateJsonSerNameAndCs(string id, string[] idSplit, string clsNamespace)
    {
        var jsonSerName = string.Empty;
        var cs = string.Empty;

        if (string.IsNullOrEmpty(idSplit[0]))
        {
            jsonSerName = idSplit.Last().ToPascalCase();
            cs = $"{clsNamespace}.{jsonSerName}";
        }
        else if (idSplit.Length == 1)
        {
            var splitTwo = idSplit[0].Split('.').Select(n => n.ToPascalCase()).ToArray();
            if (splitTwo.Length == 1)
            {
                jsonSerName = splitTwo.First();
                cs = $"{clsNamespace}.{jsonSerName}";
            }
            else
            {
                jsonSerName = splitTwo.Last();
                cs = string.Join(".", id.Replace("defs", jsonSerName).Split('#').First().Split('.').Select(n => n.ToPascalCase()).ToArray());
            }
        }
        else
        {
            var t = idSplit.First().Split('.').Select(n => n.ToPascalCase()).ToArray();
            jsonSerName = idSplit.Last().ToPascalCase();
            cs = string.Join(".", id.Replace("defs", jsonSerName).Split('#').First().Split('.').Select(n => n.ToPascalCase()).ToArray());
        }

        return (jsonSerName, cs);
    }

    private async Task GenerateEnum(EnumProperties enumProperties)
    {
        Console.WriteLine($"Generating Enum: {enumProperties.Document.Id}, {enumProperties.ClassName}");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{enumProperties.CSharpNamespace}");
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, enumProperties.PropertyDefinition);
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Known value for {enumProperties.ClassName}.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public enum {enumProperties.ClassName}");
        sb.AppendLine("    {");
        foreach (var value in enumProperties.Values)
        {
            sb.AppendLine($"       {value},");
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, enumProperties.Path);
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{enumProperties.ClassName}.g.cs");
        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private void GenerateProperty(StringBuilder sb, PropertyGeneration property, ClassGeneration cls)
    {
        Console.WriteLine($"Generating Property for {property.Document.Id}: {property.Key}, {property.Type}");
        // Add property documentation if available
        if (!string.IsNullOrEmpty(property.PropertyDefinition.Description))
        {
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {property.PropertyDefinition.Description}");
            sb.AppendLine($"        /// </summary>");
        }

        // Add JSON property name attribute
        sb.AppendLine($"        [JsonPropertyName(\"{property.Key}\")]");

        // Add Required attribute if property is required
        if (property.Definition.Required?.Contains(property.Key) == true)
        {
           // sb.AppendLine("        [JsonRequired]");
        }

        if (property.IsATObject)
        {
            if (property.PropertyDefinition.Type == "array")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.ATProtocolListJsonConverter))]");
            }
            else
            {
                sb.AppendLine($"        [JsonConverter(typeof(ATProtocolJsonConverter))]");
            }
        }
        else if (property.IsEnum)
        {
            sb.AppendLine($"        [JsonConverter(typeof(JsonStringEnumConverter<{property.ClassName}>))]");
        }

        // Add Converter for various AT Object types (Ex. ATDid, ATUri, etc.)
        if (property.Type == "FishyFlip.Models.ATUri?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]");
        }

        if (property.Type == "Ipfs.Cid?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATCidJsonConverter))]");
        }

        if (property.Type == "FishyFlip.Models.ATHandle?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]");
        }

        if (property.Type == "FishyFlip.Models.ATDid?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]");
        }

        // Add validation attributes
        // GenerateValidationAttributes(sb, property);

        var propertyName = property.PropertyName;
        
        // Handle default values
        if (property.PropertyDefinition.Default != null)
        {
            sb.AppendLine($"        public {property.Type} {propertyName} {{ get; set; }} = {property.GetDefaultValue()};");
        }
        else
        {
            sb.AppendLine($"        public {property.Type} {propertyName} {{ get; set; }}");
        }

        sb.AppendLine();
    }

    private string GenerateATObjectSource(string ns)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, ns);
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// The base class for FishyFlip ATProtocol Objects.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public abstract class ATObject");
        sb.AppendLine("    {");
        sb.AppendLine();
        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Gets the ATRecord Type.");
        sb.AppendLine($"        /// </summary>");
        sb.AppendLine($"        [JsonPropertyName(\"$type\")]");
        sb.AppendLine($"        public virtual string Type => throw new NotImplementedException();");
        sb.AppendLine();
        sb.AppendLine($"        public virtual string ToJson()");
        sb.AppendLine("        {");
        sb.AppendLine($"            throw new NotImplementedException();");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        return sb.ToString();
    }

    private void GenerateTypeProperty(StringBuilder sb, string id)
    {
        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Gets the ATRecord Type.");
        sb.AppendLine($"        /// </summary>");
        sb.AppendLine($"        [JsonPropertyName(\"$type\")]");
        sb.AppendLine($"        public override string Type => \"{id}\";");
        sb.AppendLine();
        sb.AppendLine($"        public const string RecordType = \"{id}\";");
    }

    private void GenerateHeader(StringBuilder sb)
    {
        sb.AppendLine("// <auto-generated />");
        sb.AppendLine("// This file was generated by FFSourceGen.");
        sb.AppendLine("// Do not modify this file.");
        sb.AppendLine();

        // Add #nullable
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
    }

    private void GenerateNamespace(StringBuilder sb, string ns)
    {
        sb.AppendLine($"namespace {ns}");
    }

    private void GenerateClassDocumentation(StringBuilder sb, SchemaDefinition definition)
    {
        // Add class documentation if available
        if (!string.IsNullOrEmpty(definition.Description))
        {
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {definition.Description}");
            sb.AppendLine($"    /// </summary>");
        }
    }

    private void GenerateClassDocumentation(StringBuilder sb, PropertyDefinition definition)
    {
        // Add class documentation if available
        if (!string.IsNullOrEmpty(definition.Description))
        {
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {definition.Description}");
            sb.AppendLine($"    /// </summary>");
        }
    }

    private enum JsonType
    {
        Defs,
        Other
    }
}