// <copyright file="Constants.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

internal static class Constants
{
    internal const string BlueskyApiClient = "Bluesky";
    internal const string ContentMediaType = "application/json";
    internal const string AcceptedMediaType = "application/json";

    internal static class Urls
    {
        internal static class AtProtoServer
        {
            internal const string Login = "/xrpc/com.atproto.server.createSession";
            internal const string GetSession = "/xrpc/com.atproto.server.getSession";
            internal const string RefreshSession = "/xrpc/com.atproto.server.refreshSession";
            internal const string CreateAccount = "/xrpc/com.atproto.server.createAccount";
            internal const string DeleteAccount = "/xrpc/com.atproto.server.deleteAccount";
            internal const string RequestAccountDelete = "/xrpc/com.atproto.server.requestAccountDelete";
            internal const string RequestPasswordReset = "/xrpc/com.atproto.server.requestPasswordReset";
            internal const string ResetPassword = "/xrpc/com.atproto.server.resetPassword";
            internal const string RevokeAppPassword = "/xrpc/com.atproto.server.revokeAppPassword";
            internal const string DeleteSession = "/xrpc/com.atproto.server.deleteSession";
            internal const string DescribeServer = "/xrpc/com.atproto.server.describeServer";
            internal const string GetAccountInviteCodes = "/xrpc/com.atproto.server.getAccountInviteCodes";
            internal const string CreateAppPassword = "/xrpc/com.atproto.server.createAppPassword";
            internal const string ListAppPasswords = "/xrpc/com.atproto.server.listAppPasswords";
            internal const string CreateInviteCode = "/xrpc/com.atproto.server.createInviteCode";
            internal const string CreateInviteCodes = "/xrpc/com.atproto.server.createInviteCodes";
        }

        internal static class AtProtoModeration
        {
            internal const string CreateReport = "/xrpc/com.atproto.moderation.createReport";
        }

        internal static class AtProtoAdmin
        {
            internal const string GetInviteCodes = "/xrpc/com.atproto.admin.getInviteCodes";
            internal const string GetModerationAction = "/xrpc/com.atproto.admin.getModerationAction";
            internal const string GetModerationActions = "/xrpc/com.atproto.admin.getModerationActions";
            internal const string GetModerationReport = "/xrpc/com.atproto.admin.getModerationReport";
            internal const string GetModerationReports = "/xrpc/com.atproto.admin.getModerationReports";
            internal const string GetRecord = "/xrpc/com.atproto.admin.getRecord";
            internal const string GetRepo = "/xrpc/com.atproto.admin.getRepo";
            internal const string SearchRepos = "/xrpc/com.atproto.admin.searchRepos";
            internal const string ResolveModerationReports = "/xrpc/com.atproto.admin.resolveModerationReports";
            internal const string ReverseModerationAction = "/xrpc/com.atproto.admin.reverseModerationAction";
            internal const string TakeModerationAction = "/xrpc/com.atproto.admin.takeModerationAction";
            internal const string UpdateAccountEmail = "/xrpc/com.atproto.admin.updateAccountEmail";
            internal const string UpdateAccountHandle = "/xrpc/com.atproto.admin.updateAccountHandle";
        }

        internal static class AtProtoLabel
        {
            internal const string QueryLabels = "/xrpc/com.atproto.label.queryLabels";
            internal const string SubscribeLabels = "/xrpc/com.atproto.label.subscribeLabels";
        }

        internal static class AtProtoIdentity
        {
            internal const string ResolveHandle = "/xrpc/com.atproto.identity.resolveHandle";
            internal const string UpdateHandle = "/xrpc/com.atproto.identity.updateHandle";
        }

        internal static class AtProtoRepo
        {
            internal const string CreateRecord = "/xrpc/com.atproto.repo.createRecord";
            internal const string UploadBlob = "/xrpc/com.atproto.repo.uploadBlob";
            internal const string PutRecord = "/xrpc/com.atproto.repo.putRecord";
            internal const string ListRecords = "/xrpc/com.atproto.repo.listRecords";
            internal const string GetRecord = "/xrpc/com.atproto.repo.getRecord";
            internal const string DescribeRepo = "/xrpc/com.atproto.repo.describeRepo";
            internal const string DeleteRecord = "/xrpc/com.atproto.repo.deleteRecord";
            internal const string ApplyWrites = "/xrpc/com.atproto.repo.applyWrites";
        }

