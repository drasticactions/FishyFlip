// <copyright file="Blob.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Models;

/// <summary>
/// Represents a blob record.
/// </summary>
public class Blob
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Blob"/> class.
    /// </summary>
    public Blob()
    {
    }

    public Blob(CBORObject obj)
    {
        this.MimeType = obj["mimeType"].AsString();
        this.Size = obj["size"].AsInt32();
        this.Ref = new ATLinkRef(obj["ref"]);
    }

    /// <summary>
    /// Gets or sets the MIME type of the blob.
    /// </summary>
    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }

    /// <summary>
    /// Gets or sets the size of the blob in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets the reference to the blob.
    /// </summary>
    [JsonPropertyName("ref")]
    public ATLinkRef? Ref { get; set; }
}