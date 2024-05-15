// <copyright file="TagSuggestion.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a suggestion for a tag.
/// </summary>
/// <param name="Tag">The suggested tag.</param>
/// <param name="SubjectType">The type of the subject related to the suggested tag.</param>
/// <param name="Subject">The subject related to the suggested tag.</param>
public record TagSuggestion(string Tag, string SubjectType, string Subject);