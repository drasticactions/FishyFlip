// <copyright file="ImageViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ImageViewEmbed : Embed
{
    public ImageViewEmbed(ImageView[] images)
    {
        this.Images = images;
        this.Type = Constants.EmbedTypes.ImageView;
    }

    public ImageView[] Images { get; }
}

public record ImageView(string Thumb, string Fullsize, string Alt);
