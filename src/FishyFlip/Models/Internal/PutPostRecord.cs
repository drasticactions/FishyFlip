// <copyright file="PutPostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record used for updating or inserting a post in a collection and repository.
/// </summary>
public record PutPostRecord(string Collection, string Repo, Post Record, string? Rkey = null, string? SwapRecord = null, string? SwapCommit = null);
