// <copyright file="Video.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an video with its MIME type, size, and type.
/// </summary>
public class Video : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Video"/> class.
    /// </summary>
    /// <param name="mimeType">The MIME type of the image.</param>
    /// <param name="size">The size of the image in bytes.</param>
    /// <param name="type">The type of the image.</param>
    [JsonConstructor]
    public Video(string? mimeType, int size, string? type)
        : base(type)
    {
        this.MimeType = mimeType;
        this.Size = size;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Video"/> class from a CBOR object.
    /// </summary>
    /// <param name="image">The CBOR object representing the image.</param>
    public Video(CBORObject image)
    {
        this.Type = image["$type"]?.AsString() ?? string.Empty;
        this.Size = image["size"]?.AsInt32() ?? 0;
        this.MimeType = image["mimeType"]?.AsString() ?? string.Empty;
        var refObj = image["ref"];
        if (refObj is not null)
        {
            this.Ref = new VideoRef(image["ref"]);
        }
    }

    /// <summary>
    /// Gets the MIME type of the video.
    /// </summary>
    public string? MimeType { get; }

    /// <summary>
    /// Gets the size of the video in bytes.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Gets or sets the reference to the video.
    /// </summary>
    [JsonPropertyName("ref")]
    public VideoRef? Ref { get; set; }
}