// <copyright file="ClassGeneration.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FFSourceGen;
using FFSourceGen.Models;

public class ClassGeneration
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
        this.CBorProperty = $"this.{this.ClassName} = new {this.ClassName}(obj[\"{this.Key}\"]);";
    }

    public string ClassName { get; }

    public SchemaDocument Document { get; }

    public SchemaDefinition Definition { get; }

    public string Namespace { get; }

    public string CSharpNamespace { get; }

    public string Path { get; }

    public string Key { get; }

    public string CBorProperty { get; }

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

                this.Properties.Add(new PropertyGeneration(prop.Value, prop.Key, this.Document, this.Definition, this.Path, this.Namespace, this.CSharpNamespace, this.ClassName));
            }
        }

        if (this.Definition.Record is not null)
        {
            foreach (var prop in this.Definition.Record.Properties)
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

                this.Properties.Add(new PropertyGeneration(prop.Value, prop.Key, this.Document, this.Definition, this.Path, this.Namespace, this.CSharpNamespace, this.ClassName));
            }
        }
    }
}