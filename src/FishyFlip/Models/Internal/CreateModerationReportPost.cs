// <copyright file="CreateModerationReportPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a moderation report post.
/// </summary>
/// <param name="ReasonType">The type of the reason for the report.</param>
/// <param name="Subject">The subject of the report, represented as a strong reference to a repository.</param>
/// <param name="Reason">The specific reason for the report. This is optional and defaults to null.</param>
public record CreateModerationReportPost(string ReasonType, RepoStrongRef Subject, string? Reason = default);