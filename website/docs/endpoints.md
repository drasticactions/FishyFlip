# Endpoints

As a general rule of thumb, `com.atproto` endpoints (such as `com.atproto.sync`) do not require authentication, where `app.bsky` ones do.

❌ - Not Implemented
⚠️ - Partial support, untested
✅ - Should be "working"

### Sync

| Endpoint | Implemented
|----------|----------|
| [com.atproto.sync.getHead](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/getHead.json)  | ✅  |
| [com.atproto.sync.getBlob](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/getBlob.json)  | ✅  |
| [com.atproto.sync.getRepo](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/getRepo.json)  | ✅  |
| [com.atproto.sync.notifyOfUpdate](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/notifyOfUpdate.json)  | ✅  |
| [com.atproto.sync.requestCrawl](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/requestCrawl.json)  | ✅  |
| [com.atproto.sync.listBlobs](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/listBlobs.json)  | ✅  |
| [com.atproto.sync.getLatestCommit](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/getLatestCommit.json)  | ✅  |
| [com.atproto.sync.subscribeRepos](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/subscribeRepos.json)  | ✅  |
| [com.atproto.sync.getRecord](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/getRecord.json)  | ✅  |
| [com.atproto.sync.listRepos](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/listRepos.json)  | ✅  |
| [com.atproto.sync.getBlocks](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/getBlocks.json)  | ✅  |
| [com.atproto.sync.getCheckout](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/sync/getCheckout.json)  | ✅  |

### Actor

| Endpoint | Implemented
|----------|----------|
| [app.bsky.actor.searchActorsTypeahead](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/actor/searchActorsTypeahead.json)  | ✅  |
| [app.bsky.actor.putPreferences](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/actor/putPreferences.json)  | ❌  |
| [app.bsky.actor.getProfile](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/actor/getProfile.json)  | ✅  |
| [app.bsky.actor.getSuggestions](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/actor/getSuggestions.json)  | ✅  |
| [app.bsky.actor.searchActors](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/actor/searchActors.json)  | ✅  |
| [app.bsky.actor.getProfiles](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/actor/getProfiles.json)  | ✅  |
| [app.bsky.actor.getPreferences](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/actor/getPreferences.json)  | ❌  |

### Feed

| Endpoint | Implemented
|----------|----------|
| [app.bsky.feed.getFeedGenerators](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getFeedGenerators.json)  | ✅  |
| [app.bsky.feed.getTimeline](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getTimeline.json)  | ✅  |
| [app.bsky.feed.getFeedGenerator](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getFeedGenerator.json)  | ✅  |
| [app.bsky.feed.getAuthorFeed](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getAuthorFeed.json)  | ✅  |
| [app.bsky.feed.getLikes](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getLikes.json)  | ✅  |
| [app.bsky.feed.getPostThread](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getPostThread.json)  | ✅  |
| [app.bsky.feed.getActorLikes](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getActorLikes.json)  | ✅  |
| [app.bsky.feed.getRepostedBy](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getRepostedBy.json)  | ✅  |
| [app.bsky.feed.describeFeedGenerator](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/describeFeedGenerator.json)  | ❌  |
| [app.bsky.feed.searchPosts](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/searchPosts.json)  | ✅  |
| [app.bsky.feed.getPosts](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getPosts.json)  | ✅  |
| [app.bsky.feed.getFeed](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getFeed.json)  | ✅  |
| [app.bsky.feed.getFeedSkeleton](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getFeedSkeleton.json)  | ❌  |
| [app.bsky.feed.getListFeed](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getListFeed.json)  | ✅  |
| [app.bsky.feed.getSuggestedFeeds](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getSuggestedFeeds.json)  | ✅  |
| [app.bsky.feed.getActorFeeds](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getActorFeeds.json)  | ✅  |


### Graph

