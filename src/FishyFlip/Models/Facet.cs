// <copyright file="Facet.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Facet : ATRecord
{
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
    public FacetFeature(CBORObject obj)
    {
        this.Type = obj["$type"].AsString();
        this.Uri = obj["uri"]?.AsString();
    }

    public string Type { get; }

    public string? Uri { get; }
}