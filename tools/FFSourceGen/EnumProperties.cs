// <copyright file="EnumProperties.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FFSourceGen;
using FFSourceGen.Models;

public class EnumProperties
{
    public EnumProperties(PropertyDefinition propertyDefinition, string key, SchemaDocument document, SchemaDefinition def, string path, string ns, string cns)
    {
        this.PropertyDefinition = propertyDefinition;
        this.Document = document;
        this.Definition = def;
        this.Path = path;
        this.Key = key;
        this.SetNamespace(ns, cns);
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

    public string Namespace { get; private set; }

    public string CSharpNamespace { get; private set; }

    public bool IsDefinitionFile => this.Document.Id.EndsWith("defs");

    public override string ToString()
    {
        return $"Enum: {this.Document.Id}, {this.ClassName} - {string.Join(", ", this.RawValues)}";
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
}