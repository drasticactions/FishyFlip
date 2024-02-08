// <copyright file="ImageViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed containing multiple images.
/// </summary>
public class ImageViewEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageViewEmbed"/> class.
    /// </summary>
    /// <param name="images">The array of images to be embedded.</param>
    public ImageViewEmbed(ImageView[] images)
    {
        this.Images = images;
        this.Type = Constants.EmbedTypes.ImageView;
    }

    /// <summary>
    /// Gets the array of images embedded in the ImageViewEmbed.
    /// </summary>
    public ImageView[] Images { get; }
}
