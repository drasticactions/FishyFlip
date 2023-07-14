// <copyright file="ImageEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ImageEmbed
{
    [JsonConstructor]
    public ImageEmbed(Image? image, string? alt)
    {
        this.Image = image;
        this.Alt = alt;
    }

    public ImageEmbed(CBORObject obj)
    {
        this.Alt = obj["alt"]?.AsString() ?? string.Empty;
        var image = obj["image"];
        this.Image = image is not null ? new Image(image) : null;
    }

    public string? Alt { get; }

    public Image? Image { get; }
}
