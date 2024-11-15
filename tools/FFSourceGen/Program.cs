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

        foreach (var property in cls.Properties)
        {
            this.GenerateProperty(sb, property, cls);
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
            await this.GenerateEnum(enumProp);
            // Console.WriteLine($"Generating Enum: {enumProp.Document.Id}, {enumProp.ClassName}");
            // var enumSource = this.GenerateEnumSource(enumProp.ClassName, enumProp.PropertyDefinition, enumProp.CSharpNamespace);
            // var enumPath = Path.Combine(outputPath, $"{enumProp.ClassName}.g.cs");
            // await File.WriteAllTextAsync(enumPath, enumSource);
        }
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

        // Add validation attributes
        // GenerateValidationAttributes(sb, property);

        var propertyName = property.ClassName;
        if (propertyName == cls.ClassName)
        {
            propertyName = $"{propertyName}Value";
        }

        if (propertyName == "Type")
        {
            propertyName = "TypeValue";
        }

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