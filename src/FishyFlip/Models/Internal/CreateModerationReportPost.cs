// <copyright file="CreateModerationReportPost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record CreateModerationReportPost(string ReasonType, RepoStrongRef Subject, string? Reason = default);