        internal static class AtProtoSync
        {
            internal const string GetBlob = "/xrpc/com.atproto.sync.getBlob";
            internal const string GetBlocks = "/xrpc/com.atproto.sync.getBlocks";
            internal const string GetCheckout = "/xrpc/com.atproto.sync.getCheckout";
            internal const string GetCommitPath = "/xrpc/com.atproto.sync.getCommitPath";
            internal const string GetHead = "/xrpc/com.atproto.sync.getHead";
            internal const string GetRecord = "/xrpc/com.atproto.sync.getRecord";
            internal const string GetRepo = "/xrpc/com.atproto.sync.getRepo";
            internal const string ListBlobs = "/xrpc/com.atproto.sync.listBlobs";
            internal const string ListRepos = "/xrpc/com.atproto.sync.listRepos";
            internal const string NotifyOfUpdate = "/xrpc/com.atproto.sync.notifyOfUpdate";
            internal const string RequestCrawl = "/xrpc/com.atproto.sync.requestCrawl";
            internal const string SubscribeRepos = "/xrpc/com.atproto.sync.subscribeRepos";
        }

        internal static class Bluesky
        {
            internal const string GetAuthorFeed = "/xrpc/app.bsky.feed.getAuthorFeed";
            internal const string GetTimeline = "/xrpc/app.bsky.feed.getTimeline";
            internal const string GetFeedSkeleton = "/xrpc/app.bsky.feed.getFeedSkeleton";
            internal const string GetPostThread = "/xrpc/app.bsky.feed.getPostThread";
            internal const string GetPosts = "/xrpc/app.bsky.feed.getPosts";
            internal const string GetLikes = "/xrpc/app.bsky.feed.getLikes";
            internal const string GetRepostedBy = "/xrpc/app.bsky.feed.getRepostedBy";
            internal const string GetActorProfile = "/xrpc/app.bsky.actor.getProfile";
            internal const string GetActorProfiles = "/xrpc/app.bsky.actor.getProfiles";
            internal const string GetActorSuggestions = "/xrpc/app.bsky.actor.getSuggestions";
            internal const string SearchActors = "/xrpc/app.bsky.actor.searchActors";
            internal const string SearchActorsTypeahead = "/xrpc/app.bsky.actor.searchActorsTypeahead";
            internal const string GetBlocks = "/xrpc/app.bsky.graph.getBlocks";
            internal const string GetFollowers = "/xrpc/app.bsky.graph.getFollowers";
            internal const string GetFollows = "/xrpc/app.bsky.graph.getFollows";
            internal const string GetMutes = "/xrpc/app.bsky.graph.getMutes";
            internal const string MuteActor = "/xrpc/app.bsky.graph.muteActor";
            internal const string UnmuteActor = "/xrpc/app.bsky.graph.unmuteActor";
            internal const string Block = "/xrpc/app.bsky.graph.block";
            internal const string Follow = "/xrpc/app.bsky.graph.follow";
            internal const string NotificationUpdateSeen = "/xrpc/app.bsky.notification.updateSeen";
            internal const string NotificationListNotifications = "/xrpc/app.bsky.notification.listNotifications";
            internal const string NotificationGetUnreadCount = "/xrpc/app.bsky.notification.getUnreadCount";
        }
    }

    internal static class ActorTypes
    {
        internal const string Profile = "app.bsky.actor.profile";
    }

    internal static class GraphTypes
    {
        internal const string ListItem = "app.bsky.graph.listitem";
        internal const string List = "app.bsky.graph.list";
        internal const string Follow = "app.bsky.graph.follow";
        internal const string Block = "app.bsky.graph.block";
    }

    internal static class FacetTypes
    {
        internal const string Link = "app.bsky.richtext.facet#link";
        internal const string Mention = "app.bsky.richtext.facet#mention";
    }

    internal static class EmbedTypes
    {
        internal const string External = "app.bsky.embed.external";
        internal const string Images = "app.bsky.embed.images";
        internal const string Record = "app.bsky.embed.record";
        internal const string RecordWithMedia = "app.bsky.embed.recordWithMedia";
    }

    internal static class FeedType
    {
        internal const string Generator = "app.bsky.feed.generator";
        internal const string Like = "app.bsky.feed.like";
        internal const string Repost = "app.bsky.feed.repost";
        internal const string Post = "app.bsky.feed.post";
    }

    internal class HeaderNames
    {
        internal const string UserAgent = "user-agent";
    }
}