// <copyright file="PropertyGeneration.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;
using FFSourceGen;
using FFSourceGen.Models;

public class PropertyGeneration
{
    public PropertyGeneration(PropertyDefinition propertyDefinition, string key, ClassGeneration cs)
    {
        this.Class = cs;
        this.PropertyDefinition = propertyDefinition;
        this.Key = key;
        this.SetNamespace(this.Class.Namespace, this.Class.CSharpNamespace);
        this.Id = $"{this.Class.Document.Id}#{key}";
        this.ClassName = key != "main" ? key.ToPascalCase() : string.Join(string.Empty, this.Class.Document.Id.Split('.').TakeLast(1).Select(n => n.ToPascalCase())).ToPascalCase();
        this.PropertyName = this.ClassName;
        if (this.PropertyName == this.Class.ClassName)
        {
            this.PropertyName = $"{this.PropertyName}Value";
        }

        if (this.PropertyName == "Type")
        {
            this.PropertyName = "TypeValue";
        }
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

    public ClassGeneration? Class { get; }

    public string Id { get; }

    public string ClassName { get; }

    public string RawType => this.Type.Replace("?", string.Empty);

    public PropertyDefinition PropertyDefinition { get; }

    public SchemaDocument Document => this.Class.Document;

    public SchemaDefinition Definition => this.Class.Definition;

    public bool IsATObject => this.Type.Contains("ATObject");

    public string PropertyName { get; }

    public string Key { get; }

    public string Path { get; }

    public string Namespace { get; private set; }

    public string CSharpNamespace { get; private set; }

    public string CBorProperty => this.GenerateCborProperty();

    public bool IsDefinitionFile => this.Document.Id.EndsWith("defs");

    public string Type => this.GetPropertyType(this.ClassName, this.ClassName, this.PropertyDefinition);

    public bool IsBaseType => this._IsBaseType(this.Type);

    public string GetDefaultValue()
    {
        var defaultValue = this.GetDefaultValueString(this.PropertyDefinition);
        if (this.PropertyDefinition.KnownValues?.Length > 0)
        {
            if (this.Type == "string?")
            {
                return $"\"{defaultValue}\"";
            }

            return $"{defaultValue}";
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

    private string GenerateCborPropertyForUnknownType(PropertyDefinition property)
    {
        if (this.ClassName == "DidDoc")
        {
            return "// Ignore DidDoc";
        }

        return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATObject();";
    }

    private string GenerateCborProperty()
    {
        var property = this.PropertyDefinition;
        // if (property.KnownValues?.Length > 0)
        // {
        //     return $"// enum";
        // }

        var baseType = property.Type?.ToLower() switch
        {
            "string" when property.Format == "did" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATDid();",
            "string" when property.Format == "datetime" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToDateTime();",
            "string" when property.Format == "handle" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATHandle();",
            "string" when property.Format == "at-uri" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATUri();",
            "string" when property.Format == "at-identifier" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATIdentifier();",
            "string" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].AsString();",
            "integer" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].AsInt64Value();",
            "boolean" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].AsBoolean();",
            "bytes" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].EncodeToBytes();",
            "cid-link" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATCid();",
            "unknown" => this.GenerateCborPropertyForUnknownType(property),
            "object" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATObject();",
            "record" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].ToATObject();",
            "union" when property.Refs?.Length > 0 => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = {this.GetCborUnionType(property, this.Key)};",
            "blob" => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = new FishyFlip.Models.Blob(obj[\"{this.Key}\"]);",
            "array" when property.Items != null => this.GetListString(),
            "ref" when !string.IsNullOrEmpty(property.Ref) && !this.IsBaseType && !property.Ref.Equals("com.atproto.repo.strongRef") => this.GetFullCBORTypeFromRef(property.Ref),
            "ref" when !string.IsNullOrEmpty(property.Ref) && this.IsBaseType && !property.Ref.Equals("com.atproto.repo.strongRef") => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].{this.TypeToCBorCast(this.Type)};",
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj[\"{this.Key}\"]);",
            _ => throw new InvalidOperationException($"Unknown property type: {property.Type}"),
        };

        return baseType;
    }

    private string GetCborUnionType(PropertyDefinition property, string key)
    {
        if (property.Refs?.Length == 1)
        {
            var objectName = this.GetClassNameFromRef(property.Refs[0]);
            return $"new {objectName}(obj[\"{this.Key}\"])";
        }

        return $"obj[\"{this.Key}\"].ToATObject()";
    }

    private string GetListString()
    {
        var propertyName = this.GetPropertyType(this.ClassName, this.ClassName, this.PropertyDefinition.Items!);

        if (this.IsBaseType)
        {
            return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.{this.TypeToCBorCast(propertyName)}).ToList();";
        }

