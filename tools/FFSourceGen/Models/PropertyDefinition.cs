// <copyright file="PropertyDefinition.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FFSourceGen.Models;

public class PropertyDefinition
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("format")]
    public string Format { get; set; } = string.Empty;

    [JsonPropertyName("ref")]

    // [JsonPropertyName("$ref")]  // Handle both styles of ref property
    public string? Ref { get; set; }

    [JsonPropertyName("refs")]
    public string[]? Refs { get; set; }

    [JsonPropertyName("items")]
    public PropertyDefinition? Items { get; set; }

    [JsonPropertyName("minimum")]
    public int? Minimum { get; set; }

    [JsonPropertyName("maximum")]
    public int? Maximum { get; set; }

    [JsonPropertyName("maxLength")]
    public int? MaxLength { get; set; }

    [JsonPropertyName("maxGraphemes")]
    public int? MaxGraphemes { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("knownValues")]
    public string[]? KnownValues { get; set; }

    [JsonPropertyName("default")]
    public object? Default { get; set; }

    [JsonPropertyName("const")]
    public object? Const { get; set; }

    [JsonPropertyName("properties")]
    public Dictionary<string, PropertyDefinition>? Properties { get; set; }

    public string CSharpType => this.Type switch
    {
        "string" when this.Format == "did" => "FishyFlip.Models.ATDid",
        "string" when this.Format == "handle" => "FishyFlip.Models.ATHandle",
        "string" when this.Format == "datetime" => "DateTime",
        "string" when this.Format == "at-uri" => "FishyFlip.Models.ATUri",
        "string" when this.Format == "at-identifier" => "FishyFlip.Models.ATIdentifier",
        "string" => "string",
        "number" => "double",
        "integer" => "int",
        "boolean" => "bool",
        "array" => $"List<{this.Items?.CSharpType}?>",
        _ => "ATObject",
    };
}