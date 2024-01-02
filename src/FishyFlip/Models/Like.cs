// <copyright file="Like.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Like : ATRecord
{
    [JsonConstructor]
    public Like(Subject? subject, DateTime? createdAt, string? type)
        : base(type)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt;
    }

    public Like(CBORObject obj, ILogger? logger = default)
    {
        var cid = obj["subject"]["cid"].ToCid(logger);
        var uri = new ATUri(obj["subject"]["uri"].AsString());
        this.Subject = new Subject(cid, uri);
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.FeedType.Like;
    }

    public Subject? Subject { get; }

    public DateTime? CreatedAt { get; }
}