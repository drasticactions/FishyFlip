// <copyright file="CreateFollowRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record for creating a follow record.
/// </summary>
/// <param name="Collection">The collection name.</param>
/// <param name="Repo">The repository name.</param>
/// <param name="Record">The follow record.</param>
public record CreateFollowRecord(string Collection, string Repo, FollowRecord Record);