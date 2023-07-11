// <copyright file="PostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class PostRecord : ATFeedTypeAPI
{
    [JsonConstructor]
    public PostRecord(Post? value, AtUri? uri, Cid? cid)
        : base(uri, cid)
    {
        this.Value = value;
    }

    public Post? Value { get; }
}
