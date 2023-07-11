// <copyright file="ImagesEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ImagesEmbed : Embed
{
    public ImagesEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Images;
        this.Images = obj.Values.Select(n => new ImageEmbed(n)).ToArray();
    }

    public ImageEmbed[]? Images { get; }
}

public class ImageEmbed : ATRecord
{
    public ImageEmbed(CBORObject obj)
    {
        this.Alt = obj["alt"]?.AsString() ?? string.Empty;
        var image = obj["image"];
        this.Image = image is not null ? new Image(image) : null;
    }

    public string Alt { get; }

    public Image? Image { get; }
}

public class Image : ATRecord
{
    public Image(CBORObject image)
    {
        this.Type = image["$type"]?.AsString() ?? string.Empty;
        this.Size = image["size"]?.AsInt32() ?? 0;
        this.MimeType = image["mimeType"]?.AsString() ?? string.Empty;
        var refObj = image["ref"];
        if (refObj is not null)
        {
            this.Ref = Cid.Read(refObj.GetByteString());
        }
    }

    public string MimeType { get; }

    public int Size { get; }

    public Cid? Ref { get; }
}
