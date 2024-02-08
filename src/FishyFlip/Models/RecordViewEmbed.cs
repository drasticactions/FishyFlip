// <copyright file="RecordViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed for a record view.
/// </summary>
public class RecordViewEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecordViewEmbed"/> class.
    /// </summary>
    /// <param name="record">The post view embed record.</param>
    [JsonConstructor]
    public RecordViewEmbed(PostViewEmbed record)
    {
        this.Record = record;
        var embed = record.Embeds?.FirstOrDefault();
        this.Post = new PostView(record.Uri, record.Cid, record.ReplyCount, record.RepostCount, record.LikeCount, embed, record.Author, record.Value, record.IndexedAt, record.Label);
        this.Type = Constants.EmbedTypes.RecordView;
    }

    /// <summary>
    /// Gets the post view embed record.
    /// </summary>
    public PostViewEmbed Record { get; }

    /// <summary>
    /// Gets the post view.
    /// </summary>
    [JsonIgnore]
    public PostView Post { get; }
}