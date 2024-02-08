// <copyright file="GeneratorView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a view of a generator.
/// </summary>
public record GeneratorView(ATUri Uri, Cid Cid, ATDid Did, string Avatar, int LikeCount, string DisplayName, string Description, ActorProfile Creator, Viewer Viewer, DateTime IndexedAt);
