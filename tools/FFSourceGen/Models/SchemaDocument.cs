// <copyright file="SchemaDocument.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FFSourceGen.Models;

public class SchemaDocument
{
    [JsonPropertyName("lexicon")]
    [JsonRequired]
    public int Lexicon { get; set; }

    [JsonPropertyName("id")]
    [JsonRequired]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("defs")]
    [JsonRequired]
    public Dictionary<string, SchemaDefinition> Defs { get; set; } = new();
}