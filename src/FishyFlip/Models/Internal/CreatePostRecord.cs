// <copyright file="CreatePostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

internal record CreatePostRecord(string Collection, string Repo, Post Record, string? Rkey = null, string? SwapCommit = null);