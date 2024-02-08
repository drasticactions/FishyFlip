// <copyright file="FeedGeneratorRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a feed generator record.
/// </summary>
/// <param name="IsOnline">Indicates whether the generator is online.</param>
/// <param name="IsValid">Indicates whether the generator is valid.</param>
/// <param name="View">The feed record associated with the generator.</param>
public record FeedGeneratorRecord(bool IsOnline, bool IsValid, FeedRecord View);
