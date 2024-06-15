// <copyright file="BlobRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models.Experimental.MackerelMedia;

namespace FishyFlip.Models;

/// <summary>
/// Represents a blob record.
/// </summary>
public class BlobRecord : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlobRecord"/> class.
    /// </summary>
    /// <param name="mimeType">The MIME type of the blob.</param>
    /// <param name="size">The size of the blob.</param>
    /// <param name="type">The type of the blob.</param>
    [JsonConstructor]
    public BlobRecord(string? mimeType, int size, string? type)
        : base(type)
    {
        this.MimeType = mimeType;
        this.Size = size;
    }

    /// <summary>
    /// Gets the MIME type of the blob.
    /// </summary>
    [JsonPropertyName("mimeType")]
    public string? MimeType { get; }

    /// <summary>
    /// Gets the size of the blob in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; }

    /// <summary>
    /// Gets or sets the reference to the blob.
    /// </summary>
    [JsonPropertyName("ref")]
    public ATLinkRef? Ref { get; set; }

    /// <summary>
    /// Converts the blob record to an image.
    /// </summary>
    /// <returns>An image object.</returns>
    public Image ToImage()
        => new(this.MimeType, this.Size, "blob")
        {
            Ref = new ImageRef(this.Ref?.Link),
        };

    /// <summary>
    /// Converts the blob record to media.
    /// </summary>
    /// <returns>An image object.</returns>
    public Media ToMedia()
        => new(this.MimeType, this.Size, "blob")
        {
            Ref = new MediaRef(this.Ref?.Link),
        };
}
