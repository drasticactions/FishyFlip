// <copyright file="CreateLikeRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record for creating a like.
/// </summary>
public record CreateLikeRecord(string Collection, string Repo, LikeRecord Record);