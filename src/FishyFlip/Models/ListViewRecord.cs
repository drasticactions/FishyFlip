// <copyright file="ListViewRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record containing an array of ListView objects and a cursor.
/// </summary>
public record ListViewRecord(ListView[] Lists, string? Cursor);
