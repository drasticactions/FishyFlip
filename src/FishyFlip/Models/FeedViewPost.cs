// <copyright file="FeedViewPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record FeedViewPost(PostView Post, ReplyRef? Ref, ReasonRepost? Reason);