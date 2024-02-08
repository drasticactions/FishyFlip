// <copyright file="ImagesEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed containing images.
/// </summary>
public class ImagesEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImagesEmbed"/> class with the specified images.
    /// </summary>
    /// <param name="images">The images to be included in the embed.</param>
    [JsonConstructor]
    public ImagesEmbed(ImageEmbed[]? images)
    {
        this.Images = images;
        this.Type = Constants.EmbedTypes.Images;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImagesEmbed"/> class with a single image and optional alt text.
    /// </summary>
    /// <param name="image">The image to be included in the embed.</param>
    /// <param name="alt">The alternative text for the image.</param>
    public ImagesEmbed(Image image, string? alt = default)
    {
        this.Images = new ImageEmbed[] { new(image, alt) };
        this.Type = Constants.EmbedTypes.Images;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImagesEmbed"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the embed.</param>
    public ImagesEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Images;
        this.Images = obj.Values.Select(n => new ImageEmbed(n)).ToArray();
    }

    /// <summary>
    /// Gets the images included in the embed.
    /// </summary>
    public ImageEmbed[]? Images { get; }
}
