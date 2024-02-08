// <copyright file="Reply.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reply to a post.
/// </summary>
public record Reply(ReplyRef Root, ReplyRef Parent);
