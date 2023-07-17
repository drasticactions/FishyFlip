// <copyright file="ActorRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ActorRecord : ATFeedTypeAPI
{
    [JsonConstructor]
    public ActorRecord(Profile? value, ATUri? uri, Cid? cid)
        : base(uri, cid)
    {
        this.Value = value;
    }

    public Profile? Value { get; }
}