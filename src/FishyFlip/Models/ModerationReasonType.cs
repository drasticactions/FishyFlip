// <copyright file="ModerationReasonType.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the types of moderation reasons.
/// </summary>
public enum ModerationReasonType
{
    /// <summary>
    /// Spam.
    /// </summary>
    ReasonSpam,

    /// <summary>
    /// Violation.
    /// </summary>
    ReasonViolation,

    /// <summary>
    /// Misleading.
    /// </summary>
    ReasonMisleading,

    /// <summary>
    /// Sexual.
    /// </summary>
    ReasonSexual,

    /// <summary>
    /// Violent/Rude.
    /// </summary>
    ReasonRude,

    /// <summary>
    /// Other.
    /// </summary>
    ReasonOther,
}
