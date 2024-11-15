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
        var files = Directory.EnumerateFiles(lexiconPath, "*.json", SearchOption.AllDirectories);
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

        foreach (var cls in classList)
        {
            await this.GenerateModelFile(cls);
        }

        var enumList = classList.SelectMany(n => n.EnumProperties).ToList();

        await this.GenerateJsonSerializerContextFile(classList, enumList);

        var atRecordSource = this.GenerateATRecordSource(this.baseNamespace);
        var atRecordPath = Path.Combine(this.basePath, "ATRecord.g.cs");
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

        sb.AppendLine($"    public partial class {cls.ClassName}");
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

        var requiresJsonConverters = cls.Properties.Where(n => n.RequiresConverter);
        foreach (var converter in requiresJsonConverters)
        {
            if (converter.PropertyDefinition.Refs is null)
            {
                continue;
            }

            sb.AppendLine();
            var ids = converter.PropertyDefinition.Refs.ToArray();
            sb.AppendLine($"        public class {converter.ClassName}JsonConverter : JsonConverter<{converter.Type}>");
            sb.AppendLine("        {");
            sb.AppendLine($"            public override {converter.Type} Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)");
            sb.AppendLine("            {");
            sb.AppendLine($"                if (reader.TokenType == JsonTokenType.StartObject)");
            sb.AppendLine("                {");
            sb.AppendLine("                    using (JsonDocument doc = JsonDocument.ParseValue(ref reader))");
            sb.AppendLine("                    {");
            sb.AppendLine($"                        if (doc.RootElement.TryGetProperty(\"$type\", out JsonElement typeElement))");
            sb.AppendLine("                        {");
            sb.AppendLine("                            var type = typeElement.GetString();");
            foreach (var id in ids)
            {
                (var jsonSerName, var cs) = this.GenerateJsonSerNameAndCs(id, id.Replace("#view", string.Empty).Replace(".defs", string.Empty).Split('#'), cls.CSharpNamespace);

                if (string.IsNullOrEmpty(jsonSerName))
                {
                    throw new Exception("Failed to get jsonSerName.");
                }

                if (string.IsNullOrEmpty(cs))
                {
                    throw new Exception("Failed to get cs.");
                }

                sb.AppendLine($"                            if (type == \"{id}\")");
                sb.AppendLine("                            {");
                sb.AppendLine($"                                return JsonSerializer.Deserialize<{cs}>(doc.RootElement.GetRawText(), (JsonTypeInfo<{cs}>)((SourceGenerationContext)options.TypeInfoResolver!).{$"{cs}".Replace(".", string.Empty)});");
                sb.AppendLine("                            }");
            }
            sb.AppendLine("                        }");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine($"                return null;");
            sb.AppendLine("            }");
            sb.AppendLine();
            sb.AppendLine($"            public override void Write(Utf8JsonWriter writer, {converter.Type} value, JsonSerializerOptions options)");
            sb.AppendLine("            {");
            foreach (var id in ids)
            {
                (var jsonSerName, var cs) = this.GenerateJsonSerNameAndCs(id, id.Replace("#view", string.Empty).Replace(".defs", string.Empty).Split('#'), cls.CSharpNamespace);

                if (string.IsNullOrEmpty(jsonSerName))
                {
                    throw new Exception("Failed to get jsonSerName.");
                }

                if (string.IsNullOrEmpty(cs))
                {
                    throw new Exception("Failed to get cs.");
                }

                sb.AppendLine($"                if (value is {cs} {jsonSerName})");
                sb.AppendLine("                {");
                sb.AppendLine($"                    JsonSerializer.Serialize(writer, {jsonSerName}, (JsonTypeInfo<{cs}>)((SourceGenerationContext)options.TypeInfoResolver!).{$"{cs}".Replace(".", string.Empty)});");
                sb.AppendLine("                    return;");
                sb.AppendLine("                }");
            }

            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

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
        sb.AppendLine($"        public string ToJson()");
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
            sb.AppendLine("        [JsonRequired]");
        }

        if (property.RequiresConverter)
        {
            sb.AppendLine($"        [JsonConverter(typeof({property.ClassName}JsonConverter))]");
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

    private string GenerateATRecordSource(string ns)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, ns);
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// The ATRecord class.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public abstract class ATRecord");
        sb.AppendLine("    {");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        return sb.ToString();
    }

    private void GenerateTypeProperty(StringBuilder sb, string id)
    {
        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Gets or sets the ATRecord Type.");
        sb.AppendLine($"        /// </summary>");
        sb.AppendLine($"        [JsonPropertyName(\"$type\")]");
        sb.AppendLine($"        public string Type {{ get; set; }} = \"{id}\";");
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