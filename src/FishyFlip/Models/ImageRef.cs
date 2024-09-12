// <copyright file="ImageRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reference to an image.
/// </summary>
public class ImageRef
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageRef"/> class with the specified link.
    /// </summary>
    /// <param name="link">The link to the image.</param>
    [JsonConstructor]
    public ImageRef(ATCid? link)
    {
        this.Link = link;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageRef"/> class with the specified CBOR object.
    /// </summary>
    /// <param name="image">The CBOR object representing the image.</param>
    public ImageRef(CBORObject image)
    {
        switch (image.Type)
        {
            case CBORType.ByteString:
                var cid = image.GetByteString();
                this.Link = Cid.Read(cid);
                break;
            case CBORType.TextString:
                this.Link = Cid.Decode(image.AsString());
                break;
        }
    }

    /// <summary>
    /// Gets or sets the link to the image.
    /// </summary>
    [JsonPropertyName("$link")]
    public ATCid? Link { get; set; }
}