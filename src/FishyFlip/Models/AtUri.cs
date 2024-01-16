// <copyright file="ATUri.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ATUri
{
    private static readonly Regex AtpUriRegex = new Regex(
        @"^(at:\/\/)?((?:did:[a-z0-9:%-]+)|(?:[a-z][a-z0-9.:-]*))(\/[^?#\s]*)?(\?[^#\s]+)?(#[^\s]+)?$",
        RegexOptions.IgnoreCase);

    private string host;

    internal ATUri(string uri)
    {
        Match match = AtpUriRegex.Match(uri);

        if (match == null || !match.Success)
        {
            throw new FormatException($"Invalid at uri: {uri}");
        }

        this.host = match.Groups[2].Value ?? string.Empty;
        this.Pathname = match.Groups[3].Value ?? string.Empty;
        this.Hash = match.Groups[5].Value ?? string.Empty;
        this.Did = ATDid.Create(this);
        this.Handle = ATHandle.Create(this);
    }

    public string Hash { get; private set; }

    public string Pathname { get; private set; }

    public string Protocol => "at:";

    public string Origin => $"at://{this.host}";

    public string Hostname => this.host;

    public static ATUri Create(string uri)
        => new ATUri(uri);

    public ATDid? Did { get; }

    public ATHandle? Handle { get; }

    public string? Identity => this.Did?.ToString() ?? this.Handle?.ToString();

    public string Collection => this.Pathname.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];

    public string Rkey => this.Pathname?.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ElementAtOrDefault(1) ?? string.Empty;

    public string Href => this.ToString();

    public override string ToString()
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
