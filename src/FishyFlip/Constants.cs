// <copyright file="Constants.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;
#pragma warning disable SA1600 // Elements should be documented
public static class Constants
{
    internal const string BlueskyApiClient = "FishyFlip";
    internal const string ContentMediaType = "application/json";
    internal const string AcceptedMediaType = "application/json";

    public static class Urls
    {
        public static class ATProtoServer
        {
            public const string CreateSession = "/xrpc/com.atproto.server.createSession";
            public const string GetSession = "/xrpc/com.atproto.server.getSession";
            public const string RefreshSession = "/xrpc/com.atproto.server.refreshSession";
            public const string CreateAccount = "/xrpc/com.atproto.server.createAccount";
            public const string DeleteAccount = "/xrpc/com.atproto.server.deleteAccount";
            public const string RequestAccountDelete = "/xrpc/com.atproto.server.requestAccountDelete";
            public const string RequestPasswordReset = "/xrpc/com.atproto.server.requestPasswordReset";
            public const string ResetPassword = "/xrpc/com.atproto.server.resetPassword";
            public const string RevokeAppPassword = "/xrpc/com.atproto.server.revokeAppPassword";
            public const string DeleteSession = "/xrpc/com.atproto.server.deleteSession";
            public const string DescribeServer = "/xrpc/com.atproto.server.describeServer";
            public const string GetAccountInviteCodes = "/xrpc/com.atproto.server.getAccountInviteCodes";
            public const string CreateAppPassword = "/xrpc/com.atproto.server.createAppPassword";
            public const string ListAppPasswords = "/xrpc/com.atproto.server.listAppPasswords";
            public const string CreateInviteCode = "/xrpc/com.atproto.server.createInviteCode";
            public const string CreateInviteCodes = "/xrpc/com.atproto.server.createInviteCodes";
        }

        public static class ATProtoModeration
        {
            public const string CreateReport = "/xrpc/com.atproto.moderation.createReport";
        }

        public static class ATProtoAdmin
        {
            public const string GetInviteCodes = "/xrpc/com.atproto.admin.getInviteCodes";
            public const string GetModerationAction = "/xrpc/com.atproto.admin.getModerationAction";
            public const string GetModerationActions = "/xrpc/com.atproto.admin.getModerationActions";
            public const string GetModerationReport = "/xrpc/com.atproto.admin.getModerationReport";
            public const string GetModerationReports = "/xrpc/com.atproto.admin.getModerationReports";
            public const string GetRecord = "/xrpc/com.atproto.admin.getRecord";
            public const string GetRepo = "/xrpc/com.atproto.admin.getRepo";
            public const string SearchRepos = "/xrpc/com.atproto.admin.searchRepos";
            public const string ResolveModerationReports = "/xrpc/com.atproto.admin.resolveModerationReports";
            public const string ReverseModerationAction = "/xrpc/com.atproto.admin.reverseModerationAction";
            public const string TakeModerationAction = "/xrpc/com.atproto.admin.takeModerationAction";
            public const string UpdateAccountEmail = "/xrpc/com.atproto.admin.updateAccountEmail";
            public const string UpdateAccountHandle = "/xrpc/com.atproto.admin.updateAccountHandle";
        }

        public static class ATProtoLabel
        {
            public const string QueryLabels = "/xrpc/com.atproto.label.queryLabels";
            public const string SubscribeLabels = "/xrpc/com.atproto.label.subscribeLabels";
        }

        public static class ATProtoIdentity
        {
            public const string ResolveHandle = "/xrpc/com.atproto.identity.resolveHandle";
            public const string UpdateHandle = "/xrpc/com.atproto.identity.updateHandle";
        }

        public static class ATProtoRepo
        {
            public const string CreateRecord = "/xrpc/com.atproto.repo.createRecord";
            public const string UploadBlob = "/xrpc/com.atproto.repo.uploadBlob";
            public const string PutRecord = "/xrpc/com.atproto.repo.putRecord";
            public const string ListRecords = "/xrpc/com.atproto.repo.listRecords";
            public const string GetRecord = "/xrpc/com.atproto.repo.getRecord";
            public const string DescribeRepo = "/xrpc/com.atproto.repo.describeRepo";
            public const string DeleteRecord = "/xrpc/com.atproto.repo.deleteRecord";
            public const string ApplyWrites = "/xrpc/com.atproto.repo.applyWrites";
        }

