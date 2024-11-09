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

    /// <summary>
    /// Returns the filter type string for the enum.
    /// </summary>
    /// <param name="me"><see cref="AuthorFeedFilterType"/>.</param>
    /// <returns>String.</returns>
    public static string ToActorFeedFilterString(this AuthorFeedFilterType me)
    {
        switch (me)
        {
            case AuthorFeedFilterType.PostsWithReplies:
                return Constants.AuthorFeedFilterTypes.PostsWithReplies;
            case AuthorFeedFilterType.PostsNoReplies:
                return Constants.AuthorFeedFilterTypes.PostsNoReplies;
            case AuthorFeedFilterType.PostsWithMedia:
                return Constants.AuthorFeedFilterTypes.PostsWithMedia;
            case AuthorFeedFilterType.PostsAndAuthorThreads:
                return Constants.AuthorFeedFilterTypes.PostsAndAuthorThreads;
            default:
                throw new InvalidOperationException("Unknwon ActorFeedFilter type.");
        }
    }
}
