// <copyright file="ListRecords.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a list of records.
/// </summary>
public record ListRecords(ListRecord[] Records, string? Cursor);
