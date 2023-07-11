// <copyright file="Facet.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Facet : ATRecord
{
    [JsonConstructor]
    public Facet(FacetIndex? index, FacetFeature[] features)
    {
        this.Index = index;
        this.Features = features;
    }

    public Facet(CBORObject obj)
    {
        this.Index = obj["index"] is not null ? new FacetIndex(obj["index"]) : null;
        this.Features = obj["features"] is not null ? obj["features"].Values.Select(n => new FacetFeature(n)).ToArray() : null;
    }

    public FacetIndex? Index { get; }

    public FacetFeature[]? Features { get; }
}

public class FacetIndex
{
    [JsonConstructor]
    public FacetIndex(int byteStart, int byteEnd)
    {
        this.ByteStart = byteStart;
        this.ByteEnd = byteEnd;
    }

    public FacetIndex(CBORObject obj)
    {
        this.ByteStart = obj["byteStart"].AsInt32();
        this.ByteEnd = obj["byteEnd"].AsInt32();
    }

    public int ByteEnd { get; }

    public int ByteStart { get; }
}

public class FacetFeature
{
    [JsonConstructor]
    public FacetFeature(string? type, string? uri, AtDid? did)
    {
        this.Type = type;
        this.Uri = uri;
        this.Did = did;
    }

    public FacetFeature(CBORObject obj)
    {
        this.Type = obj["$type"].AsString();
        this.Uri = obj["uri"]?.AsString();
        this.Did = obj["did"] is not null ? AtDid.Create(obj["did"].AsString()) : null;
    }

    [JsonPropertyName("$type")]
    public string? Type { get; }

    public string? Uri { get; }

    /// <summary>
    /// Gets a <see cref="Did"/> of the actor.
    /// </summary>
    public AtDid? Did { get; }
}