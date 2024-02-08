// <copyright file="RecordRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reference to a record.
/// </summary>
public record RecordRef(Cid Cid, ATUri Uri);