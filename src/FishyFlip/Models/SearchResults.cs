// <copyright file="SearchResults.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Search Results.
/// </summary>
/// <param name="Posts">Post Views.</param>
/// <param name="HitsTotal">Count of search hits. Optional, may be rounded/truncated, and may not be possible to paginate through all hits.</param>
/// <param name="Cursor">Optional pagination mechanism.</param>
public record SearchResults(PostView[] Posts, int HitsTotal, string? Cursor);