// <copyright file="FacetFeature.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools.Json;

namespace FishyFlip.Models;

/// <summary>
/// Represents a facet feature.
/// </summary>
public class FacetFeature
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FacetFeature"/> class.
    /// </summary>
    /// <param name="type">The type of the facet feature.</param>
    /// <param name="uri">The URI of the facet feature.</param>
    /// <param name="tag">The tag of the facet feature.</param>
    /// <param name="did">The DID (Decentralized Identifier) of the facet feature.</param>
    [JsonConstructor]
    public FacetFeature(string? type, string? uri, string? tag, ATDid? did)
    {
        this.Type = type;
        this.Uri = uri;
        this.Tag = tag;
        this.Did = did;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FacetFeature"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the facet feature.</param>
    public FacetFeature(CBORObject obj)
    {
        this.Type = obj["$type"].AsString();
        this.Uri = obj["uri"]?.AsString();
        this.Tag = obj["tag"]?.AsString();
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
    }

    /// <summary>
    /// Gets the type of the facet feature.
    /// </summary>
    [JsonPropertyName("$type")]
    public string? Type { get; }

    /// <summary>
    /// Gets the URI of the facet feature.
    /// </summary>
    public string? Uri { get; }

    /// <summary>
    /// Gets the tag of the facet feature.
    /// </summary>
    public string? Tag { get; }

    /// <summary>
    /// Gets the DID (Decentralized Identifier) of the facet feature.
    /// </summary>
    [JsonConverter(typeof(ATDidJsonConverter))]
    public ATDid? Did { get; }

    /// <summary>
    /// Creates a facet feature of type "Link" with the specified URI.
    /// </summary>
    /// <param name="uri">The URI of the facet feature.</param>
    /// <returns>A new instance of the <see cref="FacetFeature"/> class.</returns>
    public static FacetFeature CreateLink(string uri)
        => new(Lexicon.App.Bsky.Richtext.Link.RecordType, uri, null, null);

    /// <summary>
    /// Creates a facet feature of type "Mention" with the specified DID.
    /// </summary>
    /// <param name="mention">The DID (Decentralized Identifier) to mention.</param>
    /// <returns>A new instance of the <see cref="FacetFeature"/> class.</returns>
    public static FacetFeature CreateMention(ATDid mention)
        => new(Lexicon.App.Bsky.Richtext.Mention.RecordType, null, null, mention);

    /// <summary>
    /// Creates a facet feature of type "Tag" with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the facet feature.</param>
    /// <returns>A new instance of the <see cref="FacetFeature"/> class.</returns>
    public static FacetFeature CreateHashtag(string tag)
        => new(Lexicon.App.Bsky.Richtext.Tag.RecordType, null, tag, null);

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        return this.Type switch
        {
            Lexicon.App.Bsky.Richtext.Link.RecordType => $"Link: {this.Uri}",
            Lexicon.App.Bsky.Richtext.Mention.RecordType => $"Mention: {this.Did}",
            Lexicon.App.Bsky.Richtext.Tag.RecordType => $"Tag: {this.Tag}",
            _ => "Unknown",
        };
    }
}