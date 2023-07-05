// <copyright file="FacetFeature.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;

namespace FishyFlip.Models;

public abstract class FacetFeature
{
    [JsonPropertyName("$type")]
    public abstract string Type { get; }
}