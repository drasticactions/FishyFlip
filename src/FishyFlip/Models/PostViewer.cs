// <copyright file="PostViewer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Metadata about the requesting account's relationship with the subject content. Only has meaningful content for authed requests.
/// </summary>
public record PostViewer(
    bool ThreadMuted,
    bool ReplyDisabled,
    bool EmbeddingDisabled,
    bool Pinned,
    ATUri? Like,
    ATUri? Repost);