// <copyright file="PostView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record PostView(
    string Uri,
    string Cid,
    int ReplyCount,
    int RepostCount,
    int LikeCount,
    Embed Embed,
    Profile Author,
    PostRecord Record,
    DateTime IndexedAt,
    IReadOnlyList<Label> Labels);