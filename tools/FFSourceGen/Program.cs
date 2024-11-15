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
public class AppCommands
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

    private class EnumProperties
    {
        public EnumProperties(PropertyDefinition propertyDefinition, string key, SchemaDocument document, SchemaDefinition def, string path, string ns, string cns)
        {
            this.PropertyDefinition = propertyDefinition;
            this.Document = document;
            this.Definition = def;
            this.Path = path;
            this.Key = key;
            this.Namespace = ns;
            this.CSharpNamespace = cns;
            this.ClassName = key != "main" ? key.ToPascalCase() : string.Join(string.Empty, document.Id.Split('.').TakeLast(1).Select(n => n.ToPascalCase())).ToPascalCase();
            this.RawValues = this.PropertyDefinition.KnownValues ?? Array.Empty<string>();
            this.Values = this.PropertyDefinition.KnownValues?.Select(v => v.Split('#').Last().ToClassSafe().ToPascalCase()).ToArray() ?? Array.Empty<string>();
        }

        public string ClassName { get; }

        public PropertyDefinition PropertyDefinition { get; }

        public SchemaDocument Document { get; }

        public SchemaDefinition Definition { get; }

        public string Key { get; }

        public string Path { get; }

        public string[] RawValues { get; }

        public string[] Values { get; }

        public string Namespace { get; }

        public string CSharpNamespace { get; }

        public bool IsDefinitionFile => this.Document.Id.EndsWith("defs");

        public override string ToString()
        {
            return $"Enum: {this.Document.Id}, {this.ClassName} - {string.Join(", ", this.RawValues)}";
        }
    }

    private class PropertyGeneration
    {
        public PropertyGeneration(PropertyDefinition propertyDefinition, string key, SchemaDocument document, SchemaDefinition def, string path, string ns, string cns)
        {
            this.PropertyDefinition = propertyDefinition;
            this.Document = document;
            this.Definition = def;
            this.Path = path;
            this.Key = key;
            this.Namespace = ns;
            this.CSharpNamespace = cns;
            this.ClassName = key != "main" ? key.ToPascalCase() : string.Join(string.Empty, document.Id.Split('.').TakeLast(1).Select(n => n.ToPascalCase())).ToPascalCase();
            this.Type = this.GetPropertyType(this.ClassName, this.ClassName, propertyDefinition);
        }

        public string ClassName { get; }

        public PropertyDefinition PropertyDefinition { get; }

        public SchemaDocument Document { get; }

        public SchemaDefinition Definition { get; }

        public string Key { get; }

        public string Path { get; }

        public string Namespace { get; }

        public string CSharpNamespace { get; }

        public bool IsDefinitionFile => this.Document.Id.EndsWith("defs");

        public string Type { get; }

        public string GetDefaultValue()
        {
            var defaultValue = this.GetDefaultValueString(this.PropertyDefinition);
            if (this.PropertyDefinition.KnownValues?.Length > 0)
            {
                defaultValue = $"{this.GetClassNameAndNamespace()}.{defaultValue.ToPascalCase()}";
            }
            return defaultValue;
        }

        private string GetDefaultValueString(PropertyDefinition property)
        {
            return property.Type?.ToLower() switch
            {
                "string" => property.Default?.ToString() ?? "\"\"",
                "integer" => property.Default?.ToString() ?? "0",
                "boolean" => property.Default?.ToString()?.ToLower() ?? "false",
                "array" => "new()",
                _ => "default",
            };
        }

        public string GetClassNameAndNamespace()
        {
            // If Ref contains a #, with something before it, then it's a reference to another class.
            if (this.PropertyDefinition.Ref != null && this.PropertyDefinition.Ref.Contains("#"))
            {
                return $"{this.CSharpNamespace}.{this.GetClassNameFromRef(this.PropertyDefinition.Ref)}";
            }

            if (this.IsEnum)
            {
                return $"{this.CSharpNamespace}.{this.ClassName}";
            }

            return this.Type;
        }

        private bool IsEnum => this.PropertyDefinition.KnownValues?.Length > 0;

        private string GetPropertyType(string className, string name, PropertyDefinition property)
        {
            // Handle known values as enums
            if (property.KnownValues?.Length > 0)
            {
                return $"{name}".ToPascalCase();
            }

            var baseType = property.Type?.ToLower() switch
            {
                "string" when property.Format == "did" => "FishyFlip.Models.ATDid",
                "string" when property.Format == "handle" => "FishyFlip.Models.ATHandle",
                "string" when property.Format == "datetime" => "DateTime",
                "string" when property.Format == "at-uri" => "FishyFlip.Models.ATUri",
                "string" when property.Format == "at-identifier" => "FishyFlip.Models.ATIdentifier",
                "string" => "string",
                "blob" => "Blob",
                "integer" => "int",
                "boolean" => "bool",
                "bytes" => "byte[]",
                "cid-link" => "Ipfs.Cid",
                "array" when property.Items != null => this.GetListPropertyName(className, name, property),
                // "object" when property.Properties != null => "Dictionary<string, object>", // Could be expanded to generate nested types
                "unknown" => "object",
                "union" when property.Refs?.Length > 0 => "object", // Could be expanded to generate union types
                "ref" when !string.IsNullOrEmpty(property.Ref) && !property.Ref.Equals("com.atproto.repo.strongRef") => this.GetClassNameFromRef(property.Ref),
                "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "RepoStrongRef",
                _ => throw new InvalidOperationException($"Unknown property type: {property.Type}"),
            };

            return $"{baseType}?";
        }

        private string GetListPropertyName(string className, string name, PropertyDefinition property)
        {
            if (property.Items is null)
            {
                throw new InvalidOperationException("Property does not have items.");
            }

            var item = $"List<{this.GetPropertyType(className, name, property.Items)}>";
            Console.WriteLine($"List Property: {item}");
            return item;
        }

        private string GetClassNameFromRef(string refString)
        {
            Console.WriteLine($"{refString} Has #, {refString.Contains("#")}");
            return refString.Split('#').Last().ToPascalCase();
        }
    }

    private class ClassGeneration
    {
        public ClassGeneration(SchemaDocument document, string key, SchemaDefinition def, string path)
        {
            this.Path = path;
            this.Document = document;
            this.Definition = def;
            this.Key = key;
            this.ClassName = key != "main" ? key.ToPascalCase() : string.Join(string.Empty, document.Id.Split('.').TakeLast(1).Select(n => n.ToPascalCase())).ToPascalCase();
            this.Namespace = string.Join(".", document.Id.Split('.').Take(document.Id.Split('.').Length - 1).ToArray());
            this.CSharpNamespace = string.Join(".", document.Id.Split('.').Take(document.Id.Split('.').Length - 1).Select(n => n.ToPascalCase()).ToArray());
            this.ProcessProperties();
        }

        public string ClassName { get; }

        public SchemaDocument Document { get; }

        public SchemaDefinition Definition { get; }

        public string Namespace { get; }

        public string CSharpNamespace { get; }

        public string Path { get; }

        public string Key { get; }

        public bool IsEndpoint => this.Definition.Type == "procedure";

        public List<EnumProperties> EnumProperties { get; } = new();

        public List<PropertyGeneration> Properties { get; } = new();

        public bool IsDefinitionFile => this.Document.Id.EndsWith("defs");

        public string Id
        {
            get
            {
                if (this.Key == "main")
                {
                    return this.Document.Id;
                }
                else
                {
                    return $"{this.Document.Id}#{this.Key}";
                }
            }
        }

        public override string ToString()
        {
            return $"Class: {this.Id}, {this.ClassName}";
        }

        private void ProcessProperties()
        {
            if (this.Definition.Properties != null)
            {
                foreach (var prop in this.Definition.Properties)
                {
                    switch (prop.Value.Type)
                    {
                        case "string":
                            if (prop.Value.KnownValues is not null)
                            {
                                this.EnumProperties.Add(new EnumProperties(prop.Value, prop.Key, this.Document, this.Definition, this.Path, this.Namespace, this.CSharpNamespace));
                            }

                            break;
                    }

                    this.Properties.Add(new PropertyGeneration(prop.Value, prop.Key, this.Document, this.Definition, this.Path, this.Namespace, this.CSharpNamespace));
                }
            }
        }
    }

    private async Task GenerateModelFile(ClassGeneration cls)
    {
        Console.WriteLine($"Generating Class: {cls.Id}, {cls.CSharpNamespace}, {cls.ClassName}");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{cls.CSharpNamespace}");
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, cls.Definition);

        sb.AppendLine($"    public partial class {cls.ClassName}");
        sb.AppendLine("    {");

        foreach (var property in cls.Properties)
        {
            this.GenerateProperty(sb, property);
        }

        this.GenerateTypeProperty(sb, cls.Id);
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, cls.Path);
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{cls.ClassName}.g.cs");
        await File.WriteAllTextAsync(classPath, sb.ToString());

        foreach (var enumProp in cls.EnumProperties)
        {
            Console.WriteLine($"Generating Enum: {enumProp.Document.Id}, {enumProp.ClassName}");
            var enumSource = this.GenerateEnumSource(enumProp.ClassName, enumProp.PropertyDefinition, enumProp.CSharpNamespace);
            var enumPath = Path.Combine(outputPath, $"{enumProp.ClassName}.g.cs");
            await File.WriteAllTextAsync(enumPath, enumSource);
        }
    }

    private void GenerateProperty(StringBuilder sb, PropertyGeneration property)
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

        // Add validation attributes
        // GenerateValidationAttributes(sb, property);

        // Handle default values
        if (property.PropertyDefinition.Default != null)
        {
            sb.AppendLine($"        public {property.GetClassNameAndNamespace()} {property.ClassName} {{ get; set; }} = {property.GetDefaultValue()};");
        }
        else
        {
            sb.AppendLine($"        public {property.GetClassNameAndNamespace()} {property.ClassName} {{ get; set; }}");
        }

        sb.AppendLine();
    }

    private async Task GenerateModelFile(string defJsonPath, string baseOutputDir, JsonType jsonType)
    {
        var defJsonText = await File.ReadAllTextAsync(defJsonPath);
        var schemaDocument = JsonSerializer.Deserialize<SchemaDocument>(defJsonText, SourceGenerationContext.Default.SchemaDocument);
        if (schemaDocument == null)
        {
            Console.WriteLine($"Failed to deserialize {defJsonPath}.");
            return;
        }

        // Take everything except last.
        var path = schemaDocument.Id.Split('.').Take(schemaDocument.Id.Split('.').Length - 1).Select(n => n.ToPascalCase()).ToArray();
        var ns = $"{this.baseNamespace}.{string.Join(".", path)}";
        var outputPath = Path.Combine(baseOutputDir, Path.Combine(path));
        Directory.CreateDirectory(outputPath);

        switch (jsonType)
        {
            case JsonType.Defs:
                Console.WriteLine($"Definition: {schemaDocument.Id}");
                foreach (KeyValuePair<string, SchemaDefinition> def in schemaDocument.Defs)
                {
                    await this.GenerateClass(schemaDocument, def, outputPath, ns);
                }

                break;
            case JsonType.Other:
                foreach (var def in schemaDocument.Defs)
                {
                    switch (def.Value.Type)
                    {
                        case "object":
                            var cn = string.Join(string.Empty, schemaDocument.Id.Split('.').TakeLast(2).Select(n => n.ToPascalCase())).ToPascalCase();
                            if (def.Key != "main")
                            {
                                cn = $"{cn}{def.Key.ToPascalCase()}";
                            }

                            Console.WriteLine($"Generating Class: {schemaDocument.Id}, {cn}");
                            var classSource = this.GenerateClassSource(cn, def.Value, schemaDocument, ns);
                            var classPath = Path.Combine(outputPath, $"{cn}.g.cs");
                            await File.WriteAllTextAsync(classPath, classSource);

                            break;
                        default:
                            Console.WriteLine($"GenerateClass Unknown ({def.Value.Type}) {schemaDocument.Id} {def.Key}.");
                            break;
                    }
                }

                break;
        }
    }

    private async Task GenerateClass(SchemaDocument schemaDocument, KeyValuePair<string, SchemaDefinition> def, string outputPath, string ns)
    {
        var className = def.Key.ToPascalCase();
        switch (def.Value.Type)
        {
            case "string":
                Console.WriteLine($"Generating Enum {className}.");
                List<KeyValuePair<string, SchemaDefinition>> tokens = schemaDocument.Defs.Where(d => d.Value.Type == "token").ToList();
                var enumSource = this.GenerateEnumSource(className, def.Value, tokens, ns);
                var enumPath = Path.Combine(outputPath, $"{className}.g.cs");
                await File.WriteAllTextAsync(enumPath, enumSource);
                break;
            case "object":
                Console.WriteLine($"Generating Class {className}.");
                var classSource = this.GenerateClassSource(className, def.Value, schemaDocument, ns);
                var classPath = Path.Combine(outputPath, $"{className}.g.cs");
                await File.WriteAllTextAsync(classPath, classSource);
                break;
            case "token":
                // This is used in enums.
                break;
            default:
                Console.WriteLine($"GenerateClass Unknown ({def.Value.Type}) {className}.");
                break;
        }

        if (def.Value.Properties != null)
        {
            foreach (var prop in def.Value.Properties)
            {
                switch (prop.Value.Type)
                {
                    case "string":
                        if (prop.Value.KnownValues is not null)
                        {
                            var enumSource = this.GenerateEnumSource(prop.Key.ToPascalCase(), prop.Value, ns);
                            var enumPath = Path.Combine(outputPath, $"{prop.Key.ToPascalCase()}.g.cs");
                            await File.WriteAllTextAsync(enumPath, enumSource);
                        }

                        break;
                }
            }
        }
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

    private string GenerateClassSource(string className, SchemaDefinition definition, SchemaDocument document, string ns)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, ns);
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, definition);

        sb.AppendLine($"    public partial class {className}");
        sb.AppendLine("    {");

        // Generate properties
        if (definition.Properties != null)
        {
            foreach (var prop in definition.Properties)
            {
                this.GenerateProperty(sb, className, prop.Key, definition, prop.Value);
            }
        }

        this.GenerateTypeProperty(sb, document.Id);

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

    private void GenerateProperty(StringBuilder sb, string className, string propertyName, SchemaDefinition definition, PropertyDefinition property)
    {
        // Add property documentation if available
        if (!string.IsNullOrEmpty(property.Description))
        {
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {property.Description}");
            sb.AppendLine($"        /// </summary>");
        }

        // Add JSON property name attribute
        sb.AppendLine($"        [JsonPropertyName(\"{propertyName}\")]");

        // Add Required attribute if property is required
        if (definition.Required?.Contains(propertyName) == true)
        {
            sb.AppendLine("        [JsonRequired]");
        }

        // Add validation attributes
        // GenerateValidationAttributes(sb, property);

        // Generate the property
        var propertyType = this.GetPropertyType(className, propertyName.ToPascalCase(), property);
        var pascalPropertyName = propertyName.ToPascalCase().ToPreventClassPropertyNameClash(className);

        // Handle default values
        if (property.Default != null)
        {
            var defaultValue = this.GetDefaultValueString(property);
            if (property.KnownValues?.Length > 0)
            {
                defaultValue = $"{propertyName.ToPascalCase()}.{defaultValue.ToPascalCase()}";
            }

            sb.AppendLine($"        public {propertyType} {pascalPropertyName} {{ get; set; }} = {defaultValue};");
        }
        else
        {
            sb.AppendLine($"        public {propertyType} {pascalPropertyName} {{ get; set; }}");
        }

        sb.AppendLine();
    }

    private string GetDefaultValueString(PropertyDefinition property)
    {
        return property.Type?.ToLower() switch
        {
            "string" => property.Default?.ToString() ?? "\"\"",
            "integer" => property.Default?.ToString() ?? "0",
            "boolean" => property.Default?.ToString()?.ToLower() ?? "false",
            "array" => "new()",
            _ => "default",
        };
    }

    internal string GetPropertyType(string className, string name, PropertyDefinition property)
    {
        // Handle known values as enums
        if (property.KnownValues?.Length > 0)
        {
            return $"{name}".ToPascalCase();
        }

        var baseType = property.Type?.ToLower() switch
        {
            "string" when property.Format == "did" => "FishyFlip.Models.ATDid",
            "string" when property.Format == "handle" => "FishyFlip.Models.ATHandle",
            "string" when property.Format == "datetime" => "DateTime",
            "string" when property.Format == "at-uri" => "FishyFlip.Models.ATUri",
            "string" when property.Format == "at-identifier" => "FishyFlip.Models.ATIdentifier",
            "string" => "string",
            "blob" => "Blob",
            "integer" => "int",
            "boolean" => "bool",
            "bytes" => "byte[]",
            "cid-link" => "Ipfs.Cid",
            "array" when property.Items != null => this.GetListPropertyName(className, name, property),
            // "object" when property.Properties != null => "Dictionary<string, object>", // Could be expanded to generate nested types
            "unknown" => "object",
            "union" when property.Refs?.Length > 0 => "object", // Could be expanded to generate union types
            "ref" when !string.IsNullOrEmpty(property.Ref) && !property.Ref.Equals("com.atproto.repo.strongRef") => this.GetClassNameFromRef(property.Ref),
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "RepoStrongRef",
            _ => throw new InvalidOperationException($"Unknown property type: {property.Type}"),
        };

        return $"{baseType}?";
    }

    private string GetListPropertyName(string className, string name, PropertyDefinition property)
    {
        if (property.Items is null)
        {
            throw new InvalidOperationException("Property does not have items.");
        }

        var item = $"List<{this.GetPropertyType(className, name, property.Items)}>";
        Console.WriteLine($"List Property: {item}");
        return item;
    }

    private string GetClassNameFromRef(string refString)
    {
        if (!refString.Contains("#"))
        {
            var cn = string.Join(string.Empty, refString.Split('.').TakeLast(2).Select(n => n.ToPascalCase())).ToPascalCase();
            var ns2 = string.Join(".", refString.Split('.').Take(refString.Split('.').Length - 1).Select(n => n.ToPascalCase()));
            return $"{this.baseNamespace}.{ns2}.{cn}";
        }

        Console.WriteLine($"{refString} Has #, {refString.Contains("#")}");
        var className = refString.Split('#').Last().ToPascalCase();
        var namespaceName = refString.Split('#').First().Split('.').Select(n => n.ToPascalCase()).ToArray();
        Console.WriteLine($"Ref Property className: {className}");
        Console.WriteLine($"Ref Property namespaceName: {namespaceName}");
        if (namespaceName.Contains("Defs"))
        {
            namespaceName = namespaceName.Take(namespaceName.Length - 1).Select(n => n.ToPascalCase()).ToArray();
        }

        var ns = string.Join(".", namespaceName);
        if (string.IsNullOrEmpty(ns))
        {
            return className;
        }

        var item = $"{this.baseNamespace}.{ns}.{className}";
        Console.WriteLine($"Ref Property: {item}");
        return item;
    }

    private string GenerateEnumSource(string className, PropertyDefinition definition, string ns)
    {
        if (definition.KnownValues == null)
        {
            throw new InvalidOperationException("Definition does not have known values.");
        }

        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, ns);
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, definition);

        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Known values for {className}");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    [JsonConverter(typeof(JsonStringEnumConverter))]");
        sb.AppendLine($"    public enum {className}");
        sb.AppendLine("    {");
        sb.AppendLine();
        foreach (var value in definition.KnownValues)
        {
            var rawValue = value.Split('#').Last();
            var enumValue = rawValue.ToPascalCase();
            sb.AppendLine($"       /// <summary>");
            sb.AppendLine($"       /// Known value for {className}.");
            sb.AppendLine($"       /// </summary>");

            sb.AppendLine($"       [JsonPropertyName(\"{value}\")]");
            sb.AppendLine($"       {enumValue.ToClassSafe()},");
            sb.AppendLine();
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        return sb.ToString();
    }

    private string GenerateEnumSource(string className, SchemaDefinition definition, List<KeyValuePair<string, SchemaDefinition>> tokens, string ns)
    {
        if (definition.KnownValues == null)
        {
            throw new InvalidOperationException("Definition does not have known values.");
        }

        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, ns);
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, definition);

        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Known values for {className}");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    [JsonConverter(typeof(JsonStringEnumConverter))]");
        sb.AppendLine($"    public enum {className}");
        sb.AppendLine("    {");
        sb.AppendLine();
        foreach (var value in definition.KnownValues)
        {
            var rawValue = value.Split('#').Last();
            var tokenDef = tokens.FirstOrDefault(t => t.Key.Equals(rawValue));
            var enumValue = rawValue.ToPascalCase();
            if (tokenDef.Value?.Description != null)
            {
                sb.AppendLine($"       /// <summary>");
                sb.AppendLine($"       /// {tokenDef.Value.Description}");
                sb.AppendLine($"       /// </summary>");
            }
            else
            {
                sb.AppendLine($"       /// <summary>");
                sb.AppendLine($"       /// Known value for {className}.");
                sb.AppendLine($"       /// </summary>");
            }

            sb.AppendLine($"       [JsonPropertyName(\"{value}\")]");
            sb.AppendLine($"       {enumValue.ToClassSafe()},");
            sb.AppendLine();
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        return sb.ToString();
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