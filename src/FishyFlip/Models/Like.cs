// <copyright file="Like.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Like : ATRecord
{
    public Like(Cid cid, ATUri uri, DateTime createdAt)
    {
        this.Cid = cid;
        this.Uri = uri;
        this.CreatedAt = createdAt;
        this.Type = Constants.FeedType.Like;
    }

    public Like(CBORObject obj)
    {
        this.Cid = obj["subject"]["cid"].ToCid();
        this.Uri = new ATUri(obj["subject"]["uri"].AsString());
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.FeedType.Like;
    }

    public Cid? Cid { get; }

    public ATUri Uri { get; }

    public DateTime? CreatedAt { get; }
}