        public static class ATProtoSync
        {
            public const string GetBlob = "/xrpc/com.atproto.sync.getBlob";
            public const string GetBlocks = "/xrpc/com.atproto.sync.getBlocks";
            public const string GetCheckout = "/xrpc/com.atproto.sync.getCheckout";
            public const string GetCommitPath = "/xrpc/com.atproto.sync.getCommitPath";
            public const string GetHead = "/xrpc/com.atproto.sync.getHead";
            public const string GetRecord = "/xrpc/com.atproto.sync.getRecord";
            public const string GetRepo = "/xrpc/com.atproto.sync.getRepo";
            public const string ListBlobs = "/xrpc/com.atproto.sync.listBlobs";
            public const string ListRepos = "/xrpc/com.atproto.sync.listRepos";
            public const string NotifyOfUpdate = "/xrpc/com.atproto.sync.notifyOfUpdate";
            public const string RequestCrawl = "/xrpc/com.atproto.sync.requestCrawl";
            public const string SubscribeRepos = "/xrpc/com.atproto.sync.subscribeRepos";
        }

        public static class Bluesky
        {
            public static class Feed
            {
                public const string GetAuthorFeed = "/xrpc/app.bsky.feed.getAuthorFeed";
                public const string GetTimeline = "/xrpc/app.bsky.feed.getTimeline";
                public const string GetFeedSkeleton = "/xrpc/app.bsky.feed.getFeedSkeleton";
                public const string GetPostThread = "/xrpc/app.bsky.feed.getPostThread";
                public const string GetPosts = "/xrpc/app.bsky.feed.getPosts";
                public const string GetLikes = "/xrpc/app.bsky.feed.getLikes";
                public const string GetRepostedBy = "/xrpc/app.bsky.feed.getRepostedBy";
            }

            public static class Actor
            {
                public const string GetActorProfile = "/xrpc/app.bsky.actor.getProfile";
                public const string GetActorProfiles = "/xrpc/app.bsky.actor.getProfiles";
                public const string GetActorSuggestions = "/xrpc/app.bsky.actor.getSuggestions";
                public const string SearchActors = "/xrpc/app.bsky.actor.searchActors";
                public const string SearchActorsTypeahead = "/xrpc/app.bsky.actor.searchActorsTypeahead";
            }

            public static class Graph
            {
                public const string GetBlocks = "/xrpc/app.bsky.graph.getBlocks";
                public const string GetFollowers = "/xrpc/app.bsky.graph.getFollowers";
                public const string GetFollows = "/xrpc/app.bsky.graph.getFollows";
                public const string GetMutes = "/xrpc/app.bsky.graph.getMutes";
                public const string MuteActor = "/xrpc/app.bsky.graph.muteActor";
                public const string UnmuteActor = "/xrpc/app.bsky.graph.unmuteActor";
                public const string Block = "/xrpc/app.bsky.graph.block";
                public const string Follow = "/xrpc/app.bsky.graph.follow";
            }

            public static class Notification
            {
                public const string NotificationUpdateSeen = "/xrpc/app.bsky.notification.updateSeen";
                public const string NotificationListNotifications = "/xrpc/app.bsky.notification.listNotifications";
                public const string NotificationGetUnreadCount = "/xrpc/app.bsky.notification.getUnreadCount";
            }
        }
    }

    public static class ActorTypes
    {
        public const string Profile = "app.bsky.actor.profile";
    }

    public static class GraphTypes
    {
        public const string ListItem = "app.bsky.graph.listitem";
        public const string List = "app.bsky.graph.list";
        public const string Follow = "app.bsky.graph.follow";
        public const string Block = "app.bsky.graph.block";
    }

    public static class FacetTypes
    {
        public const string Link = "app.bsky.richtext.facet#link";
        public const string Mention = "app.bsky.richtext.facet#mention";
    }

    public static class EmbedTypes
    {
        public const string External = "app.bsky.embed.external";
        public const string Images = "app.bsky.embed.images";
        public const string ImageView = "app.bsky.embed.images#view";
        public const string Record = "app.bsky.embed.record";
        public const string RecordWithMedia = "app.bsky.embed.recordWithMedia";
    }

    public static class FeedType
    {
        public const string Generator = "app.bsky.feed.generator";
        public const string Like = "app.bsky.feed.like";
        public const string Repost = "app.bsky.feed.repost";
        public const string Post = "app.bsky.feed.post";
    }

    public static class ModerationReasons
    {
        public const string ReasonSpam = "com.atproto.moderation.defs#reasonSpam";
        public const string ReasonViolation = "com.atproto.moderation.defs#reasonViolation";
        public const string ReasonMisleading = "com.atproto.moderation.defs#reasonMisleading";
        public const string ReasonSexual = "com.atproto.moderation.defs#reasonSexual";
        public const string ReasonRude = "com.atproto.moderation.defs#reasonRude";
        public const string ReasonOther = "com.atproto.moderation.defs#reasonOther";
    }

    internal class HeaderNames
    {
        public const string UserAgent = "user-agent";
    }
}
#pragma warning restore SA1600 // Elements should be documented