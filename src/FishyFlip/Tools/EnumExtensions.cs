// <copyright file="EnumExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Enum Extensions.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Returns the endpoint string for the enum.
    /// </summary>
    /// <param name="me"><see cref="ModerationReasonType"/>.</param>
    /// <returns>String.</returns>
    public static string ToEndpointString(this ModerationReasonType me)
    {
        switch (me)
        {
            case ModerationReasonType.ReasonSpam:
                return Constants.ModerationReasons.ReasonSpam;
            case ModerationReasonType.ReasonViolation:
                return Constants.ModerationReasons.ReasonViolation;
            case ModerationReasonType.ReasonMisleading:
                return Constants.ModerationReasons.ReasonMisleading;
            case ModerationReasonType.ReasonSexual:
                return Constants.ModerationReasons.ReasonSexual;
            case ModerationReasonType.ReasonRude:
                return Constants.ModerationReasons.ReasonRude;
            case ModerationReasonType.ReasonOther:
                return Constants.ModerationReasons.ReasonOther;
            default:
                return "Unknown";
        }
    }
}
