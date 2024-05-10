// <copyright file="ATRecordWrapper.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;

namespace SampleApp.Models;

public class ATRecordWrapper(ATRecord record)
{
    public ATRecord Record => record;
}

public class PostWrapper : ATRecordWrapper
{
    public PostWrapper(Post record)
        : base(record)
    {
    }

    public Post Post => (Post)this.Record;
}

public class LikeWrapper : ATRecordWrapper
{
    public LikeWrapper(Like record)
        : base(record)
    {
    }

    public Like Like => (Like)this.Record;
}