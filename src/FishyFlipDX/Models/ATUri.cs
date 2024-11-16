// <copyright file="ATUri.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an ATUri object that parses and manipulates AT URIs.
/// </summary>
public class ATUri
{
    private static readonly Regex AtpUriRegex = new Regex(
        @"^(at:\/\/)?((?:did:[a-z0-9:%-]+)|(?:[a-z][a-z0-9.:-]*))(\/[^?#\s]*)?(\?[^#\s]+)?(#[^\s]+)?$",
        RegexOptions.IgnoreCase);

    private string host;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATUri"/> class.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <exception cref="FormatException">Thrown if format is invalid.</exception>
    public ATUri(string uri)
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

    /// <summary>
    /// Gets the hash value.
    /// </summary>
    public string Hash { get; private set; }

    /// <summary>
    /// Gets the pathname value.
    /// </summary>
    public string Pathname { get; private set; }

    /// <summary>
    /// Gets the protocol value.
    /// </summary>
    public string Protocol => "at:";

    /// <summary>
    /// Gets the origin value.
    /// </summary>
    public string Origin => $"at://{this.host}";

    /// <summary>
    /// Gets the hostname value.
    /// </summary>
    public string Hostname => this.host;

    /// <summary>
    /// Gets the associated AT DID.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the associated AT handle.
    /// </summary>
    public ATHandle? Handle { get; }

    /// <summary>
    /// Gets the identity value.
    /// </summary>
    public string? Identity => this.Did?.ToString() ?? this.Handle?.ToString();

    /// <summary>
    /// Gets the collection value.
    /// </summary>
    public string Collection => this.Pathname.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];

    /// <summary>
    /// Gets the rkey value.
    /// </summary>
    public string Rkey => this.Pathname?.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ElementAtOrDefault(1) ?? string.Empty;

    /// <summary>
    /// Gets the href value.
    /// </summary>
    public string Href => this.ToString();

    /// <summary>
    /// Creates a new instance of the <see cref="ATUri"/> class.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns>A new instance of the <see cref="ATUri"/> class.</returns>
    public static ATUri Create(string uri)
        => new ATUri(uri);

    /// <summary>
    /// Creates a new instance of the <see cref="ATUri"/> class.
    /// </summary>
    /// <param name="uri">String based URI.</param>
    /// <param name="atUri">A new instance of the <see cref="ATUri"/> class.</param>
    /// <returns>Bool if ATUri is valid.</returns>
    public static bool TryCreate(string uri, out ATUri? atUri)
    {
        try
        {
            atUri = new ATUri(uri);
            return true;
        }
        catch (Exception)
        {
            atUri = null;
            return false;
        }
    }

    /// <summary>
    /// Checks if the AT URI is valid.
    /// </summary>
    /// <param name="uri">Uri value.</param>
    /// <returns>Bool.</returns>
    public static bool IsValid(string uri)
    {
        Match match = AtpUriRegex.Match(uri);

        if (match == null || !match.Success)
        {
            return false;
        }

        return match.Success;
    }

    /// <summary>
    /// Returns a string representation of the AT URI.
    /// </summary>
    /// <returns>A string representation of the AT URI.</returns>
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
