// <copyright file="Facet.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a facet in the FishyFlip application.
/// </summary>
public class Facet : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Facet"/> class with the specified index and features.
    /// </summary>
    /// <param name="index">The index of the facet.</param>
    /// <param name="features">The features of the facet.</param>
    [JsonConstructor]
    public Facet(FacetIndex? index, FacetFeature[] features)
    {
        this.Index = index;
        this.Features = features;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Facet"/> class with the specified index and feature.
    /// </summary>
    /// <param name="index">The index of the facet.</param>
    /// <param name="feature">The feature of the facet.</param>
    public Facet(FacetIndex index, FacetFeature feature)
    {
        this.Index = index;
        this.Features = new FacetFeature[] { feature };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Facet"/> class from the specified CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the facet.</param>
    public Facet(CBORObject obj)
    {
        this.Index = obj["index"] is not null ? new FacetIndex(obj["index"]) : null;
        this.Features = obj["features"] is not null ? obj["features"].Values.Select(n => new FacetFeature(n)).ToArray() : null;
    }

    /// <summary>
    /// Gets the index of the facet.
    /// </summary>
    public FacetIndex? Index { get; }

    /// <summary>
    /// Gets the features of the facet.
    /// </summary>
    public FacetFeature[]? Features { get; }

    /// <summary>
    /// Creates a facet with a link feature.
    /// </summary>
    /// <param name="start">The start index of the link.</param>
    /// <param name="end">The end index of the link.</param>
    /// <param name="uri">The URI of the link.</param>
    /// <returns>The created facet.</returns>
    public static Facet CreateFacetLink(int start, int end, string uri)
        => new(new FacetIndex(start, end), new FacetFeature[] { FacetFeature.CreateLink(uri) });

    /// <summary>
    /// Creates a facet with a hashtag feature.
    /// </summary>
    /// <param name="start">The start index of the hashtag.</param>
    /// <param name="end">The end index of the hashtag.</param>
    /// <param name="hashtag">The hashtag value.</param>
    /// <returns>The created facet.</returns>
    public static Facet CreateFacetHashtag(int start, int end, string hashtag)
        => new(new FacetIndex(start, end), new FacetFeature[] { FacetFeature.CreateHashtag(hashtag) });

    /// <summary>
    /// Creates a facet with a mention feature.
    /// </summary>
    /// <param name="start">The start index of the mention.</param>
    /// <param name="end">The end index of the mention.</param>
    /// <param name="mention">The mention value.</param>
    /// <returns>The created facet.</returns>
    public static Facet CreateFacetMention(int start, int end, ATDid mention)
        => new(new FacetIndex(start, end), new FacetFeature[] { FacetFeature.CreateMention(mention) });
}
