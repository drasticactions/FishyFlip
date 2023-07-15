// <copyright file="ListRecords.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record ListRecords(ListRecord[] Records, string? Cursor);

public record ListRecord(ATUri? Uri, Cid? Cid, ATRecord? Value);
