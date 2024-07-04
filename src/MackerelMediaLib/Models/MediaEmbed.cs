// <copyright file="MediaEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MackerelMediaLib.Models;

/// <summary>
/// Represents an image embedded in a document.
/// </summary>
public class MediaEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MediaEmbed"/> class.
    /// </summary>
    /// <param name="media">The image to embed.</param>
    /// <param name="alt">The alternative text for the image.</param>
    [JsonConstructor]
    public MediaEmbed(Media? media, string? alt)
        : base(Constants.MackerelMediaTypes.MediaEmbed)
    {
        this.Media = media;
        this.Alt = alt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaEmbed"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the image embed.</param>
    public MediaEmbed(CBORObject obj)
        : base(Constants.MackerelMediaTypes.MediaEmbed)
    {
        this.Alt = obj["alt"]?.AsString() ?? string.Empty;
        var media = obj["image"];
        this.Media = media is not null ? new Media(media) : null;
    }

    /// <summary>
    /// Gets the alternative text for the media.
    /// </summary>
    public string? Alt { get; }

    /// <summary>
    /// Gets the media embedded in the document.
    /// </summary>
    public Media? Media { get; }
}