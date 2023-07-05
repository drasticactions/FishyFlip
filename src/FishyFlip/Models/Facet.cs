// <copyright file="Facet.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// See <see cref="Link" /> and <see cref="Mention"/>.
/// </summary>
public class Facet
{
    public Facet(ByteSlice index)
    {
        this.Index = index;
        this.Features = new List<FacetFeature>();
    }

    /// <summary>
    /// Gets the start and end of the the facet in the enriched text.
    /// </summary>
    public ByteSlice Index { get; }

    /// <summary>
    /// Gets the features of the Facet.
    /// </summary>
    public List<FacetFeature> Features { get; }

    public void AddFeature(FacetFeature feature)
        => this.Features.Add(feature);
}