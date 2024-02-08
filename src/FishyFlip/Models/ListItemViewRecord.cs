// <copyright file="ListItemViewRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record that holds a collection of <see cref="ListItemView"/> items, a <see cref="ListView"/>,
/// and an optional cursor.
/// </summary>
public record ListItemViewRecord(ListItemView[] Items, ListView List, string? Cursor);