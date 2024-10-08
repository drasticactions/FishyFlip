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
    /// <param name="record">The record to be embedded. Can be null.</param>
    /// <param name="embed">The media to be embedded. Can be null.</param>
    [JsonConstructor]
    public RecordWithMediaEmbed(RecordEmbed? record, Embed? embed)
    {
        this.Record = record;
        this.Embed = embed;
        this.Type = Constants.EmbedTypes.RecordWithMedia;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RecordWithMediaEmbed"/> class.
    /// </summary>
    /// <param name="record">The record to be embedded. Can be null.</param>
    /// <param name="media">The media to be embedded. Can be null.</param>
    public RecordWithMediaEmbed(CBORObject record, CBORObject media)
    {
        this.Type = Constants.EmbedTypes.RecordWithMedia;
        this.Record = new RecordEmbed(record["record"]);
        var type = media["$type"]?.AsString() ?? string.Empty;
        switch (type)
        {
            case Constants.EmbedTypes.Images:
                this.Embed = new ImagesEmbed(media["images"]);
                break;
        }
    }

    /// <summary>
    /// Gets the record to be embedded.
    /// </summary>
    public RecordEmbed? Record { get; }

    /// <summary>
    /// Gets the images to be embedded.
    /// </summary>
    public Embed? Embed { get; }
}
