// <copyright file="RecordWithMediaEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Record with Media Embed.
/// </summary>
public class RecordWithMediaEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecordWithMediaEmbed"/> class.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    public RecordWithMediaEmbed(CBORObject record, CBORObject media)
    {
        this.Type = Constants.EmbedTypes.RecordWithMedia;
        this.Record = new RecordEmbed(record["record"]);
        var type = media["$type"]?.AsString() ?? string.Empty;
        switch (type)
        {
            case Constants.EmbedTypes.Images:
                this.Images = new ImagesEmbed(media["images"]);
                break;
        }
    }

    public RecordEmbed Record { get; }

    public ImagesEmbed? Images { get; }
}
