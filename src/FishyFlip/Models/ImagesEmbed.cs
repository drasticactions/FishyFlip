// <copyright file="ImagesEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ImagesEmbed : Embed
{
    [JsonConstructor]
    public ImagesEmbed(ImageEmbed[]? images)
    {
        this.Images = images;
        this.Type = Constants.EmbedTypes.Images;
    }

    public ImagesEmbed(Image image, string? alt = default)
    {
        this.Images = new ImageEmbed[] { new(image, alt) };
        this.Type = Constants.EmbedTypes.Images;
    }

    public ImagesEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Images;
        this.Images = obj.Values.Select(n => new ImageEmbed(n)).ToArray();
    }

    public ImageEmbed[]? Images { get; }
}
