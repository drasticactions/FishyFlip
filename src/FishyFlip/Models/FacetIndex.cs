// <copyright file="FacetIndex.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

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