        if (propertyName.Contains("ATObject"))
        {
            return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.ToATObject()).ToList();";
        }

        switch (propertyName)
        {
            case "FishyFlip.Models.ATDid?":
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.ToATDid()!).ToList();";
            case "FishyFlip.Models.ATHandle?":
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.ToATHandle()!).ToList();";
            case "FishyFlip.Models.ATUri?":
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.ToATUri()!).ToList();";
            case "FishyFlip.Models.ATIdentifier?":
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.ToATIdentifier()!).ToList();";
            case "DateTime?":
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n.ToDateTime()).ToList();";
            case "Blob?":
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(new FishyFlip.Models.Blob(n)).ToList();";
            case "Ipfs.Cid?":
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.ToATCid()!).ToList();";
            default:
                return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>new {propertyName.Replace("?", string.Empty)}(n)).ToList();";
        }
    }

    private string GetFullCBORTypeFromRef(string refString)
    {
        if (AppCommands.DoesRefStringHaveNoNamespace(refString))
        {
            refString = $"{this.Id.Split("#").First()}{refString}";
        }

        var classRef = AppCommands.FindClassFromRef(refString);

        if (classRef is not null)
        {
            if (classRef.IsArray)
            {
                if (classRef.IsArrayOfATObjects)
                {
                    return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.ToATObject()).ToList();";
                }

                if (classRef.IsArrayOfStrings)
                {
                    return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = obj[\"{this.Key}\"].Values.Select(n =>n.AsString()).ToList();";
                }
            }

            return $"if (obj[\"{this.Key}\"] is not null) this.{this.PropertyName} = new {this.Type.Replace("?", string.Empty)}(obj[\"{this.Key}\"]);";
        }

        throw new InvalidOperationException($"Could not find class ref: {refString}");
    }

    private string TypeToCBorCast(string type)
    {
        return type.Replace("?", string.Empty).ToLowerInvariant() switch
        {
            "string" => "AsString()",
            "long" => "AsInt64Value()",
            "bool" => "AsBoolean()",
            "byte[]" => "EncodeToBytes()",
            "datetime" => "AsDateTime()",
            "ipfs.cid" => "ToATCid()!",
            _ => throw new InvalidOperationException($"Unknown type: {type}"),
        };
    }

    private bool _IsBaseType(string type)
    {
        var item = type.Replace("?", string.Empty);
        if (item.Contains("List<"))
        {
            return this._IsBaseType(item.Replace("List<", string.Empty).Replace(">", string.Empty));
        }

        return type.Replace("?", string.Empty).ToLowerInvariant() switch
        {
            "string" => true,
            "long" => true,
            "integer" => true,
            "bool" => true,
            "boolean" => true,
            "byte[]" => true,
            "blob" => true,
            "datetime" => true,
            "ipfs.cid" => true,
            _ => false,
        };
    }

    private string GetUnknownObjectType(PropertyDefinition property)
    {
        if (this.ClassName == "DidDoc")
        {
            return "FishyFlip.Models.DidDoc";
        }

        return "ATObject";
    }

    private string GetPropertyType(string className, string name, PropertyDefinition property)
    {
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
            "unknown" => this.GetUnknownObjectType(property),
            "object" => "ATObject",
            "record" => "ATObject",
            "union" => this.GetUnionType(property), // Could be expanded to generate union types
            "ref" when !string.IsNullOrEmpty(property.Ref) && !property.Ref.Equals("com.atproto.repo.strongRef") => this.GetClassNameFromRef(property.Ref),
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "Com.Atproto.Repo.StrongRef",
            "ref" when !string.IsNullOrEmpty(property.Ref) && property.Ref.Equals("com.atproto.repo.strongRef") => "string",
            _ => throw new InvalidOperationException($"Unknown property type: {property.Type}"),
        };

        if (string.IsNullOrEmpty(baseType))
        {
            throw new InvalidOperationException("Base Type is null or empty.");
        }

        return $"{baseType}?";
    }

    private string GetUnionType(PropertyDefinition property)
    {
        if (property.Refs?.Length == 1)
        {
            return this.GetClassNameFromRef(property.Refs[0]);
        }

        return "ATObject";
    }

    private string GetListPropertyName(string className, string name, PropertyDefinition property)
    {
        if (property.Items is null)
        {
            throw new InvalidOperationException("Property does not have items.");
        }
        var propertyName = this.GetPropertyType(className, name, property.Items);
        var propertyNamePascal = !this._IsBaseType(propertyName) ? string.Join(".", propertyName.Split('.').Select(n => n.ToPascalCase())) : propertyName;
        var item = $"List<{propertyName.Replace("?", string.Empty)}>";
        Console.WriteLine($"List Property: {item}");
        return item;
    }

    private string GetClassNameFromRef(string refString)
    {
        if (!refString.Contains("."))
        {
            refString = $"{this.Document.Id}{refString}";
        }

        var mainRef = AppCommands.FindClassFromRef(refString);
        var idSplit = refString.Split('#');
        var namespaceName = idSplit.First().Replace(".defs", string.Empty);
        var idName = idSplit.Last();

        if (mainRef is not null)
        {
            if (mainRef.Definition.Type == "string")
            {
                return "string";
            }

            if (mainRef.Definition.Type == "array" && mainRef.Definition.Items?.Type == "string")
            {
                return "List<string?>";
            }

            if (mainRef.Definition.Type == "array" && mainRef.Definition.Items?.Type == "union")
            {
                return "List<ATObject>";
            }

            return $"{mainRef.CSharpNamespace}.{mainRef.ClassName}";
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

        return "ATObject";
    }
}