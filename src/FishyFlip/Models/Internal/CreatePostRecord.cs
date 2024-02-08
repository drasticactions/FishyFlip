// <copyright file="CreatePostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record used for creating a post.
/// </summary>
internal record CreatePostRecord(string Collection, string Repo, Post Record, string? Rkey = null, string? SwapCommit = null);