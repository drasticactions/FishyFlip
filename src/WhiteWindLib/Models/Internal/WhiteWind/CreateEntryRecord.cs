// <copyright file="CreateEntryRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace WhiteWindLib.Models.Internal.WhiteWind;

/// <summary>
/// Represents a record used for creating a entry.
/// </summary>
internal record CreateEntryRecord(string Collection, string Repo, Models.WhiteWind.Entry Record, string? Rkey = null, bool Validate = false);