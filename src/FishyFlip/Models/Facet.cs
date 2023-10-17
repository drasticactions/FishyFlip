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

    public Facet(FacetIndex index, FacetFeature feature)
    {
        this.Index = index;
        this.Features = new FacetFeature[] { feature };
    }

    public Facet(CBORObject obj)
    {
        this.Index = obj["index"] is not null ? new FacetIndex(obj["index"]) : null;
        this.Features = obj["features"] is not null ? obj["features"].Values.Select(n => new FacetFeature(n)).ToArray() : null;
    }

    public FacetIndex? Index { get; }

    public FacetFeature[]? Features { get; }

    public static Facet CreateFacetLink(int start, int end, string uri)
        => new(new FacetIndex(start, end), new FacetFeature[] { FacetFeature.CreateLink(uri) });

    public static Facet CreateFacetHashtag(int start, int end, string hashtag)
        => new(new FacetIndex(start, end), new FacetFeature[] { FacetFeature.CreateHashtag(hashtag) });

    public static Facet CreateFacetMention(int start, int end, ATDid mention)
        => new(new FacetIndex(start, end), new FacetFeature[] { FacetFeature.CreateMention(mention) });
}
