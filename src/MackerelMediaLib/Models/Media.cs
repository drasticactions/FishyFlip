// <copyright file="Media.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MackerelMediaLib.Models;

/// <summary>
/// Media Record.
/// </summary>
public class Media : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Media"/> class.
    /// </summary>
    /// <param name="mimeType">The MIME type of the media.</param>
    /// <param name="size">The size of the image in bytes.</param>
    /// <param name="thumbnail">The thumbnail for the media.</param>
    [JsonConstructor]
    public Media(string? mimeType, int size, ImageEmbed? thumbnail = default)
        : base(Constants.MackerelMediaTypes.Media)
    {
        this.MimeType = mimeType;
        this.Size = size;
        this.Thumbnail = thumbnail;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Media"/> class from a CBOR object.
    /// </summary>
    /// <param name="image">The CBOR object representing the image.</param>
    public Media(CBORObject image)
     : base(Constants.MackerelMediaTypes.Media)
    {
        this.Size = image["size"]?.AsInt32() ?? 0;
        this.MimeType = image["mimeType"]?.AsString() ?? string.Empty;
        var refObj = image["ref"];
        if (refObj is not null)
        {
            this.Ref = new MediaRef(image["ref"]);
        }

        if (!image["thumbnail"]?.IsNull ?? false)
        {
            this.Thumbnail = new ImageEmbed(image["thumbnail"]);
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
    /// Gets the thumbnail, optional.
    /// </summary>
    public ImageEmbed? Thumbnail { get; }

    /// <summary>
    /// Gets or sets the reference to the media.
    /// </summary>
    [JsonPropertyName("ref")]
    public MediaRef? Ref { get; set; }
}