| Endpoint | Implemented
|----------|----------|
| [app.bsky.graph.getSuggestedFollowsByActor](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getSuggestedFollowsByActor.json)  | ✅  |
| [app.bsky.graph.unmuteActorList](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/unmuteActorList.json)  | ✅  |
| [app.bsky.graph.getListBlocks](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getListBlocks.json)  | ✅  |
| [app.bsky.graph.muteActorList](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/muteActorList.json)  | ✅  |
| [app.bsky.graph.getLists](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getLists.json)  | ✅  |
| [app.bsky.graph.getFollowers](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getFollowers.json)  | ✅  |
| [app.bsky.graph.muteActor](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/muteActor.json)  | ✅  |
| [app.bsky.graph.getMutes](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getMutes.json)  | ✅  |
| [app.bsky.graph.getListMutes](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getListMutes.json)  | ✅  |
| [app.bsky.graph.getFollows](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getFollows.json)  | ✅  |
| [app.bsky.graph.getBlocks](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getBlocks.json)  | ✅  |
| [app.bsky.graph.unmuteActor](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/unmuteActor.json)  | ✅  |
| [app.bsky.graph.getList](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/graph/getList.json)  | ✅  |

### Notification

| Endpoint | Implemented
|----------|----------|
| [app.bsky.notification.registerPush](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/notification/registerPush.json)  | ❌  |
| [app.bsky.notification.updateSeen](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/notification/updateSeen.json)  | ✅  |
| [app.bsky.notification.listNotifications](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/notification/listNotifications.json)  | ✅  |
| [app.bsky.notification.getUnreadCount](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/notification/getUnreadCount.json)  | ✅  |

### Server

| Endpoint | Implemented
|----------|----------|
| [com.atproto.server.requestEmailConfirmation](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/requestEmailConfirmation.json)  | ❌  |
| [com.atproto.server.reserveSigningKey](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/reserveSigningKey.json)  | ❌  |
| [com.atproto.server.getAccountInviteCodes](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/getAccountInviteCodes.json)  | ✅  |
| [com.atproto.server.createSession](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/createSession.json)  | ✅  |
| [com.atproto.server.listAppPasswords](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/listAppPasswords.json)  | ✅  |
| [com.atproto.server.createInviteCodes](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/createInviteCodes.json)  | ✅  |
| [com.atproto.server.deleteSession](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/deleteSession.json)  | ❌  |
| [com.atproto.server.revokeAppPassword](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/revokeAppPassword.json)  | ❌  |
| [com.atproto.server.createAppPassword](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/createAppPassword.json)  | ❌  |
| [com.atproto.server.describeServer](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/describeServer.json)  | ✅  |
| [com.atproto.server.confirmEmail](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/confirmEmail.json)  | ❌  |
| [com.atproto.server.getSession](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/getSession.json)  | ✅  |
| [com.atproto.server.refreshSession](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/refreshSession.json)  | ✅  |
| [com.atproto.server.updateEmail](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/updateEmail.json)  | ❌  |
| [com.atproto.server.resetPassword](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/resetPassword.json)  | ❌  |
| [com.atproto.server.requestEmailUpdate](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/requestEmailUpdate.json)  | ❌  |
| [com.atproto.server.requestPasswordReset](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/requestPasswordReset.json)  | ❌  |
| [com.atproto.server.requestAccountDelete](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/requestAccountDelete.json)  | ❌  |
| [com.atproto.server.createAccount](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/createAccount.json)  | ❌  |
| [com.atproto.server.deleteAccount](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/deleteAccount.json)  | ❌  |
| [com.atproto.server.createInviteCode](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/server/createInviteCode.json)  | ✅  |

### Repo

| Endpoint | Implemented
|----------|----------|
| [com.atproto.repo.createRecord](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/createRecord.json)  | ✅  |
| [com.atproto.repo.deleteRecord](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/deleteRecord.json)  | ✅  |
| [com.atproto.repo.putRecord](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/putRecord.json)  | ✅  |
| [com.atproto.repo.uploadBlob](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/uploadBlob.json)  | ✅  |
| [com.atproto.repo.describeRepo](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/describeRepo.json)  | ✅  |
| [com.atproto.repo.getRecord](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/getRecord.json)  | ✅  |
| [com.atproto.repo.applyWrites](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/applyWrites.json)  | ❌  |
| [com.atproto.repo.listRecords](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/repo/listRecords.json)  | ✅  |

