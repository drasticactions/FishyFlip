// <copyright file="PropertyGeneration.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FFSourceGen;
using FFSourceGen.Models;

public class PropertyGeneration
{
    public PropertyGeneration(PropertyDefinition propertyDefinition, string key, SchemaDocument document, SchemaDefinition def, string path, string ns, string cns)
    {
        this.PropertyDefinition = propertyDefinition;
        this.Document = document;
        this.Definition = def;
        this.Path = path;
        this.Key = key;
        this.SetNamespace(ns, cns);
        this.ClassName = key != "main" ? key.ToPascalCase() : string.Join(string.Empty, document.Id.Split('.').TakeLast(1).Select(n => n.ToPascalCase())).ToPascalCase();
        this.Type = this.GetPropertyType(this.ClassName, this.ClassName, propertyDefinition);
    }

    private void SetNamespace(string ns, string cns)
    {
        string refString = string.Empty;

        if (this.PropertyDefinition.Ref != null)
        {
            refString = this.PropertyDefinition.Ref;
        }
        else if (this.PropertyDefinition.Items?.Ref is not null)
        {
            refString = this.PropertyDefinition.Items.Ref;
        }

        if (!string.IsNullOrEmpty(refString))
        {
            var splitHashtag = refString.Split('#').Where(n => !string.IsNullOrEmpty(n)).ToArray();
            if (splitHashtag.Length == 2)
            {
                this.Namespace = splitHashtag.First();
                var splitDot = this.Namespace.Split('.');
                if (splitDot.Contains("defs"))
                {
                    this.CSharpNamespace = string.Join(".", splitDot.Take(splitDot.Length - 1).Select(n => n.ToPascalCase()).ToArray());
                }
                else
                {
                    this.CSharpNamespace = string.Join(".", splitDot.Select(n => n.ToPascalCase()).ToArray());
                }
            }
            else
            {
                this.Namespace = ns;
                this.CSharpNamespace = cns;
            }

            return;
        }

        this.Namespace = ns;
        this.CSharpNamespace = cns;
    }

    public string ClassName { get; }

    public PropertyDefinition PropertyDefinition { get; }

    public SchemaDocument Document { get; }

    public SchemaDefinition Definition { get; }

    public string Key { get; }

    public string Path { get; }

    public string Namespace { get; private set; }

    public string CSharpNamespace { get; private set; }

    public bool IsDefinitionFile => this.Document.Id.EndsWith("defs");

    public string Type { get; }

    public string GetDefaultValue()
    {
        var defaultValue = this.GetDefaultValueString(this.PropertyDefinition);
        if (this.PropertyDefinition.KnownValues?.Length > 0)
        {
            defaultValue = $"{defaultValue.ToPascalCase()}";
        }

        if (this.IsEnum)
        {
            defaultValue = $"{this.ClassName}.{defaultValue}";
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
            // "string" when property.Format == "did" => "FishyFlip.Models.ATDid",
            // "string" when property.Format == "handle" => "FishyFlip.Models.ATHandle",
            "string" when property.Format == "datetime" => "DateTime",
            // "string" when property.Format == "at-uri" => "FishyFlip.Models.ATUri",
            // "string" when property.Format == "at-identifier" => "FishyFlip.Models.ATIdentifier",
            "string" => "string",
            "blob" => "Blob",
            "integer" => "int",
            "boolean" => "bool",
            "bytes" => "byte[]",
            "cid-link" => "Ipfs.Cid",
            "array" when property.Items != null => this.GetListPropertyName(className, name, property),
            "unknown" => "object",
            "union" when property.Refs?.Length > 0 => "object", // Could be expanded to generate union types
            "ref" when !string.IsNullOrEmpty(property.Ref) && !property.Ref.Equals("com.atproto.repo.strongRef") => this.GetClassNameFromRef(property.Ref),
            // "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "RepoStrongRef",
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "string",
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
        var propertyName = this.GetPropertyType(className, name, property.Items);
        var propertyNamePascal = string.Join(".", propertyName.Split('.').Select(n => n.ToPascalCase()));
        var item = $"List<{propertyNamePascal}>";
        Console.WriteLine($"List Property: {item}");
        return item;
    }

    private string GetClassNameFromRef(string refString)
    {
        Console.WriteLine($"{refString} Has #, {refString.Contains("#")}");
        var result = string.Join(".", refString.Replace(".defs", string.Empty).Replace("#", ".").Split('.').Select(n => n.ToPascalCase())).Split(".").Last();
        return result;
    }
}