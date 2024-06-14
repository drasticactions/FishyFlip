// <copyright file="ListRecords.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a list of records.
/// </summary>
/// <typeparam name="T">The type of the records in the list.</typeparam>
public record ListRecords<T>(ListRecord<T>[] Records, string? Cursor);
