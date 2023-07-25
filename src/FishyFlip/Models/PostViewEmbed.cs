// <copyright file="PostViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record PostViewEmbed(
    string Uri,
    Cid Cid,
    int ReplyCount,
    int RepostCount,
    int LikeCount,
    FeedProfile Author,
    Embed[] Embeds,
    DateTime IndexedAt,
    IReadOnlyList<Label> Label,
    Post Value);