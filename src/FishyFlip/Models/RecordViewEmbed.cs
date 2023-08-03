// <copyright file="RecordViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class RecordViewEmbed : Embed
{
    public RecordViewEmbed(PostViewEmbed record)
    {
        this.Record = record;
        var embed = record.Embeds?.FirstOrDefault();
        this.Post = new PostView(record.Uri, record.Cid, record.ReplyCount, record.RepostCount, record.LikeCount, embed, record.Author,  record.Value, record.IndexedAt, record.Label);
        this.Type = Constants.EmbedTypes.RecordView;
    }

    public PostViewEmbed Record { get; }
    
    [JsonIgnore]
    public PostView Post { get; }
}