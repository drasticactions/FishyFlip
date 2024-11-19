// <copyright file="MarkdownPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.App.Bsky.Richtext;

namespace FishyFlip.Models;

/// <summary>
/// Markdown Post.
/// Converts a markdown string into a post.
/// </summary>
public class MarkdownPost
{
    private MarkdownPost(string markdown)
    {
        this.OriginalMarkdown = markdown;
        this.Post = StripMarkdown(markdown);
        var links = ExtractLinks(markdown);
        var facets = GetFacetLinks(links);
        var hashtagFacets = Facet.ForHashtags(this.Post);
        this.Facets = facets.Concat(hashtagFacets).ToArray();
    }

    /// <summary>
    /// Gets the post content, with no markdown.
    /// </summary>
    public string Post { get; private set; }

    /// <summary>
    /// Gets the original markdown.
    /// </summary>
    public string OriginalMarkdown { get; private set; }

    /// <summary>
    /// Gets the facets in the post.
    /// </summary>
    public Facet[] Facets { get; private set; }

    /// <summary>
    /// Parse a markdown string into a post.
    /// </summary>
    /// <param name="markdown">Markdown.</param>
    /// <returns><see cref="MarkdownPost"/>.</returns>
    public static MarkdownPost Parse(string markdown)
    {
        return new MarkdownPost(markdown);
    }

    private static Facet[] GetFacetLinks(MarkdownLink[] links)
    {
        if (links == null || links.Length == 0)
        {
            return Array.Empty<Facet>();
        }

        var facets = new List<Facet>();
        foreach (var link in links)
        {
            if (Uri.TryCreate(link.Url, UriKind.Absolute, out Uri? uri))
            {
                // If link is an did:pcl, create a mention facet
                if (uri.Scheme == "did")
                {
                    if (ATDid.TryCreate(uri.ToString(), out ATDid? did))
                    {
                        facets.Add(Facet.CreateFacetMention(link.StartIndex, link.EndIndex, did!));
                    }
                }
                else
                {
                    facets.Add(Facet.CreateFacetLink(link.StartIndex, link.EndIndex, uri.ToString()));
                }
            }
        }

        return facets.ToArray();
    }

    private static string StripMarkdown(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
        {
            return string.Empty;
        }

        // Headers
        markdown = Regex.Replace(markdown, @"^#+\s*", string.Empty, RegexOptions.Multiline);

        // Bold and Italic
        markdown = Regex.Replace(markdown, @"[*_]{1,3}(.*?)[*_]{1,3}", "$1");

        // Code blocks with language specification
        markdown = Regex.Replace(markdown, @"```[\w-]*\n(.*?)\n```", "$1", RegexOptions.Singleline);

        // Inline code
        markdown = Regex.Replace(markdown, @"`([^`]+)`", "$1");

        // Links [text](url)
        markdown = Regex.Replace(markdown, @"\[([^\]]+)\]\([^\)]+\)", "$1");

        // Images ![alt](url)
        markdown = Regex.Replace(markdown, @"!\[[^\]]*\]\([^\)]+\)", string.Empty);

        // Blockquotes
        markdown = Regex.Replace(markdown, @"^\s*>\s*", string.Empty, RegexOptions.Multiline);

        // Unordered lists
        markdown = Regex.Replace(markdown, @"^\s*[-*+]\s+", string.Empty, RegexOptions.Multiline);

        // Ordered lists
        markdown = Regex.Replace(markdown, @"^\s*\d+\.\s+", string.Empty, RegexOptions.Multiline);

        // Horizontal rules
        markdown = Regex.Replace(markdown, @"^\s*[-*_]{3,}\s*$", string.Empty, RegexOptions.Multiline);

        // Strikethrough
        markdown = Regex.Replace(markdown, @"~~(.*?)~~", "$1");

        // Clean up extra whitespace
        markdown = Regex.Replace(markdown, @"\n{3,}", "\n\n");
        markdown = markdown.Trim();

        return markdown;
    }

    private static MarkdownLink[] ExtractLinks(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
        {
            return Array.Empty<MarkdownLink>();
        }

        var links = new List<MarkdownLink>();
        var strippedMarkdown = StripMarkdown(markdown);

        // Regular expression to match Markdown links, excluding image links
        var linkRegex = new Regex(@"(?<!!)\[([^\]]+)\]\(([^\)]+)\)");

        var matches = linkRegex.Matches(markdown);

        foreach (Match match in matches)
        {
            var text = match.Groups[1].Value;
            var url = match.Groups[2].Value;

            // Find where this text appears in the final stripped version
            var textStartIndex = strippedMarkdown.IndexOf(text);

            // Create the link object with indices based on where the text would appear
            // in the fully stripped version
            links.Add(new MarkdownLink(
                text: text,
                url: url,
                startIndex: textStartIndex,
                endIndex: textStartIndex + text.Length));
        }

        return links.ToArray();
    }

    private class MarkdownLink
    {
        public MarkdownLink(string text, string url, int startIndex, int endIndex)
        {
            this.Text = text;
            this.Url = url;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
        }

        /// <summary>
        /// Gets the text of the link.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the url of the link.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets the start index of the link.
        /// </summary>
        public int StartIndex { get; }

        /// <summary>
        /// Gets the end index of the link.
        /// </summary>
        public int EndIndex { get; }
    }
}