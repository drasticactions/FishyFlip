// <copyright file="ThreadView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ThreadView : ATRecord
{
    [JsonConstructor]
    public ThreadView(PostView? post, ThreadView[] replies, string type)
    {
        this.Post = post;
        this.Replies = replies;
        this.Type = type;
    }

    public PostView Post { get; }

    public ThreadView[]? Replies { get; }
}