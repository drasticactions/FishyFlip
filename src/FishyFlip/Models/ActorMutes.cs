// <copyright file="ActorMutes.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a collection of actor mutes.
/// </summary>
public record ActorMutes(FeedProfile[]? Mutes, string? Cursor);