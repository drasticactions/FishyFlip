// <copyright file="ImagesEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ImagesEmbed : Embed
{
    [JsonConstructor]
    public ImagesEmbed(ImageEmbed[]? images, string? type)
        : base(type)
    {
        this.Images = images;
    }

    public ImagesEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Images;
        this.Images = obj.Values.Select(n => new ImageEmbed(n)).ToArray();
    }

    public ImageEmbed[]? Images { get; }
}

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

public class Image : ATRecord
{
    [JsonConstructor]
    public Image(string? mimeType, int size, string? type)
        : base(type)
    {
        this.MimeType = mimeType;
        this.Size = size;
    }

    public Image(CBORObject image)
    {
        this.Type = image["$type"]?.AsString() ?? string.Empty;
        this.Size = image["size"]?.AsInt32() ?? 0;
        this.MimeType = image["mimeType"]?.AsString() ?? string.Empty;
        var refObj = image["ref"];
        if (refObj is not null)
        {
            this.Ref = new ImageRef(image["ref"]);
        }
    }

    public string MimeType { get; }

    public int Size { get; }

    [JsonPropertyName("ref")]
    public ImageRef? Ref { get; set; }
}

public class ImageRef
{
    [JsonConstructor]
    public ImageRef(Cid? link)
    {
        Link = link;
    }

    public ImageRef(CBORObject image)
    {
        this.Link = Cid.Read(image.GetByteString());
    }

    [JsonPropertyName("$link")]
    public Cid? Link { get; set; }
}
