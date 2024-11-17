// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon
{
    /// <summary>
    /// Converts CBOR to ATObjects.
    /// </summary>
    public static class CborLexiconExtensions
    {
        public static ATObject? ToATObject(this CBORObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            var type = obj["$type"].AsString();
            switch (type)
            {
                case "app.bsky.actor.defs#profileViewBasic":
                    return new App.Bsky.Actor.ProfileViewBasic(obj);
                case "app.bsky.actor.defs#profileView":
                    return new App.Bsky.Actor.ProfileView(obj);
                case "app.bsky.actor.defs#profileViewDetailed":
                    return new App.Bsky.Actor.ProfileViewDetailed(obj);
                case "app.bsky.actor.defs#profileAssociated":
                    return new App.Bsky.Actor.ProfileAssociated(obj);
                case "app.bsky.actor.defs#profileAssociatedChat":
                    return new App.Bsky.Actor.ProfileAssociatedChat(obj);
                case "app.bsky.actor.defs#viewerState":
                    return new App.Bsky.Actor.ViewerState(obj);
                case "app.bsky.actor.defs#knownFollowers":
                    return new App.Bsky.Actor.KnownFollowers(obj);
                case "app.bsky.actor.defs#adultContentPref":
                    return new App.Bsky.Actor.AdultContentPref(obj);
                case "app.bsky.actor.defs#contentLabelPref":
                    return new App.Bsky.Actor.ContentLabelPref(obj);
                case "app.bsky.actor.defs#savedFeed":
                    return new App.Bsky.Actor.SavedFeed(obj);
                case "app.bsky.actor.defs#savedFeedsPrefV2":
                    return new App.Bsky.Actor.SavedFeedsPrefV2(obj);
                case "app.bsky.actor.defs#savedFeedsPref":
                    return new App.Bsky.Actor.SavedFeedsPref(obj);
                case "app.bsky.actor.defs#personalDetailsPref":
                    return new App.Bsky.Actor.PersonalDetailsPref(obj);
                case "app.bsky.actor.defs#feedViewPref":
                    return new App.Bsky.Actor.FeedViewPref(obj);
                case "app.bsky.actor.defs#threadViewPref":
                    return new App.Bsky.Actor.ThreadViewPref(obj);
                case "app.bsky.actor.defs#interestsPref":
                    return new App.Bsky.Actor.InterestsPref(obj);
                case "app.bsky.actor.defs#mutedWord":
                    return new App.Bsky.Actor.MutedWord(obj);
                case "app.bsky.actor.defs#mutedWordsPref":
                    return new App.Bsky.Actor.MutedWordsPref(obj);
                case "app.bsky.actor.defs#hiddenPostsPref":
                    return new App.Bsky.Actor.HiddenPostsPref(obj);
                case "app.bsky.actor.defs#labelersPref":
                    return new App.Bsky.Actor.LabelersPref(obj);
                case "app.bsky.actor.defs#labelerPrefItem":
                    return new App.Bsky.Actor.LabelerPrefItem(obj);
                case "app.bsky.actor.defs#bskyAppStatePref":
                    return new App.Bsky.Actor.BskyAppStatePref(obj);
                case "app.bsky.actor.defs#bskyAppProgressGuide":
                    return new App.Bsky.Actor.BskyAppProgressGuide(obj);
                case "app.bsky.actor.defs#nux":
                    return new App.Bsky.Actor.Nux(obj);
                case "app.bsky.embed.defs#aspectRatio":
                    return new App.Bsky.Embed.AspectRatio(obj);
                case "app.bsky.feed.defs#postView":
                    return new App.Bsky.Feed.PostView(obj);
                case "app.bsky.feed.defs#viewerState":
                    return new App.Bsky.Feed.ViewerState(obj);
                case "app.bsky.feed.defs#feedViewPost":
                    return new App.Bsky.Feed.FeedViewPost(obj);
                case "app.bsky.feed.defs#replyRef":
                    return new App.Bsky.Feed.ReplyRef(obj);
                case "app.bsky.feed.defs#reasonRepost":
                    return new App.Bsky.Feed.ReasonRepost(obj);
                case "app.bsky.feed.defs#reasonPin":
                    return new App.Bsky.Feed.ReasonPin(obj);
                case "app.bsky.feed.defs#threadViewPost":
                    return new App.Bsky.Feed.ThreadViewPost(obj);
                case "app.bsky.feed.defs#notFoundPost":
                    return new App.Bsky.Feed.NotFoundPost(obj);
                case "app.bsky.feed.defs#blockedPost":
                    return new App.Bsky.Feed.BlockedPost(obj);
                case "app.bsky.feed.defs#blockedAuthor":
                    return new App.Bsky.Feed.BlockedAuthor(obj);
                case "app.bsky.feed.defs#generatorView":
                    return new App.Bsky.Feed.GeneratorView(obj);
                case "app.bsky.feed.defs#generatorViewerState":
                    return new App.Bsky.Feed.GeneratorViewerState(obj);
                case "app.bsky.feed.defs#skeletonFeedPost":
                    return new App.Bsky.Feed.SkeletonFeedPost(obj);
                case "app.bsky.feed.defs#skeletonReasonRepost":
                    return new App.Bsky.Feed.SkeletonReasonRepost(obj);
                case "app.bsky.feed.defs#skeletonReasonPin":
                    return new App.Bsky.Feed.SkeletonReasonPin(obj);
                case "app.bsky.feed.defs#threadgateView":
                    return new App.Bsky.Feed.ThreadgateView(obj);
                case "app.bsky.feed.defs#interaction":
                    return new App.Bsky.Feed.Interaction(obj);
                case "app.bsky.graph.defs#listViewBasic":
                    return new App.Bsky.Graph.ListViewBasic(obj);
                case "app.bsky.graph.defs#listView":
                    return new App.Bsky.Graph.ListView(obj);
                case "app.bsky.graph.defs#listItemView":
                    return new App.Bsky.Graph.ListItemView(obj);
                case "app.bsky.graph.defs#starterPackView":
                    return new App.Bsky.Graph.StarterPackView(obj);
                case "app.bsky.graph.defs#starterPackViewBasic":
                    return new App.Bsky.Graph.StarterPackViewBasic(obj);
                case "app.bsky.graph.defs#listViewerState":
                    return new App.Bsky.Graph.ListViewerState(obj);
                case "app.bsky.graph.defs#notFoundActor":
                    return new App.Bsky.Graph.NotFoundActor(obj);
                case "app.bsky.graph.defs#relationship":
                    return new App.Bsky.Graph.Relationship(obj);
                case "app.bsky.labeler.defs#labelerView":
                    return new App.Bsky.Labeler.LabelerView(obj);
                case "app.bsky.labeler.defs#labelerViewDetailed":
                    return new App.Bsky.Labeler.LabelerViewDetailed(obj);
                case "app.bsky.labeler.defs#labelerViewerState":
                    return new App.Bsky.Labeler.LabelerViewerState(obj);
                case "app.bsky.labeler.defs#labelerPolicies":
                    return new App.Bsky.Labeler.LabelerPolicies(obj);
                case "app.bsky.unspecced.defs#skeletonSearchPost":
                    return new App.Bsky.Unspecced.SkeletonSearchPost(obj);
                case "app.bsky.unspecced.defs#skeletonSearchActor":
                    return new App.Bsky.Unspecced.SkeletonSearchActor(obj);
                case "app.bsky.video.defs#jobStatus":
                    return new App.Bsky.Video.JobStatus(obj);
                case "chat.bsky.actor.defs#profileViewBasic":
                    return new Chat.Bsky.Actor.ProfileViewBasic(obj);
                case "chat.bsky.convo.defs#messageRef":
                    return new Chat.Bsky.Convo.MessageRef(obj);
                case "chat.bsky.convo.defs#messageInput":
                    return new Chat.Bsky.Convo.MessageInput(obj);
                case "chat.bsky.convo.defs#messageView":
                    return new Chat.Bsky.Convo.MessageView(obj);
                case "chat.bsky.convo.defs#deletedMessageView":
                    return new Chat.Bsky.Convo.DeletedMessageView(obj);
                case "chat.bsky.convo.defs#messageViewSender":
                    return new Chat.Bsky.Convo.MessageViewSender(obj);
                case "chat.bsky.convo.defs#convoView":
                    return new Chat.Bsky.Convo.ConvoView(obj);
                case "chat.bsky.convo.defs#logBeginConvo":
                    return new Chat.Bsky.Convo.LogBeginConvo(obj);
                case "chat.bsky.convo.defs#logLeaveConvo":
                    return new Chat.Bsky.Convo.LogLeaveConvo(obj);
                case "chat.bsky.convo.defs#logCreateMessage":
                    return new Chat.Bsky.Convo.LogCreateMessage(obj);
                case "chat.bsky.convo.defs#logDeleteMessage":
                    return new Chat.Bsky.Convo.LogDeleteMessage(obj);
                case "com.atproto.admin.defs#statusAttr":
                    return new Com.Atproto.Admin.StatusAttr(obj);
                case "com.atproto.admin.defs#accountView":
                    return new Com.Atproto.Admin.AccountView(obj);
                case "com.atproto.admin.defs#repoRef":
                    return new Com.Atproto.Admin.RepoRef(obj);
                case "com.atproto.admin.defs#repoBlobRef":
                    return new Com.Atproto.Admin.RepoBlobRef(obj);
                case "com.atproto.admin.defs#threatSignature":
                    return new Com.Atproto.Admin.ThreatSignature(obj);
                case "com.atproto.label.defs#label":
                    return new Com.Atproto.Label.Label(obj);
                case "com.atproto.label.defs#selfLabels":
                    return new Com.Atproto.Label.SelfLabels(obj);
                case "com.atproto.label.defs#selfLabel":
                    return new Com.Atproto.Label.SelfLabel(obj);
                case "com.atproto.label.defs#labelValueDefinition":
                    return new Com.Atproto.Label.LabelValueDefinition(obj);
                case "com.atproto.label.defs#labelValueDefinitionStrings":
                    return new Com.Atproto.Label.LabelValueDefinitionStrings(obj);
                case "com.atproto.repo.defs#commitMeta":
                    return new Com.Atproto.Repo.CommitMeta(obj);
                case "com.atproto.server.defs#inviteCode":
                    return new Com.Atproto.Server.InviteCode(obj);
                case "com.atproto.server.defs#inviteCodeUse":
                    return new Com.Atproto.Server.InviteCodeUse(obj);
                case "tools.ozone.communication.defs#templateView":
                    return new Tools.Ozone.Communication.TemplateView(obj);
                case "tools.ozone.moderation.defs#modEventView":
                    return new Tools.Ozone.Moderation.ModEventView(obj);
                case "tools.ozone.moderation.defs#modEventViewDetail":
                    return new Tools.Ozone.Moderation.ModEventViewDetail(obj);
                case "tools.ozone.moderation.defs#subjectStatusView":
                    return new Tools.Ozone.Moderation.SubjectStatusView(obj);
                case "tools.ozone.moderation.defs#modEventTakedown":
                    return new Tools.Ozone.Moderation.ModEventTakedown(obj);
                case "tools.ozone.moderation.defs#modEventReverseTakedown":
                    return new Tools.Ozone.Moderation.ModEventReverseTakedown(obj);
                case "tools.ozone.moderation.defs#modEventResolveAppeal":
                    return new Tools.Ozone.Moderation.ModEventResolveAppeal(obj);
                case "tools.ozone.moderation.defs#modEventComment":
                    return new Tools.Ozone.Moderation.ModEventComment(obj);
                case "tools.ozone.moderation.defs#modEventReport":
                    return new Tools.Ozone.Moderation.ModEventReport(obj);
                case "tools.ozone.moderation.defs#modEventLabel":
                    return new Tools.Ozone.Moderation.ModEventLabel(obj);
                case "tools.ozone.moderation.defs#modEventAcknowledge":
                    return new Tools.Ozone.Moderation.ModEventAcknowledge(obj);
                case "tools.ozone.moderation.defs#modEventEscalate":
                    return new Tools.Ozone.Moderation.ModEventEscalate(obj);
                case "tools.ozone.moderation.defs#modEventMute":
                    return new Tools.Ozone.Moderation.ModEventMute(obj);
                case "tools.ozone.moderation.defs#modEventUnmute":
                    return new Tools.Ozone.Moderation.ModEventUnmute(obj);
                case "tools.ozone.moderation.defs#modEventMuteReporter":
                    return new Tools.Ozone.Moderation.ModEventMuteReporter(obj);
                case "tools.ozone.moderation.defs#modEventUnmuteReporter":
                    return new Tools.Ozone.Moderation.ModEventUnmuteReporter(obj);
                case "tools.ozone.moderation.defs#modEventEmail":
                    return new Tools.Ozone.Moderation.ModEventEmail(obj);
                case "tools.ozone.moderation.defs#modEventDivert":
                    return new Tools.Ozone.Moderation.ModEventDivert(obj);
                case "tools.ozone.moderation.defs#modEventTag":
                    return new Tools.Ozone.Moderation.ModEventTag(obj);
                case "tools.ozone.moderation.defs#accountEvent":
                    return new Tools.Ozone.Moderation.AccountEvent(obj);
                case "tools.ozone.moderation.defs#identityEvent":
                    return new Tools.Ozone.Moderation.IdentityEvent(obj);
                case "tools.ozone.moderation.defs#recordEvent":
                    return new Tools.Ozone.Moderation.RecordEvent(obj);
                case "tools.ozone.moderation.defs#repoView":
                    return new Tools.Ozone.Moderation.RepoView(obj);
                case "tools.ozone.moderation.defs#repoViewDetail":
                    return new Tools.Ozone.Moderation.RepoViewDetail(obj);
                case "tools.ozone.moderation.defs#repoViewNotFound":
                    return new Tools.Ozone.Moderation.RepoViewNotFound(obj);
                case "tools.ozone.moderation.defs#recordView":
                    return new Tools.Ozone.Moderation.RecordView(obj);
                case "tools.ozone.moderation.defs#recordViewDetail":
                    return new Tools.Ozone.Moderation.RecordViewDetail(obj);
                case "tools.ozone.moderation.defs#recordViewNotFound":
                    return new Tools.Ozone.Moderation.RecordViewNotFound(obj);
                case "tools.ozone.moderation.defs#moderation":
                    return new Tools.Ozone.Moderation.Moderation(obj);
                case "tools.ozone.moderation.defs#moderationDetail":
                    return new Tools.Ozone.Moderation.ModerationDetail(obj);
                case "tools.ozone.moderation.defs#blobView":
                    return new Tools.Ozone.Moderation.BlobView(obj);
                case "tools.ozone.moderation.defs#imageDetails":
                    return new Tools.Ozone.Moderation.ImageDetails(obj);
                case "tools.ozone.moderation.defs#videoDetails":
                    return new Tools.Ozone.Moderation.VideoDetails(obj);
                case "tools.ozone.moderation.defs#accountHosting":
                    return new Tools.Ozone.Moderation.AccountHosting(obj);
                case "tools.ozone.moderation.defs#recordHosting":
                    return new Tools.Ozone.Moderation.RecordHosting(obj);
                case "tools.ozone.set.defs#set":
                    return new Tools.Ozone.Set.Set(obj);
                case "tools.ozone.set.defs#setView":
                    return new Tools.Ozone.Set.SetView(obj);
                case "tools.ozone.setting.defs#option":
                    return new Tools.Ozone.Setting.Option(obj);
                case "tools.ozone.signature.defs#sigDetail":
                    return new Tools.Ozone.Signature.SigDetail(obj);
                case "tools.ozone.team.defs#member":
                    return new Tools.Ozone.Team.Member(obj);
                case "app.bsky.actor.profile":
                    return new App.Bsky.Actor.Profile(obj);
                case "app.bsky.feed.generator":
                    return new App.Bsky.Feed.Generator(obj);
                case "app.bsky.feed.like":
                    return new App.Bsky.Feed.Like(obj);
                case "app.bsky.feed.post":
                    return new App.Bsky.Feed.Post(obj);
                case "app.bsky.feed.post#replyRef":
                    return new App.Bsky.Feed.ReplyRefDef(obj);
                case "app.bsky.feed.post#entity":
                    return new App.Bsky.Feed.Entity(obj);
                case "app.bsky.feed.post#textSlice":
                    return new App.Bsky.Feed.TextSlice(obj);
                case "app.bsky.feed.postgate":
                    return new App.Bsky.Feed.Postgate(obj);
                case "app.bsky.feed.postgate#disableRule":
                    return new App.Bsky.Feed.DisableRule(obj);
                case "app.bsky.feed.repost":
                    return new App.Bsky.Feed.Repost(obj);
                case "app.bsky.feed.threadgate":
                    return new App.Bsky.Feed.Threadgate(obj);
                case "app.bsky.feed.threadgate#mentionRule":
                    return new App.Bsky.Feed.MentionRule(obj);
                case "app.bsky.feed.threadgate#followingRule":
                    return new App.Bsky.Feed.FollowingRule(obj);
                case "app.bsky.feed.threadgate#listRule":
                    return new App.Bsky.Feed.ListRule(obj);
                case "app.bsky.graph.block":
                    return new App.Bsky.Graph.Block(obj);
                case "app.bsky.graph.follow":
                    return new App.Bsky.Graph.Follow(obj);
                case "app.bsky.graph.list":
                    return new App.Bsky.Graph.List(obj);
                case "app.bsky.graph.listblock":
                    return new App.Bsky.Graph.Listblock(obj);
                case "app.bsky.graph.listitem":
                    return new App.Bsky.Graph.Listitem(obj);
                case "app.bsky.graph.starterpack":
                    return new App.Bsky.Graph.Starterpack(obj);
                case "app.bsky.graph.starterpack#feedItem":
                    return new App.Bsky.Graph.FeedItem(obj);
                case "app.bsky.labeler.service":
                    return new App.Bsky.Labeler.Service(obj);
                case "chat.bsky.actor.declaration":
                    return new Chat.Bsky.Actor.Declaration(obj);
                case "app.bsky.embed.external":
                    return new App.Bsky.Embed.EmbedExternal(obj);
                case "app.bsky.embed.external#external":
                    return new App.Bsky.Embed.External(obj);
                case "app.bsky.embed.external#view":
                    return new App.Bsky.Embed.ViewExternal(obj);
                case "app.bsky.embed.external#viewExternal":
                    return new App.Bsky.Embed.ViewExternalExternal(obj);
                case "app.bsky.embed.images":
                    return new App.Bsky.Embed.EmbedImages(obj);
                case "app.bsky.embed.images#image":
                    return new App.Bsky.Embed.Image(obj);
                case "app.bsky.embed.images#view":
                    return new App.Bsky.Embed.ViewImages(obj);
                case "app.bsky.embed.images#viewImage":
                    return new App.Bsky.Embed.ViewImage(obj);
                case "app.bsky.embed.record":
                    return new App.Bsky.Embed.EmbedRecord(obj);
                case "app.bsky.embed.record#view":
                    return new App.Bsky.Embed.ViewRecordDef(obj);
                case "app.bsky.embed.record#viewRecord":
                    return new App.Bsky.Embed.ViewRecord(obj);
                case "app.bsky.embed.record#viewNotFound":
                    return new App.Bsky.Embed.ViewNotFound(obj);
                case "app.bsky.embed.record#viewBlocked":
                    return new App.Bsky.Embed.ViewBlocked(obj);
                case "app.bsky.embed.record#viewDetached":
                    return new App.Bsky.Embed.ViewDetached(obj);
                case "app.bsky.embed.recordWithMedia":
                    return new App.Bsky.Embed.RecordWithMedia(obj);
                case "app.bsky.embed.recordWithMedia#view":
                    return new App.Bsky.Embed.ViewRecordWithMedia(obj);
                case "app.bsky.embed.video":
                    return new App.Bsky.Embed.EmbedVideo(obj);
                case "app.bsky.embed.video#caption":
                    return new App.Bsky.Embed.Caption(obj);
                case "app.bsky.embed.video#view":
                    return new App.Bsky.Embed.ViewVideo(obj);
                case "app.bsky.feed.describeFeedGenerator#feed":
                    return new App.Bsky.Feed.Feed(obj);
                case "app.bsky.feed.describeFeedGenerator#links":
                    return new App.Bsky.Feed.Links(obj);
                case "app.bsky.feed.getLikes#like":
                    return new App.Bsky.Feed.LikeDef(obj);
                case "app.bsky.notification.listNotifications#notification":
                    return new App.Bsky.Notification.Notification(obj);
                case "app.bsky.richtext.facet":
                    return new App.Bsky.Richtext.Facet(obj);
                case "app.bsky.richtext.facet#mention":
                    return new App.Bsky.Richtext.Mention(obj);
                case "app.bsky.richtext.facet#link":
                    return new App.Bsky.Richtext.Link(obj);
                case "app.bsky.richtext.facet#tag":
                    return new App.Bsky.Richtext.Tag(obj);
                case "app.bsky.richtext.facet#byteSlice":
                    return new App.Bsky.Richtext.ByteSlice(obj);
                case "app.bsky.unspecced.getTaggedSuggestions#suggestion":
                    return new App.Bsky.Unspecced.Suggestion(obj);
                case "chat.bsky.convo.sendMessageBatch#batchItem":
                    return new Chat.Bsky.Convo.BatchItem(obj);
                case "chat.bsky.moderation.getActorMetadata#metadata":
                    return new Chat.Bsky.Moderation.Metadata(obj);
                case "com.atproto.label.subscribeLabels#labels":
                    return new Com.Atproto.Label.Labels(obj);
                case "com.atproto.label.subscribeLabels#info":
                    return new Com.Atproto.Label.Info(obj);
                case "com.atproto.repo.applyWrites#create":
                    return new Com.Atproto.Repo.Create(obj);
                case "com.atproto.repo.applyWrites#update":
                    return new Com.Atproto.Repo.Update(obj);
                case "com.atproto.repo.applyWrites#delete":
                    return new Com.Atproto.Repo.Delete(obj);
                case "com.atproto.repo.applyWrites#createResult":
                    return new Com.Atproto.Repo.CreateResult(obj);
                case "com.atproto.repo.applyWrites#updateResult":
                    return new Com.Atproto.Repo.UpdateResult(obj);
                case "com.atproto.repo.applyWrites#deleteResult":
                    return new Com.Atproto.Repo.DeleteResult(obj);
                case "com.atproto.repo.listMissingBlobs#recordBlob":
                    return new Com.Atproto.Repo.RecordBlob(obj);
                case "com.atproto.repo.listRecords#record":
                    return new Com.Atproto.Repo.Record(obj);
                case "com.atproto.repo.strongRef":
                    return new Com.Atproto.Repo.StrongRef(obj);
                case "com.atproto.server.createAppPassword#appPassword":
                    return new Com.Atproto.Server.AppPassword(obj);
                case "com.atproto.server.createInviteCodes#accountCodes":
                    return new Com.Atproto.Server.AccountCodes(obj);
                case "com.atproto.server.describeServer#links":
                    return new Com.Atproto.Server.Links(obj);
                case "com.atproto.server.describeServer#contact":
                    return new Com.Atproto.Server.Contact(obj);
                case "com.atproto.server.listAppPasswords#appPassword":
                    return new Com.Atproto.Server.AppPasswordDef(obj);
                case "com.atproto.sync.listRepos#repo":
                    return new Com.Atproto.Sync.Repo(obj);
                case "com.atproto.sync.subscribeRepos#commit":
                    return new Com.Atproto.Sync.Commit(obj);
                case "com.atproto.sync.subscribeRepos#identity":
                    return new Com.Atproto.Sync.Identity(obj);
                case "com.atproto.sync.subscribeRepos#account":
                    return new Com.Atproto.Sync.Account(obj);
                case "com.atproto.sync.subscribeRepos#handle":
                    return new Com.Atproto.Sync.Handle(obj);
                case "com.atproto.sync.subscribeRepos#migrate":
                    return new Com.Atproto.Sync.Migrate(obj);
                case "com.atproto.sync.subscribeRepos#tombstone":
                    return new Com.Atproto.Sync.Tombstone(obj);
                case "com.atproto.sync.subscribeRepos#info":
                    return new Com.Atproto.Sync.Info(obj);
                case "com.atproto.sync.subscribeRepos#repoOp":
                    return new Com.Atproto.Sync.RepoOp(obj);
                case "tools.ozone.server.getConfig#serviceConfig":
                    return new Tools.Ozone.Server.ServiceConfig(obj);
                case "tools.ozone.server.getConfig#viewerConfig":
                    return new Tools.Ozone.Server.ViewerConfig(obj);
                case "tools.ozone.signature.findRelatedAccounts#relatedAccount":
                    return new Tools.Ozone.Signature.RelatedAccount(obj);
                default:
                    return null;
            }
        }

        public static List<ATObject?>? ToATObjectList(this CBORObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            var list = new List<ATObject?>();
            foreach (var item in obj.Values)
            {
                list.Add(item.ToATObject());
            }
            return list;
        }
    }
}

