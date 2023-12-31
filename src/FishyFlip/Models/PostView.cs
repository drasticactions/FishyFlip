﻿// <copyright file="PostView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record PostView(
    ATUri Uri,
    Cid Cid,
    int ReplyCount,
    int RepostCount,
    int LikeCount,
    Embed Embed,
    FeedProfile Author,
    Post? Record,
    DateTime IndexedAt,
    IReadOnlyList<Label> Label);
