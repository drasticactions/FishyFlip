// <copyright file="SchemaDefinition.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FFSourceGen.Models;

public class SchemaDefinition
{
    [JsonPropertyName("type")]
    [JsonRequired]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("required")]
    public string[] Required { get; set; } = Array.Empty<string>();

    [JsonPropertyName("properties")]
    public Dictionary<string, PropertyDefinition> Properties { get; set; } = new();

    [JsonPropertyName("parameters")]
    public ParametersDefinition? Parameters { get; set; }

    [JsonPropertyName("output")]
    public OutputDefinition? Output { get; set; }

    [JsonPropertyName("record")]
    public RecordDefinition? Record { get; set; }

    [JsonPropertyName("errors")]
    public ErrorDefinition[]? Errors { get; set; }

    [JsonPropertyName("knownValues")]
    public string[]? KnownValues { get; set; }
}

public class RecordDefinition
{
    [JsonPropertyName("type")]
    [JsonRequired]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public Dictionary<string, PropertyDefinition> Properties { get; set; } = new();
}