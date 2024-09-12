// <copyright file="CaptionBlob.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a caption with its MIME type, size, and type.
/// </summary>
public class CaptionBlob : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CaptionBlob"/> class.
    /// </summary>
    /// <param name="mimeType">The MIME type of the image.</param>
    /// <param name="size">The size of the caption in bytes.</param>
    [JsonConstructor]
    public CaptionBlob(string? mimeType, int size)
        : base("blob")
    {
        this.MimeType = mimeType;
        this.Size = size;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CaptionBlob"/> class from a CBOR object.
    /// </summary>
    /// <param name="caption">The CBOR object representing the image.</param>
    public CaptionBlob(CBORObject caption)
    {
        this.Type = caption["$type"]?.AsString() ?? string.Empty;
        this.Size = caption["size"]?.AsInt32() ?? 0;
        this.MimeType = caption["mimeType"]?.AsString() ?? string.Empty;
        var refObj = caption["ref"];
        if (refObj is not null)
        {
            this.Ref = new CaptionRef(caption["ref"]);
        }
    }

    /// <summary>
    /// Gets the MIME type of the image.
    /// </summary>
    public string? MimeType { get; }

    /// <summary>
    /// Gets the size of the image in bytes.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Gets or sets the reference to the image.
    /// </summary>
    [JsonPropertyName("ref")]
    public CaptionRef? Ref { get; set; }
}