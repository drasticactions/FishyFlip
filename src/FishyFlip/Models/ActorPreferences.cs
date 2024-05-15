// <copyright file="ActorPreferences.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// The actor preferences.
/// Listed as an array of ATRecords.
/// </summary>
/// <param name="Preferences">List of preferences.</param>
public record ActorPreferences(ATRecord[] Preferences);