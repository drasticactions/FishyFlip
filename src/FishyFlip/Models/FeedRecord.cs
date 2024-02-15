// <copyright file="FeedRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a feed record.
/// </summary>
/// <param name="Uri">The URI of the feed record.</param>
/// <param name="Did">The DID of the feed record.</param>
/// <param name="Avatar">The avatar of the feed record.</param>
/// <param name="Creator">The creator of the feed record.</param>
/// <param name="ATCid">The CID of the feed record.</param>
/// <param name="DisplayName">The display name of the feed record.</param>
/// <param name="Viewer">The viewer of the feed record.</param>
/// <param name="LikeCount">The number of likes of the feed record.</param>
/// <param name="IndexedAt">The date and time when the feed record was indexed.</param>
/// <param name="Description">The description of the feed record.</param>
public record FeedRecord(ATUri Uri, ATDid Did, string Avatar, FeedCreator Creator, ATCid Cid, string DisplayName, Viewer Viewer, int LikeCount, DateTime IndexedAt, string Description)
{
}
