// <copyright file="Facet.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Lexicon.App.Bsky.Richtext;

/// <summary>
/// Facets.
/// </summary>
public partial class Facet
{
    /// <summary>
    /// Creates a facet with a link feature.
    /// </summary>
    /// <param name="start">The start index of the link.</param>
    /// <param name="end">The end index of the link.</param>
    /// <param name="uri">The URI of the link.</param>
    /// <returns>The created facet.</returns>
    public static Facet CreateFacetLink(int start, int end, string uri)
    {
        var facet = new Facet();
        var link = new Link();
        link.Uri = uri;
        facet.Features = new List<ATObject> { link };
        facet.Index = new ByteSlice();
        facet.Index.ByteStart = start;
        facet.Index.ByteEnd = end;
        return facet;
    }

    /// <summary>
    /// Creates a facet with a hashtag feature.
    /// </summary>
    /// <param name="start">The start index of the hashtag.</param>
    /// <param name="end">The end index of the hashtag.</param>
    /// <param name="hashtag">The hashtag value.</param>
    /// <returns>The created facet.</returns>
    public static Facet CreateFacetHashtag(int start, int end, string hashtag)
    {
        var facet = new Facet();
        var hashtagFeature = new Tag();
        hashtagFeature.TagValue = hashtag;
        facet.Features = new List<ATObject> { hashtagFeature };
        facet.Index = new ByteSlice();
        facet.Index.ByteStart = start;
        facet.Index.ByteEnd = end;
        return facet;
    }

    /// <summary>
    /// Creates a facet with a mention feature.
    /// </summary>
    /// <param name="start">The start index of the mention.</param>
    /// <param name="end">The end index of the mention.</param>
    /// <param name="mention">The mention value.</param>
    /// <returns>The created facet.</returns>
    public static Facet CreateFacetMention(int start, int end, ATDid mention)
    {
        var facet = new Facet();
        var mentionFeature = new Mention();
        mentionFeature.Did = mention;
        facet.Features = new List<ATObject> { mentionFeature };
        facet.Index = new ByteSlice();
        facet.Index.ByteStart = start;
        facet.Index.ByteEnd = end;
        return facet;
    }

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
    public static Facet[] ForMentions(string post, FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed[] actors)
    {
        var actorList = new List<FacetActorIdentifier>();
        foreach (var actor in actors)
        {
            if (actor.Handle is null)
            {
                continue;
            }

            if (actor.Handle is null || actor.Did is null)
            {
                continue;
            }

            actorList.Add(new FacetActorIdentifier(actor.Handle, actor.Did));
        }

        return ForMentions(post, actorList.ToArray());
    }

    /// <summary>
    /// Creates an array of facets with mention features for the mentions in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <param name="actors">Actor profiles.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForMentions(string post, FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic[] actors)
    {
        var actorList = new List<FacetActorIdentifier>();
        foreach (var actor in actors)
        {
            if (actor.Handle is null)
            {
                continue;
            }

            if (actor.Handle is null || actor.Did is null)
            {
                continue;
            }

            actorList.Add(new FacetActorIdentifier(actor.Handle, actor.Did));
        }

        return ForMentions(post, actorList.ToArray());
    }

    /// <summary>
    /// Creates an array of facets with mention features for the mentions in the specified post.
    /// </summary>
    /// <param name="post">Post text.</param>
    /// <param name="actor">Actor profiles.</param>
    /// <returns>Array of Facets.</returns>
    public static Facet[] ForMentions(string post, FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic actor)
        => ForMentions(post, new FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewBasic[] { actor });
}
