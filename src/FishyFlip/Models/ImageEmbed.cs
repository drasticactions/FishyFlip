// <copyright file="ImageEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an image embedded in a document.
/// </summary>
public class ImageEmbed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageEmbed"/> class.
    /// </summary>
    /// <param name="image">The image to embed.</param>
    /// <param name="alt">The alternative text for the image.</param>
    [JsonConstructor]
    public ImageEmbed(Image? image, string? alt)
    {
        this.Image = image;
        this.Alt = alt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageEmbed"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the image embed.</param>
    public ImageEmbed(CBORObject obj)
    {
        this.Alt = obj["alt"]?.AsString() ?? string.Empty;
        var image = obj["image"];
        this.Image = image is not null ? new Image(image) : null;
    }

    /// <summary>
    /// Gets the alternative text for the image.
    /// </summary>
    public string? Alt { get; }

    /// <summary>
    /// Gets the image embedded in the document.
    /// </summary>
    public Image? Image { get; }
}
