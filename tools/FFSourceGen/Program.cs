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

    public static List<ClassGeneration> AllClasses { get; } = new();

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

        this.basePath = Path.Combine(outputDir, "Lexicon");
        if (Directory.Exists(this.basePath))
        {
            Directory.Delete(this.basePath, true);
        }

        Directory.CreateDirectory(this.basePath);

        await this.GenerateModelFiles(lexiconPath);
    }

    private async Task GenerateModelFiles(string lexiconPath)
    {
        var files = Directory.EnumerateFiles(lexiconPath, "*.json", SearchOption.AllDirectories).ToList();
        Console.WriteLine($"Found {files.Count()} files.");

        var defs = files.Where(n => n.Contains("defs.json")).ToList();
        var mainRecordFiles = this.GetMainRecordJsonFiles(files.Except(defs).ToList());
        var other = files.Except(defs).Except(mainRecordFiles).ToList();

        foreach (var jsonFile in defs)
        {
            var filename = Path.GetFileNameWithoutExtension(jsonFile);
            await this.ProcessFile(jsonFile);
        }

        foreach (var jsonFile in mainRecordFiles)
        {
            var filename = Path.GetFileNameWithoutExtension(jsonFile);
            await this.ProcessFile(jsonFile);
        }

        foreach (var jsonFile in other)
        {
            var filename = Path.GetFileNameWithoutExtension(jsonFile);
            await this.ProcessFile(jsonFile);
        }

        foreach (var cls in AllClasses)
        {
            switch (cls.Definition.Type)
            {
                case "object":
                case "record":
                    await this.GenerateModelFile(cls);
                    break;
                default:
                    Console.WriteLine($"Skipping {cls.Definition.Type} for {cls.Id}");
                    break;
            }
        }

        var modelClasses = AllClasses.Where(n => n.Definition.Type == "object" || n.Definition.Type == "record").ToList();
        var procedureClasses = AllClasses.Where(n => n.Definition.Type == "procedure" || n.Definition.Type == "query").GroupBy(n => n.CSharpNamespace).ToList();
        foreach (var group in procedureClasses)
        {
            await this.GenerateEndpointGroupAsync(group);
        }

        await this.GenerateJsonSerializerContextFile(modelClasses);
        await this.GenerateCBORToATObjectConverterClassFile(modelClasses);

        var atRecordSource = this.GenerateATObjectSource(this.baseNamespace, modelClasses);
        var atRecordPath = Path.Combine(this.basePath, "ATObject.g.cs");
        await File.WriteAllTextAsync(atRecordPath, atRecordSource);
    }

    private async Task GenerateEndpointGroupAsync(IGrouping<string, ClassGeneration> group)
    {
            Console.WriteLine($"Generating Endpoint Group: {group.Key}");
            var sb = new StringBuilder();
            this.GenerateHeader(sb);
            this.GenerateNamespace(sb, group.Key);
            var className = $"{group.Key.Split(".").Last()}Endpoints";
            sb.AppendLine("{");
            sb.AppendLine();
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {group.Key.ToLower()} Endpoint Group.");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public class {className}");
            sb.AppendLine("    {");
            foreach (var item in group)
            {
                sb.AppendLine();
                Console.WriteLine($"{item.Definition.Type} {item.Id}");
                var methodName = $"{item.ClassName}Async";
                var returnType = "Task";
                var inputProperties = this.FetchInputProperties(item);
                var outputProperty = this.FetchOutputProperties(item);
                sb.AppendLine($"        /// <summary>");
                sb.AppendLine($"        /// {item.Definition.Description}");
                sb.AppendLine($"        /// </summary>");
                sb.AppendLine($"        public {returnType} {methodName}(");
                sb.AppendLine($"            )");
                sb.AppendLine("        {");
                sb.AppendLine("            throw new NotImplementedException();");
                sb.AppendLine("        }");
                sb.AppendLine();
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine();
            var outputPath = Path.Combine(this.basePath, group.Key.Replace('.', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(outputPath);
            var classPath = Path.Combine(outputPath, $"{className}.g.cs");
            if (File.Exists(classPath))
            {
                throw new Exception($"File already exists: {className} {classPath}");
            }

            await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private List<string> FetchInputProperties(ClassGeneration classGeneration)
    {
        var inputProperties = new List<string>();
        if (classGeneration.Definition.Input?.Schema?.Properties is null)
        {
            return inputProperties;
        }

        foreach (var prop in classGeneration.Definition.Input.Schema.Properties)
        {
            Console.WriteLine($"Property: {prop.Key}");
            if (prop.Value.Ref is not null)
            {
                Console.WriteLine($"Property Ref: {prop.Value.Ref}");
                var classRef = FindClassFromRef(prop.Value.Ref);
                if (classRef is not null)
                {
                    Console.WriteLine($"Class Ref: {classRef.Id} ");
                }
            }
        }

        return inputProperties;
    }

    private string FetchOutputProperties(ClassGeneration classGeneration)
    {
        if (classGeneration.Definition.Output?.Schema?.Properties is null)
        {
            return string.Empty;
        }

        foreach (var prop in classGeneration.Definition.Output.Schema.Properties)
        {
            Console.WriteLine($"Property: {prop.Key}");
            if (prop.Value.Ref is not null)
            {
                Console.WriteLine($"Property Ref: {prop.Value.Ref}");
                var classRef = FindClassFromRef(prop.Value.Ref);
                if (classRef is not null)
                {
                    Console.WriteLine($"Class Ref: {classRef.Id}");
                }
            }
        }

        return string.Empty;
    }

    private string GenerateReturnTypeFromClassGeneration(ClassGeneration classGeneration)
    {
        switch (classGeneration.Definition.Type)
        {
            default:
                return "object";
        }
    }

    private List<string> GetMainRecordJsonFiles(List<string> jsonFiles)
    {
        var mainRecordFiles = new List<string>();
        foreach (var file in jsonFiles)
        {
            var jsonText = File.ReadAllText(file);
            var schemaDocument = JsonSerializer.Deserialize<SchemaDocument>(jsonText, SourceGenerationContext.Default.SchemaDocument);
            if (schemaDocument == null)
            {
                throw new Exception($"Failed to deserialize {file}.");
            }

            var mainRecord = schemaDocument.Defs.FirstOrDefault(n => n.Key == "main");
            if (mainRecord.Value != null)
            {
                if (mainRecord.Value.Type == "record")
                {
                    mainRecordFiles.Add(file);
                }
            }
        }

        return mainRecordFiles;
    }

    public static bool DoesRefStringHaveNoNamespace(string refString)
    {
        return refString.Contains("#") && !refString.Contains(".");
    }

    public static ClassGeneration? FindClassFromRef(string refString)
    {
        return AllClasses.FirstOrDefault(n => n.Id == refString);
    }

    public static PropertyGeneration? FindPropertyFromRef(string refString)
    {
        var splitHashtag = refString.Split('#').Where(n => !string.IsNullOrEmpty(n)).ToArray();
        if (splitHashtag.Length == 2)
        {
            var def = splitHashtag[0];
            var key = splitHashtag[1];
            var cls = AllClasses.FirstOrDefault(n => n.Id == def);
            if (cls != null)
            {
                var prop = cls.Properties.FirstOrDefault(n => n.Key == key);
                if (prop != null)
                {
                    return prop;
                }
            }
        }

        return null;
    }

    private async Task ProcessFile(string defJsonPath)
    {
        var defJsonText = await File.ReadAllTextAsync(defJsonPath);
        var schemaDocument = JsonSerializer.Deserialize<SchemaDocument>(defJsonText, SourceGenerationContext.Default.SchemaDocument);
        if (schemaDocument == null)
        {
            throw new Exception($"Failed to deserialize {defJsonPath}.");
        }

        var path = Path.Combine(schemaDocument.Id.Split('.').Take(schemaDocument.Id.Split('.').Length - 1).Select(n => n.ToPascalCase()).ToArray());
        foreach (var definition in schemaDocument.Defs)
        {
            var classGeneration = new ClassGeneration(schemaDocument, definition.Key, definition.Value, path);
            Console.WriteLine(classGeneration.ToString());
            AllClasses.Add(classGeneration);
        }
    }

    private async Task GenerateJsonSerializerContextFile(List<ClassGeneration> classes)
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
        sb.AppendLine($"        PropertyNameCaseInsensitive = true,");
        sb.AppendLine("        Converters = new [] {");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATUriJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATCidJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATHandleJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATDidJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATWebSocketCommitTypeConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATWebSocketEventConverter),");
        // foreach (var enumProp in enums)
        // {
        //     sb.AppendLine($"            typeof(JsonStringEnumConverter<{enumProp.CSharpNamespace}.{enumProp.ClassName}>),");
        // }
        sb.AppendLine("        },");
        sb.AppendLine($"        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,");
        sb.AppendLine($"        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]");
        foreach (var cls in classes)
        {
            var typeName = $"{this.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}";
            var typeInfoPropertyName = $"{cls.CSharpNamespace}.{cls.ClassName}".Replace(".", string.Empty);
            sb.AppendLine($"    [JsonSerializable(typeof({typeName}), TypeInfoPropertyName = \"{typeInfoPropertyName}\")]");
            sb.AppendLine($"    [JsonSerializable(typeof(List<{typeName}>), TypeInfoPropertyName = \"List{typeInfoPropertyName}\")]");
        }

        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketCommit))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketCommitType))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketEvent))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketRecord))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATDid))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATError))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATHandle))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATIdentifier))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATLinkRef))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATUri))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.Blob))]");
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
        Console.WriteLine($"Generating Class: {cls.Id}, {cls.CSharpNamespace}, {cls.ClassName}, {cls.Definition.Type}");

        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        // this.GenerateUsingsList(sb, cls);
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{cls.CSharpNamespace}");
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, cls.Definition);

        sb.AppendLine($"    public partial class {cls.ClassName} : ATObject");
        sb.AppendLine("    {");

        this.GenerateEmptyClassConstructor(sb, cls.ClassName);
        this.GenerateCBorObjectClassConstructor(sb, cls);
        foreach (var property in cls.Properties)
        {
            await this.GenerateProperty(sb, property, cls);
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
        if (File.Exists(classPath))
        {
            Console.WriteLine($"File already exists: {cls.Id} {classPath}");
            return;
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
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

    private async Task GenerateProperty(StringBuilder sb, PropertyGeneration property, ClassGeneration cls)
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

        if (property.PropertyDefinition.Type == "array" && !property.IsBaseType)
        {
            if (property.RawType == "FishyFlip.Models.ATUri?")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATUriJsonConverter>))]");
            }
            else if (property.RawType == "Ipfs.Cid?")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATCidJsonConverter>))]");
            }
            else if (property.RawType == "FishyFlip.Models.ATHandle?")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATHandleJsonConverter>))]");
            }
            else if (property.RawType == "FishyFlip.Models.ATDid?")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATDidJsonConverter>))]");
            }
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

        // Handle default values
        if (property.PropertyDefinition.Default != null)
        {
            sb.AppendLine($"        public {property.Type} {property.PropertyName} {{ get; set; }} = {property.GetDefaultValue()};");
        }
        else
        {
            sb.AppendLine($"        public {property.Type} {property.PropertyName} {{ get; set; }}");
        }

        sb.AppendLine();
    }

    private string GenerateTypeClassValue(PropertyGeneration property, ClassGeneration cls)
    {
        if (property.IsBaseType)
        {
            return property.Type;
        }

        if (property.RawType.Contains("ATObject") || property.RawType.Contains("FishyFlip.Models"))
        {
            return property.Type;
        }

        if (property.PropertyDefinition.Type == "ref" && property.PropertyDefinition.Ref != null)
        {
            var refString = property.PropertyDefinition.Ref;
            if (DoesRefStringHaveNoNamespace(refString))
            {
                refString = $"{property.Id.Split("#").First()}{refString}";
            }

            var classRef = FindClassFromRef(refString);
            if (classRef != null)
            {
                return $"{classRef.CSharpNamespace}.{classRef.ClassName}?";
            }

            var propRef = FindPropertyFromRef(property.PropertyDefinition.Ref);

            if (propRef != null)
            {
                return $"{propRef.CSharpNamespace}.{propRef.ClassName}?";
            }
        }

        var freshString = string.Empty;
        var type = property.RawType;

        if (!type.Contains("."))
        {
            if (!type.Contains(this.baseNamespace))
            {
                freshString = $"{this.baseNamespace}.";
            }

            if (!type.Contains(property.CSharpNamespace))
            {
                freshString += $"{property.CSharpNamespace}.";
            }

            freshString += $"{type.ToPascalCase()}";
        }
        else
        {
            if (!type.Contains(this.baseNamespace) && !type.Contains("FishyFlip.Models"))
            {
                freshString = $"{this.baseNamespace}.{type.ToPascalCase()}";
            }
            else
            {
                freshString = type;
            }
        }

        if (property.Type.Contains("List<"))
        {
            freshString = $"List<{freshString}>";
        }

        if (string.IsNullOrEmpty(freshString))
        {
            throw new Exception($"Failed to generate type for {property.Key}");
        }

        return $"{freshString}?";
    }

    private async Task GenerateCBORToATObjectConverterClassFile(List<ClassGeneration> classes)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, "FishyFlip.Lexicon");
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Converts CBOR to ATObjects.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public static class CborLexiconExtensions");
        sb.AppendLine("    {");
        sb.AppendLine("        public static ATObject? ToATObject(this CBORObject obj)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (obj == null)");
        sb.AppendLine("            {");
        sb.AppendLine("                return null;");
        sb.AppendLine("            }");
        sb.AppendLine();
        sb.AppendLine("            var type = obj[\"$type\"].AsString();");
        sb.AppendLine("            switch (type)");
        sb.AppendLine("            {");
        foreach (var cls in classes)
        {
            sb.AppendLine($"                case \"{cls.Id}\":");
            sb.AppendLine($"                    return new {cls.CSharpNamespace}.{cls.ClassName}(obj);");
        }

        sb.AppendLine("                default:");
        sb.AppendLine("                    return null;");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        public static List<ATObject?>? ToATObjectList(this CBORObject obj)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (obj == null)");
        sb.AppendLine("            {");
        sb.AppendLine("                return null;");
        sb.AppendLine("            }");
        sb.AppendLine();
        sb.AppendLine("            var list = new List<ATObject?>();");
        sb.AppendLine("            foreach (var item in obj.Values)");
        sb.AppendLine("            {");
        sb.AppendLine("                list.Add(item.ToATObject());");
        sb.AppendLine("            }");
        sb.AppendLine("            return list;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, "CborExtensions.g.cs");
        await File.WriteAllTextAsync(outputPath, sb.ToString());
    }

    private string GenerateATObjectSource(string ns, List<ClassGeneration> classes)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, ns);
        sb.AppendLine("{");
        foreach (var cls in classes)
        {
            sb.AppendLine($"    [JsonDerivedType(typeof({cls.CSharpNamespace}.{cls.ClassName}), typeDiscriminator: \"{cls.Id}\")]");
        }

        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// The base class for FishyFlip ATProtocol Objects.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public class ATObject");
        sb.AppendLine("    {");
        sb.AppendLine();
        sb.AppendLine("        public ATObject() { }");
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
}