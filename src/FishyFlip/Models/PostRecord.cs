// <copyright file="PostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;

namespace FishyFlip.Models;

public class PostRecord : Record
{
    public string Text { get; set; }

    public Facet[]? Facets { get; set; } = Array.Empty<Facet>();

    public string[] Langs { get; set; } = Array.Empty<string>();
    
    public Reply Reply { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("$type")]
    public string Type { get; set; }
}