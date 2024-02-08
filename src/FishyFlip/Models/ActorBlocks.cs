// <copyright file="ActorBlocks.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a collection of actor blocks.
/// </summary>
public record ActorBlocks(FeedProfile[]? Blocks, string? Cursor);