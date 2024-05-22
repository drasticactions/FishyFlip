// <copyright file="Conversation.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Bluesky Conversation.
/// </summary>
public record Conversation(string Id, string Rev, int UnreadCount, bool Muted);