### Moderation

| Endpoint | Implemented
|----------|----------|
| [com.atproto.moderation.createReport](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/moderation/createReport.json)  | ✅  |

### Labels

| Endpoint | Implemented
|----------|----------|
| [com.atproto.label.subscribeLabels](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/label/subscribeLabels.json)  | ⚠️  |
| [com.atproto.label.queryLabels](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/label/queryLabels.json)  | ⚠️  |

### Identity

| Endpoint | Implemented
|----------|----------|
| [com.atproto.identity.updateHandle](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/identity/updateHandle.json)  | ✅  |
| [com.atproto.identity.resolveHandle](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/identity/resolveHandle.json)  | ✅  |

### Admin

| Endpoint | Implemented
|----------|----------|
| [com.atproto.admin.getRepo](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/getRepo.json)  | ❌  |
| [com.atproto.admin.updateAccountEmail](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/updateAccountEmail.json)  | ❌  |
| [com.atproto.admin.getAccountInfo](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/getAccountInfo.json)  | ❌  |
| [com.atproto.admin.getSubjectStatus](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/getSubjectStatus.json)  | ❌  |
| [com.atproto.admin.queryModerationStatuses](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/queryModerationStatuses.json)  | ❌  |
| [com.atproto.admin.updateAccountHandle](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/updateAccountHandle.json)  | ❌  |
| [com.atproto.admin.getInviteCodes](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/getInviteCodes.json)  | ❌  |
| [com.atproto.admin.enableAccountInvites](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/enableAccountInvites.json)  | ❌  |
| [com.atproto.admin.disableAccountInvites](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/disableAccountInvites.json)  | ❌  |
| [com.atproto.admin.disableInviteCodes](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/disableInviteCodes.json)  | ❌  |
| [com.atproto.admin.updateSubjectStatus](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/updateSubjectStatus.json)  | ❌  |
| [com.atproto.admin.emitModerationEvent](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/emitModerationEvent.json)  | ❌  |
| [com.atproto.admin.getModerationEvent](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/getModerationEvent.json)  | ❌  |
| [com.atproto.admin.getRecord](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/getRecord.json)  | ❌  |
| [com.atproto.admin.queryModerationEvents](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/queryModerationEvents.json)  | ❌  |
| [com.atproto.admin.sendEmail](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/sendEmail.json)  | ❌  |
| [com.atproto.admin.searchRepos](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/searchRepos.json)  | ❌  |
| [com.atproto.admin.getAccountInfos](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/getAccountInfos.json)  | ❌  |
| [com.atproto.admin.deleteAccount](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/admin/deleteAccount.json)  | ❌  |

### Unspecced
| Endpoint | Implemented
|----------|----------|
| [app.bsky.unspecced.searchActorsSkeleton](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/unspecced/searchActorsSkeleton.json)  | ❌  |
| [app.bsky.unspecced.searchPostsSkeleton](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/unspecced/searchPostsSkeleton.json)  | ❌  |
| [app.bsky.unspecced.getPopularFeedGenerators](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/unspecced/getPopularFeedGenerators.json)  | ✅  |
| [app.bsky.unspecced.getTimelineSkeleton](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/unspecced/getTimelineSkeleton.json)  | ❌  |

### Temp

| Endpoint | Implemented
|----------|----------|
| [com.atproto.temp.transferAccount](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/temp/transferAccount.json)  | ❌  |
| [com.atproto.temp.pushBlob](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/temp/pushBlob.json)  | ❌  |
| [com.atproto.temp.importRepo](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/temp/importRepo.json)  | ❌  |
| [com.atproto.temp.fetchLabels](https://github.com/bluesky-social/atproto/blob/main/lexicons/com/atproto/temp/fetchLabels.json)  | ❌  |

<!-- ###

| Endpoint | Implemented
|----------|----------|
|   |   | -->