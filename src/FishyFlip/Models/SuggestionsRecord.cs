// <copyright file="SuggestionsRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record containing suggestions.
/// </summary>
/// <param name="Suggestions">An array of ActorProfile objects representing the suggestions.</param>
public record SuggestionsRecord(ActorProfile[] Suggestions);