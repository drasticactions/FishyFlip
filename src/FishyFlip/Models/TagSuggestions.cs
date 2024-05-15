// <copyright file="TagSuggestions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a collection of tag suggestions.
/// </summary>
/// <param name="Suggestions">An array of TagSuggestion records.</param>
public record TagSuggestions(TagSuggestion[] Suggestions);