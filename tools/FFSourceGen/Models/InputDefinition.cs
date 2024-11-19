// <copyright file="InputDefinition.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;

namespace FFSourceGen.Models;

public class InputDefinition
{
    [JsonPropertyName("encoding")]
    [JsonRequired]
    public string Encoding { get; set; } = string.Empty;

    [JsonPropertyName("schema")]
    public SchemaDefinition? Schema { get; set; } = new();
}