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
            public const string PublicApi = "https://public.api.bsky.app";
            public const string SocialApi = "https://bsky.social";
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
            public const string RequestEmailUpdate = "/xrpc/com.atproto.server.requestEmailUpdate";
            public const string CheckAccountStatus = "/xrpc/com.atproto.server.checkAccountStatus";
            public const string UpdateEmail = "/xrpc/com.atproto.server.updateEmail";
            public const string ConfirmEmail = "/xrpc/com.atproto.server.confirmEmail";
            public const string DeactivateAccount = "/xrpc/com.atproto.server.deactivateAccount";
            public const string ActivateAccount = "/xrpc/com.atproto.server.activateAccount";
            public const string GetServerAuth = "/xrpc/com.atproto.server.getServerAuth";
            public const string GetServiceAuth = "/xrpc/com.atproto.server.getServiceAuth";
            public const string ReserveSigningKey = "/xrpc/com.atproto.server.reserveSigningKey";
            public const string RequestEmailConfirmation = "/xrpc/com.atproto.server.requestEmailConfirmation";
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
            public const string DeleteAccount = "/xrpc/com.atproto.admin.deleteAccount";
            public const string GetAccountInfos = "/xrpc/com.atproto.admin.getAccountInfos";
            public const string SendEmail = "/xrpc/com.atproto.admin.sendEmail";
            public const string UpdateSubjectStatus = "/xrpc/com.atproto.admin.updateSubjectStatus";
            public const string DisableInviteCodes = "/xrpc/com.atproto.admin.disableInviteCodes";
            public const string DisableAccountInvites = "/xrpc/com.atproto.admin.disableAccountInvites";
            public const string EnableAccountInvites = "/xrpc/com.atproto.admin.enableAccountInvites";
            public const string UpdateAccountPassword = "/xrpc/com.atproto.admin.updateAccountPassword";
            public const string GetSubjectStatus = "/xrpc/com.atproto.admin.getSubjectStatus";
            public const string GetAccountInfo = "/xrpc/com.atproto.admin.getAccountInfo";
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
            public const string GetRecommendedDidCredentials = "/xrpc/com.atproto.identity.getRecommendedDidCredentials";
            public const string RequestPlcOperationSignature = "/xrpc/com.atproto.identity.requestPlcOperationSignature";
            public const string SubmitPlcOperation = "/xrpc/com.atproto.identity.submitPlcOperation";
            public const string SignPlcOperation = "/xrpc/com.atproto.identity.signPlcOperation";
        }

        public static class ATProtoTemp
        {
            public const string CheckSignupQueue = "/xrpc/com.atproto.temp.checkSignupQueue";
            public const string RequestPhoneVerification = "/xrpc/com.atproto.temp.requestPhoneVerification";
            public const string FetchLabels = "/xrpc/com.atproto.temp.fetchLabels";
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
            public const string ImportRepo = "/xrpc/com.atproto.repo.importRepo";
            public const string ListMissingBlobs = "/xrpc/com.atproto.repo.listMissingBlobs";
        }

        public static class ATProtoSync
        {
            public const string GetBlob = "/xrpc/com.atproto.sync.getBlob";
            public const string GetBlocks = "/xrpc/com.atproto.sync.getBlocks";
            public const string GetCheckout = "/xrpc/com.atproto.sync.getCheckout";
            public const string GetCommitPath = "/xrpc/com.atproto.sync.getCommitPath";
            public const string GetHead = "/xrpc/com.atproto.sync.getHead";
            public const string GetLatestCommit = "/xrpc/com.atproto.sync.getLatestCommit";
            public const string GetRecord = "/xrpc/com.atproto.sync.getRecord";
            public const string GetRepo = "/xrpc/com.atproto.sync.getRepo";
            public const string ListBlobs = "/xrpc/com.atproto.sync.listBlobs";
            public const string ListRepos = "/xrpc/com.atproto.sync.listRepos";
            public const string NotifyOfUpdate = "/xrpc/com.atproto.sync.notifyOfUpdate";
            public const string RequestCrawl = "/xrpc/com.atproto.sync.requestCrawl";
            public const string SubscribeRepos = "/xrpc/com.atproto.sync.subscribeRepos";
        }

        public static class Ozone
        {
            public static class Moderation
            {
                public const string QueryStatuses = "/xrpc/tools.ozone.moderation.queryStatuses";
                public const string GetRepo = "/xrpc/tools.ozone.moderation.getRepo";
                public const string GetEvent = "/xrpc/tools.ozone.moderation.getEvent";
                public const string QueryEvents = "/xrpc/tools.ozone.moderation.queryEvents";
                public const string GetRecord = "/xrpc/tools.ozone.moderation.getRecord";
                public const string EmitEvent = "/xrpc/tools.ozone.moderation.emitEvent";
                public const string SearchRepos = "/xrpc/tools.ozone.moderation.searchRepos";
            }

            public static class Communication
            {
                public const string UpdateTemplate = "/xrpc/tools.ozone.communication.updateTemplate";
                public const string CreateTemplate = "/xrpc/tools.ozone.communication.createTemplate";
                public const string ListTemplates = "/xrpc/tools.ozone.communication.listTemplates";
                public const string DeleteTemplate = "/xrpc/tools.ozone.communication.deleteTemplate";
            }
        }

        public static class Bluesky
        {
            public static class Chat
            {
                public static class Convo
                {
                    public const string ListConvos = "/xrpc/chat.bsky.convo.listConvos";
                    public const string UnmuteConvo = "/xrpc/chat.bsky.convo.unmuteConvo";
                    public const string GetLog = "/xrpc/chat.bsky.convo.getLog";
                    public const string SendMessage = "/xrpc/chat.bsky.convo.sendMessage";
                    public const string LeaveConvo = "/xrpc/chat.bsky.convo.leaveConvo";
                    public const string MuteConvo = "/xrpc/chat.bsky.convo.muteConvo";
                    public const string DeleteMessageForSelf = "/xrpc/chat.bsky.convo.deleteMessageForSelf";
                    public const string UpdateRead = "/xrpc/chat.bsky.convo.updateRead";
                    public const string GetConvo = "/xrpc/chat.bsky.convo.getConvo";
                    public const string GetMessages = "/xrpc/chat.bsky.convo.getMessages";
                    public const string GetConvoForMembers = "/xrpc/chat.bsky.convo.getConvoForMembers";
                    public const string SendMessageBatch = "/xrpc/chat.bsky.convo.sendMessageBatch";
                }

                public static class Actor
                {
                    public const string ExportAccountData = "/xrpc/chat.bsky.actor.exportAccountData";
                    public const string DeleteAccount = "/xrpc/chat.bsky.actor.deleteAccount";
                }

                public static class Moderation
                {
                    public const string GetActorMetadata = "/xrpc/chat.bsky.moderation.getActorMetadata";
                    public const string GetMessageContext = "/xrpc/chat.bsky.moderation.getMessageContext";
                    public const string UpdateActorAccess = "/xrpc/chat.bsky.moderation.updateActorAccess";
                }
            }

            public static class Labeler
            {
                public const string Service = "/xrpc/app.bsky.labeler.service";
                public const string GetServices = "/xrpc/app.bsky.labeler.getServices";
            }

            public static class Feed
            {
                public const string GetAuthorFeed = "/xrpc/app.bsky.feed.getAuthorFeed";
                public const string GetActorLikes = "/xrpc/app.bsky.feed.getActorLikes";
                public const string GetTimeline = "/xrpc/app.bsky.feed.getTimeline";
                public const string GetPostThread = "/xrpc/app.bsky.feed.getPostThread";
                public const string GetFeedGenerator = "/xrpc/app.bsky.feed.getFeedGenerator";
                public const string GetFeedGenerators = "/xrpc/app.bsky.feed.getFeedGenerators";
                public const string GetFeed = "/xrpc/app.bsky.feed.getFeed";
                public const string GetListFeed = "/xrpc/app.bsky.feed.getListFeed";
                public const string GetPosts = "/xrpc/app.bsky.feed.getPosts";
                public const string GetLikes = "/xrpc/app.bsky.feed.getLikes";
                public const string GetRepostedBy = "/xrpc/app.bsky.feed.getRepostedBy";
                public const string GetSuggestedFeeds = "/xrpc/app.bsky.feed.getSuggestedFeeds";
                public const string GetActorFeeds = "/xrpc/app.bsky.feed.getActorFeeds";
                public const string SearchPosts = "/xrpc/app.bsky.feed.searchPosts";
            }

            public static class Actor
            {
                public const string GetActorProfile = "/xrpc/app.bsky.actor.getProfile";
                public const string GetActorProfiles = "/xrpc/app.bsky.actor.getProfiles";
                public const string GetPreferences = "/xrpc/app.bsky.actor.getPreferences";
                public const string PutPreferences = "/xrpc/app.bsky.actor.putPreferences";
                public const string GetActorSuggestions = "/xrpc/app.bsky.actor.getSuggestions";
                public const string SearchActors = "/xrpc/app.bsky.actor.searchActors";
                public const string SearchActorsTypeahead = "/xrpc/app.bsky.actor.searchActorsTypeahead";
            }

            public static class Graph
            {
                public const string GetSuggestedFollowsByActor = "/xrpc/app.bsky.graph.getSuggestedFollowsByActor";
                public const string GetBlocks = "/xrpc/app.bsky.graph.getBlocks";
                public const string GetLists = "/xrpc/app.bsky.graph.getLists";
                public const string GetListMutes = "/xrpc/app.bsky.graph.getListMutes";
                public const string GetListBlocks = "/xrpc/app.bsky.graph.getListBlocks";
                public const string GetList = "/xrpc/app.bsky.graph.getList";
                public const string GetFollowers = "/xrpc/app.bsky.graph.getFollowers";
                public const string GetFollows = "/xrpc/app.bsky.graph.getFollows";
                public const string GetMutes = "/xrpc/app.bsky.graph.getMutes";
                public const string MuteActor = "/xrpc/app.bsky.graph.muteActor";
                public const string UnmuteActor = "/xrpc/app.bsky.graph.unmuteActor";
                public const string MuteActorList = "/xrpc/app.bsky.graph.muteActorList";
                public const string UnmuteActorList = "/xrpc/app.bsky.graph.unmuteActorList";
                public const string Block = "/xrpc/app.bsky.graph.block";
                public const string Follow = "/xrpc/app.bsky.graph.follow";
            }

            public static class Notification
            {
                public const string NotificationUpdateSeen = "/xrpc/app.bsky.notification.updateSeen";
                public const string NotificationRegisterPush = "/xrpc/app.bsky.notification.registerPush";
                public const string NotificationListNotifications = "/xrpc/app.bsky.notification.listNotifications";
                public const string NotificationGetUnreadCount = "/xrpc/app.bsky.notification.getUnreadCount";
            }

            public static class Unspecced
            {
                public const string GetPopularFeedGenerators = "/xrpc/app.bsky.unspecced.getPopularFeedGenerators";
                public const string GetTaggedSuggestions = "/xrpc/app.bsky.unspecced.getTaggedSuggestions";
                public const string SearchPostsSkeleton = "/xrpc/app.bsky.unspecced.searchPostsSkeleton";
                public const string GetSuggestionsSkeleton = "/xrpc/app.bsky.unspecced.getSuggestionsSkeleton";
                public const string SearchActorsSkeleton = "/xrpc/app.bsky.unspecced.searchActorsSkeleton";
            }
        }

        public static class WhiteWind
        {
            public const string GetAuthorPosts = "/xrpc/com.whtwnd.blog.getAuthorPosts";
            public const string GetEntryMetadataByName = "/xrpc/com.whtwnd.blog.getEntryMetadataByName";
            public const string GetMentionsByEntry = "/xrpc/com.whtwnd.blog.getMentionsByEntry";
            public const string NotifyOfNewEntry = "/xrpc/com.whtwnd.blog.notifyOfNewEntry";
        }
    }

    public static class WhiteWindTypes
    {
        public const string Entry = "com.whtwnd.blog.entry";
        public const string Comment = "com.whtwnd.blog.comment";
    }

    public static class ActorTypes
    {
        public const string Profile = "app.bsky.actor.profile";
        public const string FeedViewPref = "app.bsky.actor.defs#feedViewPref";
        public const string ContentLabelPref = "app.bsky.actor.defs#contentLabelPref";
        public const string AdultContentPref = "app.bsky.actor.defs#adultContentPref";
        public const string SavedFeedsPref = "app.bsky.actor.defs#savedFeedsPref";
    }

    public static class ConversationTypes
    {
        public const string MessageView = "chat.bsky.convo.defs#messageView";
        public const string DeletedMessageView = "chat.bsky.convo.defs#deletedMessageView";
        public const string LogBeginConvo = "chat.bsky.convo.defs#logBeginConvo";
        public const string LogLeaveConvo = "chat.bsky.convo.defs#logLeaveConvo";
        public const string LogCreateMessage = "chat.bsky.convo.defs#logCreateMessage";
        public const string LogDeleteMessage = "chat.bsky.convo.defs#logDeleteMessage";
    }

    public static class DeclarationTypes
    {
        public const string AllowIncomingPref = "chat.bsky.actor.declaration";
    }

    public static class ATProtoProxy
    {
        public const string Proxy = "atproto-proxy";
        public const string BskyChat = "did:web:api.bsky.chat#bsky_chat";
    }

    public static class AllowIncomingTypes
    {
        public const string Following = "following";
        public const string All = "all";
        public const string None = "none";
    }

    public static class GraphTypes
    {
        public const string ListItem = "app.bsky.graph.listitem";
        public const string List = "app.bsky.graph.list";
        public const string Follow = "app.bsky.graph.follow";
        public const string Block = "app.bsky.graph.block";
    }

    public static class ListReasons
    {
        public const string ModList = "app.bsky.graph.defs#modlist";
        public const string CurateList = "app.bsky.graph.defs#curatelist";
    }

    public static class FacetTypes
    {
        public const string Tag = "app.bsky.richtext.facet#tag";
        public const string Link = "app.bsky.richtext.facet#link";
        public const string Mention = "app.bsky.richtext.facet#mention";
    }

    public static class EmbedTypes
    {
        public const string External = "app.bsky.embed.external";
        public const string ExternalView = "app.bsky.embed.external#view";
        public const string Images = "app.bsky.embed.images";
        public const string ImageView = "app.bsky.embed.images#view";
        public const string RecordView = "app.bsky.embed.record#view";
        public const string Record = "app.bsky.embed.record";
        public const string RecordWithMedia = "app.bsky.embed.recordWithMedia";
        public const string RecordWithMediaView = "app.bsky.embed.recordWithMedia#view";
    }

    public static class FeedType
    {
        public const string Generator = "app.bsky.feed.generator";
        public const string Like = "app.bsky.feed.like";
        public const string Repost = "app.bsky.feed.repost";
        public const string Post = "app.bsky.feed.post";
        public const string ThreadGate = "app.bsky.feed.threadgate";
    }

    public static class ThreadGateReasonType
    {
        public const string MentionRule = "app.bsky.feed.threadgate#mentionRule";
        public const string FollowingRule = "app.bsky.feed.threadgate#followingRule";
        public const string ListRule = "app.bsky.feed.threadgate#listRule";
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

    public static class ApplyWriteTypes
    {
        public const string Create = "com.atproto.repo.applyWrites#create";
        public const string Update = "com.atproto.repo.applyWrites#update";
        public const string Delete = "com.atproto.repo.applyWrites#delete";
    }

    internal class HeaderNames
    {
        public const string UserAgent = "user-agent";
    }
}
#pragma warning restore SA1600 // Elements should be documented