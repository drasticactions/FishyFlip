// <copyright file="Used.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record of an item being used.
/// </summary>
/// <param name="UsedBy">The identifier of the entity that used the item.</param>
/// <param name="UsedAt">The date and time when the item was used.</param>
public record Used(ATDid UsedBy, DateTime UsedAt);