// <copyright file="CaptionRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reference to an caption.
/// </summary>
public class CaptionRef
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CaptionRef"/> class with the specified link.
    /// </summary>
    /// <param name="link">The link to the caption.</param>
    [JsonConstructor]
    public CaptionRef(ATCid? link)
    {
        this.Link = link;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CaptionRef"/> class with the specified CBOR object.
    /// </summary>
    /// <param name="caption">The CBOR object representing the caption.</param>
    public CaptionRef(CBORObject caption)
    {
        switch (caption.Type)
        {
            case CBORType.ByteString:
                var cid = caption.GetByteString();
                this.Link = Cid.Read(cid);
                break;
            case CBORType.TextString:
                this.Link = Cid.Decode(caption.AsString());
                break;
        }
    }

    /// <summary>
    /// Gets or sets the link to the caption.
    /// </summary>
    [JsonPropertyName("$link")]
    public ATCid? Link { get; set; }
}