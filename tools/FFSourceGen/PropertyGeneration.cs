// <copyright file="PropertyGeneration.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;
using FFSourceGen;
using FFSourceGen.Models;

public class PropertyGeneration
{
    public PropertyGeneration(PropertyDefinition propertyDefinition, string key, SchemaDocument document, SchemaDefinition def, string path, string ns, string cns, string cn)
    {
        this.PropertyDefinition = propertyDefinition;
        this.Document = document;
        this.Definition = def;
        this.Path = path;
        this.Key = key;
        this.SetNamespace(ns, cns);
        this.Id = $"{document.Id}#{key}";
        this.ClassName = key != "main" ? key.ToPascalCase() : string.Join(string.Empty, document.Id.Split('.').TakeLast(1).Select(n => n.ToPascalCase())).ToPascalCase();
        this.Type = this.GetPropertyType(this.ClassName, this.ClassName, propertyDefinition);
        this.PropertyName = this.ClassName;
        if (this.PropertyName == cn)
        {
            this.PropertyName = $"{this.PropertyName}Value";
        }

        if (this.PropertyName == "Type")
        {
            this.PropertyName = "TypeValue";
        }


        this.CBorProperty = this.GenerateCborProperty();
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

    public string Id { get; }

    public string ClassName { get; }

    public string RawType { get; private set; }

    public PropertyDefinition PropertyDefinition { get; }

    public SchemaDocument Document { get; }

    public SchemaDefinition Definition { get; }

    public bool IsATObject => this.Type.Contains("ATObject");

    public string PropertyName { get; }

    public string Key { get; }

    public string Path { get; }

    public string Namespace { get; private set; }

    public string CSharpNamespace { get; private set; }

    public string CBorProperty { get; }

    public bool IsDefinitionFile => this.Document.Id.EndsWith("defs");

    public string Type { get; private set; }

    public bool IsBaseType => this._IsBaseType(this.Type);

    public bool IsEnum => this.PropertyDefinition.KnownValues?.Length > 0;

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

    private string GenerateCborProperty()
    {
        var property = this.PropertyDefinition;
        if (property.KnownValues?.Length > 0)
        {
            return $"// enum";
        }

        var baseType = property.Type?.ToLower() switch
        {
            "string" when property.Format == "did" => $"this.{this.PropertyName} = obj[\"{this.Key}\"].ToATDid();",
            "string" when property.Format == "datetime" => $"this.{this.PropertyName} = obj[\"{this.Key}\"].ToDateTime();",
            "string" when property.Format == "handle" => $"this.{this.PropertyName} = obj[\"{this.Key}\"].ToATHandle();",
            "string" when property.Format == "at-uri" => $"this.{this.PropertyName} = obj[\"{this.Key}\"].ToATUri();",
            "string" when property.Format == "at-identifier" => $"this.{this.PropertyName} = obj[\"{this.Key}\"].ToATIdentifier();",
            "string" => $"this.{this.PropertyName} = obj[\"{this.Key}\"].AsString();",
            "cid-link" => $"this.{this.PropertyName} = obj[\"{this.Key}\"].ToATCid();",
            //"ref" when !string.IsNullOrEmpty(property.Ref) && !property.Ref.Equals("com.atproto.repo.strongRef") => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = new {this.Type.Replace("?", string.Empty)}(obj[\"{this.Key}\"]);",
            // "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => $"this.{this.PropertyName} = obj[\"{this.Key}\"].AsString();",
            _ => $"// {property.Type?.ToLower()}",
        };

        return baseType;
    }

    private bool _IsBaseType(string type)
    {
        var item = type.Replace("?", string.Empty);
        if (item.Contains("List<"))
        {
            return this._IsBaseType(item.Replace("List<", string.Empty).Replace(">", string.Empty));
        }

        return type.Replace("?", string.Empty) switch
        {
            "string" => true,
            "long" => true,
            "bool" => true,
            "byte[]" => true,
            "Blob" => true,
            "DateTime" => true,
            "Ipfs.Cid" => true,
            _ => false,
        };
    }

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
            "integer" => "long",
            "boolean" => "bool",
            "bytes" => "byte[]",
            "cid-link" => "Ipfs.Cid",
            "array" when property.Items != null => this.GetListPropertyName(className, name, property),
            "unknown" => "ATObject",
            "object" => "ATObject",
            "record" => "ATObject",
            "union" when property.Refs?.Length > 0 => "ATObject", // Could be expanded to generate union types
            "ref" when !string.IsNullOrEmpty(property.Ref) && !property.Ref.Equals("com.atproto.repo.strongRef") => this.GetClassNameFromRef(property.Ref),
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "Com.Atproto.Repo.StrongRef",
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "string",
            _ => throw new InvalidOperationException($"Unknown property type: {property.Type}"),
        };

