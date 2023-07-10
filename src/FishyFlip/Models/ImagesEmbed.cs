// <copyright file="ImagesEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ImagesEmbed : Embed
{
    public ImagesEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Images;
    }
}
