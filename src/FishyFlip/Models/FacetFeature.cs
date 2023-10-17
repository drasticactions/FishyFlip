// <copyright file="FacetFeature.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class FacetFeature
{
    [JsonConstructor]
    public FacetFeature(string? type, string? uri, string? tag, ATDid? did)
    {
        this.Type = type;
        this.Uri = uri;
        this.Tag = tag;
        this.Did = did;
    }

    public FacetFeature(CBORObject obj)
    {
        this.Type = obj["$type"].AsString();
        this.Uri = obj["uri"]?.AsString();
        this.Tag = obj["tag"]?.AsString();
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
    }

    [JsonPropertyName("$type")]
    public string? Type { get; }

    public string? Uri { get; }

    public string? Tag { get; }

    /// <summary>
    /// Gets a <see cref="Did"/> of the actor.
    /// </summary>
    public ATDid? Did { get; }

    public static FacetFeature CreateLink(string uri)
        => new(Constants.FacetTypes.Link, uri, null, null);

    public static FacetFeature CreateMention(ATDid mention)
        => new(Constants.FacetTypes.Mention, null, null, mention);

    public static FacetFeature CreateHashtag(string tag)
        => new(Constants.FacetTypes.Tag, null, tag, null);
}