        if (string.IsNullOrEmpty(baseType))
        {
            throw new InvalidOperationException("Base Type is null or empty.");
        }

        if (string.IsNullOrEmpty(this.RawType))
        {
            this.RawType = baseType;
        }

        return $"{baseType}?";
    }

    private string GetListPropertyName(string className, string name, PropertyDefinition property)
    {
        if (property.Items is null)
        {
            throw new InvalidOperationException("Property does not have items.");
        }
        var propertyName = this.GetPropertyType(className, name, property.Items);
        var propertyNamePascal = !this._IsBaseType(propertyName) ? string.Join(".", propertyName.Split('.').Select(n => n.ToPascalCase())) : propertyName;
        var item = $"List<{propertyName}>";
        Console.WriteLine($"List Property: {item}");
        this.RawType = propertyName;
        return item;
    }

    private string GetClassNameFromRef(string refString)
    {
        var idSplit = refString.Split('#');
        var namespaceName = idSplit.First().Replace(".defs", string.Empty);
        var idName = idSplit.Last();

        if (AppCommands.AllClasses.Any() && !string.IsNullOrEmpty(namespaceName) && !string.IsNullOrEmpty(idName))
        {
            var mainRef = AppCommands.AllClasses.FirstOrDefault(n => n.Id == refString);
            if (mainRef is not null)
            {
                if (mainRef.Definition.Type == "string")
                {
                    return "string";
                }

                return $"{mainRef.CSharpNamespace}.{mainRef.ClassName}";
            }
        }

        if (string.IsNullOrEmpty(namespaceName) && string.IsNullOrEmpty(idName))
        {
            return "ATObject";
        }

        var localRef = this.Document.Defs.Where(n => n.Key == idName).Select(n => n.Value).FirstOrDefault();
        if (localRef is not null)
        {
            switch (localRef.Type)
            {
                case "string":
                    return "string";
                case "object":
                    return idName.ToPascalCase();
                default:
                    Console.WriteLine($"Local Ref: {localRef.Type}");
                    break;
            }
        }

        if (string.IsNullOrEmpty(namespaceName) && !string.IsNullOrEmpty(idName))
        {
            return idName.ToPascalCase();
        }

        if (!string.IsNullOrEmpty(namespaceName) && string.IsNullOrEmpty(idName))
        {
            // Ref is in another namespace.
            return string.Join(".", namespaceName.Split('.').Select(n => n.ToPascalCase()));
        }

        if (idName.Contains("."))
        {
            return string.Join(".", idName.Split('.').Select(n => n.ToPascalCase()));
        }

        if (!string.IsNullOrEmpty(namespaceName) && !string.IsNullOrEmpty(idName))
        {
            return $"{string.Join(".", namespaceName.Split('.').Select(n => n.ToPascalCase()))}.{idName.ToPascalCase()}";
        }

        Console.WriteLine($"{refString} Has #, {refString.Contains("#")}");
        if (refString.Contains(".") && !refString.Contains("#"))
        {
            var full = string.Join(".", refString.Replace(".defs", string.Empty).Replace("#", ".").Split('.').Select(n => n.ToPascalCase()));
            return full;
            // if (AppCommands.AllProperties.Any())
            // {
            //     var ff = AppCommands.AllProperties.FirstOrDefault(n => n.Id == refString);
            //     if (ff is not null)
            //     {
            //     }
            // }
        }

        var id = string.Join(".", refString.Replace(".defs", string.Empty).Replace("#", ".").Split('.')).Split(".").Last();
        var test = this.Document.Defs.Where(n => n.Key == id).Select(n => n.Value).FirstOrDefault();
        if (test is not null)
        {
            if (test.Type == "string")
            {
                return "string";
            }
        }
        var result = string.Join(".", refString.Replace(".defs", string.Empty).Replace("#", ".").Split('.').Select(n => n.ToPascalCase())).Split(".").Last();
        return result;
    }
}