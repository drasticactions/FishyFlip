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
    /// <summary>
    /// Generate lexicon source code.
    /// </summary>
    /// <param name="lexiconPath">Path to lexicon files.</param>
    /// <param name="outputDir">-o, Output directory.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    [Command("generate")]
    public async Task GenerateAsync([Argument] string lexiconPath, string? outputDir = default, CancellationToken cancellationToken = default)
    {
        outputDir ??= AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine($"Lexicon Path: {lexiconPath}");
        Console.WriteLine($"Output Directory: {outputDir}");
        var files = Directory.EnumerateFiles(lexiconPath, "*.json", SearchOption.AllDirectories);
        Console.WriteLine($"Found {files.Count()} files.");

        var modelPath = Path.Combine(outputDir, "Models");
        if (Directory.Exists(modelPath))
        {
            Directory.Delete(modelPath, true);
        }

        Directory.CreateDirectory(modelPath);
        foreach (var jsonFile in files)
        {
            await this.GenerateModelFile(jsonFile, modelPath);
        }
    }

    private async Task GenerateModelFile(string defJsonPath, string baseOutputDir)
    {
        var defJsonText = await File.ReadAllTextAsync(defJsonPath);
        var schemaDocument = JsonSerializer.Deserialize<SchemaDocument>(defJsonText, SourceGenerationContext.Default.SchemaDocument);
        if (schemaDocument == null)
        {
            Console.WriteLine($"Failed to deserialize {defJsonPath}.");
            return;
        }

        Console.WriteLine($"Definition: {schemaDocument.Id}");
        foreach (var def in schemaDocument.Defs)
        {
            var className = def.Key.ToPascalCase();
            switch (def.Value.Type)
            {
                case "string":
                    Console.WriteLine($"Generating Enum {className}.");
                    List<KeyValuePair<string, SchemaDefinition>> tokens = schemaDocument.Defs.Where(d => d.Value.Type == "token").ToList();
                    var enumSource = this.GenerateEnumSource(className, def.Value, tokens);
                    var enumPath = Path.Combine(baseOutputDir, $"{className}.g.cs");
                    await File.WriteAllTextAsync(enumPath, enumSource);
                    break;
                case "object":
                    Console.WriteLine($"Generating Class {className}.");
                    var classSource = this.GenerateClassSource(className, def.Value);
                    var classPath = Path.Combine(baseOutputDir, $"{className}.{schemaDocument.Id}.g.cs");
                    await File.WriteAllTextAsync(classPath, classSource);
                    break;
            }
        }
    }

    private string GenerateClassSource(string className, SchemaDefinition definition)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb);
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

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        return sb.ToString();
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
            sb.AppendLine("        [Required]");
        }

        // Add validation attributes
        // GenerateValidationAttributes(sb, property);

        // Generate the property
        var propertyType = this.GetPropertyType(className, propertyName.ToPascalCase(), property);
        var pascalPropertyName = propertyName.ToPascalCase();

        // Handle default values
        if (property.Default != null)
        {
            var defaultValue = this.GetDefaultValueString(property);
            if (property.KnownValues?.Length > 0)
            {
                defaultValue = $"{className}{propertyName.ToPascalCase()}.{defaultValue.ToPascalCase()}";
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

    private string GetPropertyType(string className, string name, PropertyDefinition property)
    {
        // Handle known values as enums
        if (property.KnownValues?.Length > 0)
        {
            return $"{className}{name}".ToPascalCase();
        }

        var baseType = property.Type?.ToLower() switch
        {
            "string" when property.Format == "did" => "FishyFlip.Models.ATDid",
            "string" when property.Format == "handle" => "FishyFlip.Models.ATHandle",
            "string" when property.Format == "datetime" => "DateTime",
            "string" when property.Format == "at-uri" => "FishyFlip.Models.ATUri",
            "string" when property.Format == "at-identifier" => "FishyFlip.Models.ATIdentifier",
            "string" => "string",
            "integer" => "int",
            "boolean" => "bool",
            "array" when property.Items != null => $"List<{this.GetPropertyType(className, name, property.Items)}>",
            // "object" when property.Properties != null => "Dictionary<string, object>", // Could be expanded to generate nested types
            "unknown" => "object",
            "union" when property.Refs?.Length > 0 => "object", // Could be expanded to generate union types
            "ref" when !string.IsNullOrEmpty(property.Ref) && !property.Ref.Equals("com.atproto.repo.strongRef") => property.Ref.Split('#').Last().ToPascalCase(),
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "FishyFlip.Models.StrongRef",
            _ => "object",
        };

        return $"{baseType}?";
    }

    private string GenerateEnumSource(string className, SchemaDefinition definition, List<KeyValuePair<string, SchemaDefinition>> tokens)
    {
        if (definition.KnownValues == null)
        {
            throw new InvalidOperationException("Definition does not have known values.");
        }

        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb);
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

        // Add using statements
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using System.ComponentModel.DataAnnotations;");
        sb.AppendLine("using System.Text.Json.Serialization;");

        // Add #nullable
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
    }

    private void GenerateNamespace(StringBuilder sb, string ns = "FishyFlip.Lexicon")
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
}