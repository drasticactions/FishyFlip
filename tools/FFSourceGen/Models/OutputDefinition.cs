// <copyright file="OutputDefinition.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;

namespace FFSourceGen.Models;

public class OutputDefinition
{
    [JsonPropertyName("encoding")]
    [JsonRequired]
    public string Encoding { get; set; } = string.Empty;

    [JsonPropertyName("schema")]
    [JsonRequired]
    public SchemaDefinition Schema { get; set; } = new();
}