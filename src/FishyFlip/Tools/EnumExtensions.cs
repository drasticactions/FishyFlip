// <copyright file="EnumExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

public static class EnumExtensions
{
    public static string ToFriendlyString(this ModerationReasonType me)
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
