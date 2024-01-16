// <copyright file="Repost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Repost : ATRecord
{
    [JsonConstructor]
    public Repost(Cid? cid, ATUri? uri, DateTime? createdAt, string? type)
        : base(type)
    {
        this.Cid = cid;
        this.Uri = uri;
        this.CreatedAt = createdAt;
    }

    public Repost(CBORObject obj, ILogger? logger = default)
    {
        this.Cid = obj["subject"]["cid"].ToCid(logger);
        this.Uri = new ATUri(obj["subject"]["uri"].AsString());
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.FeedType.Repost;
    }

    public Cid? Cid { get; }

    public ATUri? Uri { get; }

    public DateTime? CreatedAt { get; }
}