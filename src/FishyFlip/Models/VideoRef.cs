// <copyright file="VideoRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reference to an video.
/// </summary>
public class VideoRef
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoRef"/> class with the specified link.
    /// </summary>
    /// <param name="link">The link to the image.</param>
    [JsonConstructor]
    public VideoRef(ATCid? link)
    {
        this.Link = link;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoRef"/> class with the specified CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the video.</param>
    public VideoRef(CBORObject obj)
    {
        switch (obj.Type)
        {
            case CBORType.ByteString:
                var cid = obj.GetByteString();
                this.Link = Cid.Read(cid);
                break;
            case CBORType.TextString:
                this.Link = Cid.Decode(obj.AsString());
                break;
        }
    }

    /// <summary>
    /// Gets or sets the link to the image.
    /// </summary>
    [JsonPropertyName("$link")]
    public ATCid? Link { get; set; }
}