// <copyright file="CreateModerationReportRepo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record for creating a moderation report.
/// </summary>
/// <param name="ReasonType">The reason type for the report.</param>
/// <param name="Subject">The reference to the admin repository.</param>
/// <param name="Reason">The optional reason for the report.</param>
public record CreateModerationReportRepo(string ReasonType, AdminRepoRef Subject, string? Reason = default);
