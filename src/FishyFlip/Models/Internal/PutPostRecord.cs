// <copyright file="PutPostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record PutPostRecord(string Collection, string Repo, Post Record, string? Rkey = null, string? SwapRecord = null, string? SwapCommit = null);
