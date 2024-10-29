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

    /// <summary>
    /// Creates an array of facets with link features for the URIs in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForUris(string post)
    {
        var facets = new List<Facet>();
        var matches = Regex.Matches(post, @"(https?://[^\s]+)");
        foreach (Match match in matches)
        {
            var start = match.Index;
            var end = match.Index + match.Length;
            var uri = match.Value;
            facets.Add(CreateFacetLink(start, end, uri));
        }

        return facets.ToArray();
    }

    /// <summary>
    /// Creates an array of facets with link features for the URIs in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <param name="baseText">Text to embed with link.</param>
    /// <param name="uri">Link Uri.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForUris(string post, string baseText, string uri)
    {
        var facets = new List<Facet>();
        var matches = Regex.Matches(post, baseText);
        foreach (Match match in matches)
        {
            var start = match.Index;
            var end = match.Index + match.Length;
            facets.Add(CreateFacetLink(start, end, uri));
        }

        return facets.ToArray();
    }

    /// <summary>
    /// Creates an array of facets with hashtag features for the hashtags in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForHashtags(string post)
    {
        var facets = new List<Facet>();

        // Match all hashtags in the post that are not part of a URL.
        var matches = Regex.Matches(post, @"(?<![@\w/])#(?!\s)[\w\u0080-\uFFFF]+");
        foreach (Match match in matches)
        {
            var start = match.Index;
            var end = match.Index + match.Length;
            var hashtag = match.Value;

            if (hashtag.StartsWith("#"))
            {
                hashtag = hashtag.Substring(1);
            }

            if (string.IsNullOrEmpty(hashtag))
            {
                continue;
            }

            facets.Add(CreateFacetHashtag(start, end, hashtag));
        }

        return facets.ToArray();
    }

    /// <summary>
    /// Creates an array of facets with mention features for the mentions in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <param name="actors">Array of actors profiles.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForMentions(string post, FacetActorIdentifier[] actors)
    {
        var facets = new List<Facet>();

        // Match all mentions in the post that are not part of a URL.
        var matches = Regex.Matches(post, @"@(?!http)[a-zA-Z0-9][-a-zA-Z0-9_.]{1,}");
        foreach (Match match in matches)
        {
            var start = match.Index;
            var end = match.Index + match.Length;
            var mention = match.Value;
            if (mention.StartsWith("@"))
            {
                mention = mention.Substring(1);
            }

            if (string.IsNullOrEmpty(mention))
            {
                continue;
            }

            var actor = actors.FirstOrDefault(n => n.Handle.ToString() == mention);
            if (actor?.Did is not null)
            {
                facets.Add(CreateFacetMention(start, end, actor.Did));
            }
        }

        return facets.ToArray();
    }

    /// <summary>
    /// Creates an array of facets with mention features for the mentions in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <param name="actors">Actor profiles.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForMentions(string post, ActorProfile[] actors)
    {
        var actorList = new List<FacetActorIdentifier>();
        foreach (var actor in actors)
        {
            if (actor.Handle is null)
            {
                continue;
            }

            if (!ATHandle.TryCreate(actor.Handle, out var atHandle))
            {
                continue;
            }

            if (atHandle is null || actor.Did is null)
            {
                continue;
            }

            actorList.Add(new FacetActorIdentifier(atHandle, actor.Did));
        }

        return ForMentions(post, actorList.ToArray());
    }

    /// <summary>
    /// Creates an array of facets with mention features for the mentions in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <param name="actors">Actor profiles.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForMentions(string post, FeedProfile[] actors)
    {
        var actorList = new List<FacetActorIdentifier>();
        foreach (var actor in actors)
        {
            if (actor.Handle is null)
            {
                continue;
            }

            if (!ATHandle.TryCreate(actor.Handle, out var atHandle))
            {
                continue;
            }

            if (atHandle is null || actor.Did is null)
            {
                continue;
            }

            actorList.Add(new FacetActorIdentifier(atHandle, actor.Did));
        }

        return ForMentions(post, actorList.ToArray());
    }

    /// <summary>
    /// Creates an array of facets with mention features for the mentions in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <param name="actor">Actor profiles.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForMentions(string post, FeedProfile actor)
        => ForMentions(post, new FeedProfile[] { actor });
}
