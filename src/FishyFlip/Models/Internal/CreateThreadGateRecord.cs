// <copyright file="CreateThreadGateRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record used for creating a thread gate.
/// </summary>
internal record CreateThreadGateRecord(string Collection, string Repo, ThreadGate Record, string? Rkey = null, string? SwapCommit = null);