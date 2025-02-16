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
        this.Namespace = string.Join(".", document.Id.Split('.').Take(document.Id.Split('.').Length - 1).ToArray());
        this.CSharpNamespace = string.Join(".", document.Id.Split('.').Take(document.Id.Split('.').Length - 1).Select(n => n.ToPascalCase()).ToArray());
        this.ClassName = this.GenerateClassName();
        this.ProcessProperties();
        this.CBorProperty = $"this.{this.ClassName} = new {this.ClassName}(obj[\"{this.Key}\"]);";
    }

    private string GenerateClassName()
    {
        var name = string.Empty;

        if (this.Key == "main")
        {
            var item = string.Join(string.Empty, this.Document.Id.Split('.').TakeLast(1));
            var test = this.HasPropertyWithClassName(item);
            var itemsToTake = 1;
            if (this.HasPropertyWithClassName(item))
            {
                itemsToTake = 2;
            }

            name = string.Join(string.Empty, this.Document.Id.Split('.').TakeLast(itemsToTake).Select(n => n.ToPascalCase())).ToPascalCase();
        }
        else if (this.Key == "view" || this.Key == "viewExternal")
        {
            var item = this.Key.ToPascalCase();
            var root = this.Document.Id.Split("#").First();
            var newItem = $"{item}{string.Join(string.Empty, root.Split('.').TakeLast(1).Select(n => n.ToPascalCase())).ToPascalCase()}";
            if (newItem == "ViewRecord")
            {
                return "ViewRecordDef";
            }

            name = newItem;
        }

        if (string.IsNullOrEmpty(name))
        {
            name = this.Key.ToPascalCase();
            var testingBesting = $"{this.Namespace}{this.Key}";
            var existingNamespace = AppCommands.AllClasses.Where(n => n.Namespace == this.Namespace);
            var existingClass = existingNamespace.FirstOrDefault(n => n.ClassName == name);
            if (existingClass != null)
            {
                name = $"{name}Def";
            }
        }

        return name;
    }

    public string ClassName { get; }

    public string FullClassName => $"{this.CSharpNamespace}.{this.ClassName}";

    public bool IsOutput => this.Key.Contains("Output");

    public bool IsArray => this.Definition.Type == "array";

    public bool IsBaseType => this.Definition.Type == "string" || this.Definition.Type == "number" || this.Definition.Type == "boolean";

    public bool IsArrayOfATObjects => this.Definition.Type == "array" && (this.Definition.Items.Type == "object" || this.Definition.Items.Type == "union");

    public bool IsArrayOfStrings => this.Definition.Type == "string" && this.Definition.KnownValues?.Count() > 0;

    public SchemaDocument Document { get; }

    public SchemaDefinition Definition { get; }

    public string FullNamespace { get; }

    public string Namespace { get; }

    public string CSharpNamespace { get; }

    public string Path { get; }

    public string Key { get; }

    public string CBorProperty { get; }

    public bool HasCursor => this.Properties.Any(n => n.PropertyName == "Cursor");

    public bool HasLimit => this.Properties.Any(n => n.PropertyName == "Limit");

    public bool HasOutputList => this.Properties.Any(n => n.IsArray);

    public PropertyGeneration? OutputList => this.Properties.FirstOrDefault(n => n.IsArray);

    public bool IsEndpoint => this.Definition.Type == "procedure";

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

    private bool HasPropertyWithClassName(string key)
    {
        var test = this.Definition.Properties.TryGetValue(key, out var prop);
        if (test)
        {
            return true;
        }

        return false;
    }

    private void ProcessProperties()
    {
        if (this.Definition.Properties != null)
        {
            foreach (var prop in this.Definition.Properties)
            {
                if (prop.Value.Description.Contains("DEPRECATED", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                this.Properties.Add(new PropertyGeneration(prop.Value, prop.Key, this));
            }
        }

        if (this.Definition.Record is not null)
        {
            foreach (var prop in this.Definition.Record.Properties)
            {
                if (prop.Value.Description.Contains("DEPRECATED", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                this.Properties.Add(new PropertyGeneration(prop.Value, prop.Key, this));
            }
        }
    }
}