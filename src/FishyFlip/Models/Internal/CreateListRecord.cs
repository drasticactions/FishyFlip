// <copyright file="CreateListRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using static FishyFlip.Constants;

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record used for creating a list.
/// </summary>
internal record CreateListRecord(string Collection, string Repo, ListRecordInternal Record, string? Rkey = null, string? SwapCommit = null);
