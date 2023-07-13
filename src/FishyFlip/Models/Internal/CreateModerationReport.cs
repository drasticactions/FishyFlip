// <copyright file="CreateModerationReport.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record CreateModerationReportPost(string ReasonType, RepoStrongRef Subject, string? Reason = default);

public record CreateModerationReportRepo(string ReasonType, AdminRepoRef Subject, string? Reason = default);
