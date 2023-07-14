// <copyright file="ImageRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ImageRef
{
    [JsonConstructor]
    public ImageRef(Cid? link)
    {
        this.Link = link;
    }

    public ImageRef(CBORObject image)
    {
        this.Link = Cid.Read(image.GetByteString());
    }

    [JsonPropertyName("$link")]
    public Cid? Link { get; set; }
}
