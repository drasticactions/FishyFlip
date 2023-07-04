// <copyright file="AtUri.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.RegularExpressions;

namespace FishyFlip.Models;

public class AtUri
{
    private static readonly Regex atpUriRegex = new Regex(
        @"^(at:\/\/)?((?:did:[a-z0-9:%-]+)|(?:[a-z][a-z0-9.:-]*))(\/[^?#\s]*)?(\?[^#\s]+)?(#[^\s]+)?$",
        RegexOptions.IgnoreCase);
    private string host;

    public AtUri(string uri)
    {
        Match match = atpUriRegex.Match(uri);

        if (match == null || !match.Success)
        {
            throw new FormatException($"Invalid at uri: {uri}");
        }

        this.host = match.Groups[2].Value ?? string.Empty;
        this.Pathname = match.Groups[3].Value ?? string.Empty;
        this.Hash = match.Groups[5].Value ?? string.Empty;
    }

    public string Hash { get; private set; }

    public string Pathname { get; private set; }

    public string Protocol => "at:";

    public string Origin => $"at://{this.host}";

    public string Hostname => this.host;

    public string Collection => this.Pathname.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];

    public string Rkey => this.Pathname.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ElementAtOrDefault(1);

    public string Href => this.ToString();

    public string ToString()
    {
        var buffer = new System.Text.StringBuilder();
        buffer.Append("at://");
        buffer.Append(this.host);

        if (!this.Pathname.StartsWith("/"))
        {
            buffer.Append($"/{this.Pathname}");
        }
        else
        {
            buffer.Append(this.Pathname);
        }

        if (!string.IsNullOrEmpty(this.Hash) && !this.Hash.StartsWith("#"))
        {
            buffer.Append($"#{this.Hash}");
        }
        else
        {
            buffer.Append(this.Hash);
        }

        return buffer.ToString();
    }
}