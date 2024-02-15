// <copyright file="ListRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record that contains information about a list.
/// </summary>
/// <param name="Uri">The URI of the list.</param>
/// <param name="ATCid">The CID of the list.</param>
/// <param name="Value">The value of the list record.</param>
public record ListRecord(ATUri? Uri, ATCid? ATCid, ATRecord? Value);
