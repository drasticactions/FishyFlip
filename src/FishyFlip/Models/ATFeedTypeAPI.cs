// <copyright file="ATFeedTypeAPI.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public abstract class ATFeedTypeAPI
{
    [JsonConstructor]
    public ATFeedTypeAPI(AtUri? uri, Cid? cid)
    {
        this.Uri = uri;
        this.Cid = cid;
    }

    public AtUri? Uri { get; }

    public Cid? Cid { get; }
}
