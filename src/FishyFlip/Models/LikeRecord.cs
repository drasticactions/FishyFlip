// <copyright file="LikeRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;

namespace FishyFlip.Models;

public class LikeRecord
{
    public Subject Subject { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("$type")]
    public string Type { get; set; }
}