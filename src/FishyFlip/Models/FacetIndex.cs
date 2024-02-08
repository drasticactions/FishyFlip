// <copyright file="FacetIndex.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an index of a facet in a data structure.
/// </summary>
public class FacetIndex
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FacetIndex"/> class with the specified byte start and byte end values.
    /// </summary>
    /// <param name="byteStart">The starting byte position of the facet.</param>
    /// <param name="byteEnd">The ending byte position of the facet.</param>
    [JsonConstructor]
    public FacetIndex(int byteStart, int byteEnd)
    {
        this.ByteStart = byteStart;
        this.ByteEnd = byteEnd;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacetIndex"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object containing the byte start and byte end values.</param>
    public FacetIndex(CBORObject obj)
    {
        this.ByteStart = obj["byteStart"].AsInt32();
        this.ByteEnd = obj["byteEnd"].AsInt32();
    }

    /// <summary>
    /// Gets the ending byte position of the facet.
    /// </summary>
    public int ByteEnd { get; }

    /// <summary>
    /// Gets the starting byte position of the facet.
    /// </summary>
    public int ByteStart { get; }
}
