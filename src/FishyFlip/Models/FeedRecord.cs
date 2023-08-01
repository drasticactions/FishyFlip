// <copyright file="FeedRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record FeedRecord(ATUri Uri, ATDid Did, string Avatar, FeedCreator Creator, Cid Cid, string DisplayName, Viewer Viewer, int LikeCount, DateTime IndexedAt, string Description)
{
}

public record FeedGeneratorRecord(bool IsOnline, bool IsValid, FeedRecord View);

public record FeedPostList(FeedPost[] Feed, string? Cursor);

public record FeedPost(PostView Post);

public record FeedCollection(FeedRecord[] Feeds);

public record FeedResultList(FeedRecord[] Feeds, string? Cursor);

public record FeedCreator(string Did,
    string Handle,
    string DisplayName,
    string Avatar,
    Viewer Viewer,
    IReadOnlyList<Label> Labels);
