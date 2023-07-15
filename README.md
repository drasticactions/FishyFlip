# FishyFlip - a .NET ATProtocol/Bluesky Library

<img alt="FishyFlip Logo" src="assets/FishyFlipLogo.png?raw=true" width=250 />

![1444070256569233](https://user-images.githubusercontent.com/898335/167266846-1ad2648f-91c1-4a04-a18d-6dd4d6c7d21c.gif)

FishyFlip is an implementation of [ATProtocol](https://atproto.com/) for .NET, forked from [bluesky-net](https://github.com/dariogriffo/bluesky-net).

It is currently under construction.

❌ - Unsupported
⚠️ - Partial support, untested
✅ - Should be working

### Sync

| Endpoint | Implemented
|----------|----------|
| [com.atproto.sync.getBlob](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncgetblob)  | ✅  |
| [com.atproto.sync.getBlocks](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncgetblocks)  | ✅  |
| [com.atproto.sync.getCheckout](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncgetcheckout)  | ✅  |
| [com.atproto.sync.getCommitPath](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncgetcommitpath)  | ✅  |
| [com.atproto.sync.getHead](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncgethead)  | ✅  |
| [com.atproto.sync.getRecord](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncgetrecord)  | ✅  |
| [com.atproto.sync.getRepo](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncgetrepo)  | ✅  |
| [com.atproto.sync.listBlobs](https://atproto.com/lexicons/com-atproto-sync#comatprotosynclistblobs)  | ✅  |
| [com.atproto.sync.listRepos](https://atproto.com/lexicons/com-atproto-sync#comatprotosynclistrepos)  | ✅  |
| [com.atproto.sync.notifyOfUpdate](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncnotifyofupdate)  | ⚠️ | 
| [com.atproto.sync.requestCrawl](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncrequestcrawl)  | ⚠️ | 
| [com.atproto.sync.subscribeRepos](https://atproto.com/lexicons/com-atproto-sync#comatprotosyncsubscriberepos)  | ✅  | 

### Actor

| Endpoint | Implemented
|----------|----------|
| [app.bsky.actor.getProfile](https://atproto.com/lexicons/app-bsky-actor#appbskyactorgetprofile)  | ✅  |
| [app.bsky.actor.getProfiles](https://atproto.com/lexicons/app-bsky-actor#appbskyactorgetprofiles)  | ✅  |
| [app.bsky.actor.getSuggestions](https://atproto.com/lexicons/app-bsky-actor#appbskyactorgetsuggestions)  | ✅  |
| [app.bsky.actor.searchActors](https://atproto.com/lexicons/app-bsky-actor#appbskyactorsearchactors)  | ✅  |
| [app.bsky.actor.searchActorsTypeahead](https://atproto.com/lexicons/app-bsky-actor#appbskyactorsearchactorstypeahead)  | ✅  |

### Feed

| Endpoint | Implemented
|----------|----------|
| [app.bsky.feed.getAuthorFeed](https://atproto.com/lexicons/app-bsky-feed#appbskyfeedgetauthorfeed)  | ✅  |
| [app.bsky.feed.getLikes](https://atproto.com/lexicons/app-bsky-feed#appbskyfeedgetlikes)  | ✅  |
| [app.bsky.feed.getPostThread](https://atproto.com/lexicons/app-bsky-feed#appbskyfeedgetpostthread)  | ✅  |
| [app.bsky.feed.getPosts](https://atproto.com/lexicons/app-bsky-feed#appbskyfeedgetposts)  | ✅  |
| [app.bsky.feed.getRepostedBy](https://atproto.com/lexicons/app-bsky-feed#appbskyfeedgetrepostedby)  | ✅  |
| [app.bsky.feed.getTimeline](https://atproto.com/lexicons/app-bsky-feed#appbskyfeedgettimeline)  | ✅  |
| [app.bsky.feed.getFeedSkeleton](https://atproto.com/lexicons/app-bsky-feed#appbskyfeedgetfeedskeleton)  | ❌  |


### Graph

| Endpoint | Implemented
|----------|----------|
| [app.bsky.graph.getBlocks](https://atproto.com/lexicons/app-bsky-graph#appbskygraphgetblocks)  | ✅  |
| [app.bsky.graph.getFollowers](https://atproto.com/lexicons/app-bsky-graph#appbskygraphgetfollowers)  | ✅  |
| [app.bsky.graph.getFollows](https://atproto.com/lexicons/app-bsky-graph#appbskygraphgetfollows)  | ✅  |
| [app.bsky.graph.getMutes](https://atproto.com/lexicons/app-bsky-graph#appbskygraphgetmutes)  | ✅  |
| [app.bsky.graph.muteActor](https://atproto.com/lexicons/app-bsky-graph#appbskygraphmuteactor)  | ✅  |
| [app.bsky.graph.unmuteActor](https://atproto.com/lexicons/app-bsky-graph#appbskygraphunmuteactor)  | ✅  |

### Notification

| Endpoint | Implemented
|----------|----------|
| [app.bsky.notification.getUnreadCount](https://atproto.com/lexicons/app-bsky-notification#appbskynotificationgetunreadcount)  | ✅  |
| [app.bsky.notification.listNotifications](https://atproto.com/lexicons/app-bsky-notification#appbskynotificationlistnotifications)  | ✅  |
| [app.bsky.notification.updateSeen](https://atproto.com/lexicons/app-bsky-notification#appbskynotificationupdateseen)  | ✅  |

### Server

| Endpoint | Implemented
|----------|----------|
| [com.atproto.server.createAccount](https://atproto.com/lexicons/com-atproto-server#comatprotoservercreateaccount)  | ❌  |
| [com.atproto.server.createAppPassword](https://atproto.com/lexicons/com-atproto-server#comatprotoservercreateapppassword)  | ❌  |
| [com.atproto.server.createInviteCode](https://atproto.com/lexicons/com-atproto-server#comatprotoservercreateinvitecode)  | ✅  |
| [com.atproto.server.createInviteCodes](https://atproto.com/lexicons/com-atproto-server#comatprotoservercreateinvitecodes)  | ✅  |
| [com.atproto.server.createSession](https://atproto.com/lexicons/com-atproto-server#comatprotoservercreatesession)  | ✅  |
| [com.atproto.server.deleteAccount](https://atproto.com/lexicons/com-atproto-server#comatprotoserverdeleteaccount)  | ❌  |
| [com.atproto.server.deleteSession](https://atproto.com/lexicons/com-atproto-server#comatprotoserverdeletesession)  | ❌  |
| [com.atproto.server.describeServer](https://atproto.com/lexicons/com-atproto-server#comatprotoserverdescribeserver)  | ✅  |
| [com.atproto.server.getAccountInviteCodes](https://atproto.com/lexicons/com-atproto-server#comatprotoservergetaccountinvitecodes)  | ✅  |
| [com.atproto.server.getSession](https://atproto.com/lexicons/com-atproto-server#comatprotoservergetsession)  | ✅  |
| [com.atproto.server.listAppPasswords](https://atproto.com/lexicons/com-atproto-server#comatprotoserverlistapppasswords)  | ✅  |
| [com.atproto.server.refreshSession](https://atproto.com/lexicons/com-atproto-server#comatprotoserverrefreshsession)  | ✅  |
| [com.atproto.server.requestAccountDelete](https://atproto.com/lexicons/com-atproto-server#comatprotoserverrequestaccountdelete)  | ❌  |
| [com.atproto.server.requestPasswordReset](https://atproto.com/lexicons/com-atproto-server#comatprotoserverrequestpasswordreset)  | ❌  |
| [com.atproto.server.resetPassword](https://atproto.com/lexicons/com-atproto-server#comatprotoserverresetpassword)  | ❌  |
| [com.atproto.server.revokeAppPassword](https://atproto.com/lexicons/com-atproto-server#comatprotoserverrevokeapppassword)  | ❌  |

### Repo

| Endpoint | Implemented
|----------|----------|
| [com.atproto.repo.applyWrites](https://atproto.com/lexicons/com-atproto-repo#comatprotorepoapplywrites)  | ❌  |
| [com.atproto.repo.createRecord](https://atproto.com/lexicons/com-atproto-repo#comatprotorepocreaterecord)  | ✅  |
| [com.atproto.repo.deleteRecord](https://atproto.com/lexicons/com-atproto-repo#comatprotorepodeleterecord)  | ✅  |
| [com.atproto.repo.describeRepo](https://atproto.com/lexicons/com-atproto-repo#comatprotorepodescriberepo)  | ✅  |
| [com.atproto.repo.getRecord](https://atproto.com/lexicons/com-atproto-repo#comatprotorepogetrecord)  | ✅  |
| [com.atproto.repo.listRecords](https://atproto.com/lexicons/com-atproto-repo#comatprotorepolistrecords)  | ✅  |
| [com.atproto.repo.putRecord](https://atproto.com/lexicons/com-atproto-repo#comatprotorepoputrecord)  | ❌  |
| [com.atproto.repo.uploadBlob](https://atproto.com/lexicons/com-atproto-repo#comatprotorepouploadblob)  | ✅  |
### Moderation

| Endpoint | Implemented
|----------|----------|
| [com.atproto.moderation.createReport](https://atproto.com/lexicons/com-atproto-moderation#comatprotomoderationcreatereport) | ✅  |

### Labels

| Endpoint | Implemented
|----------|----------|
| [com.atproto.label.queryLabels](https://atproto.com/lexicons/com-atproto-label#comatprotolabelquerylabels)  | ⚠️  |
| [com.atproto.label.subscribeLabels](https://atproto.com/lexicons/com-atproto-label#comatprotolabelsubscribelabels)  | ⚠️  |

### Identity

| Endpoint | Implemented
|----------|----------|
| [com.atproto.identity.resolveHandle](https://atproto.com/lexicons/com-atproto-identity#comatprotoidentityresolvehandle)  | ✅  |
| [com.atproto.identity.updateHandle](https://atproto.com/lexicons/com-atproto-identity#comatprotoidentityupdatehandle) | ✅  |

### Admin

| Endpoint | Implemented
|----------|----------|
| [com.atproto.admin.disableInviteCodes](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmindisableinvitecodes)    | ❌   |
| [com.atproto.admin.getInviteCodes](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmingetinvitecodes)   | ❌   |
| [com.atproto.admin.getModerationAction](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmingetmoderationaction) | ❌   |
| [com.atproto.admin.getModerationActions](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmingetmoderationactions) | ❌  |
| [com.atproto.admin.getModerationReport](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmingetmoderationreport) | ❌  |
| [com.atproto.admin.getModerationReports](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmingetmoderationreports) | ❌  |
| [com.atproto.admin.getRecord](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmingetrecord) | ❌  |
| [com.atproto.admin.getRepo](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmingetrepo) | ❌  |
| [com.atproto.admin.resolveModerationReports](https://atproto.com/lexicons/com-atproto-admin#comatprotoadminresolvemoderationreports)  | ❌  |
| [com.atproto.admin.reverseModerationAction](https://atproto.com/lexicons/com-atproto-admin#comatprotoadminreversemoderationaction)  | ❌  |
| [com.atproto.admin.searchRepos](https://atproto.com/lexicons/com-atproto-admin#comatprotoadminsearchrepos)  | ❌  |
| [com.atproto.admin.takeModerationAction](https://atproto.com/lexicons/com-atproto-admin#comatprotoadmintakemoderationaction)  | ❌  |
| [com.atproto.admin.updateAccountEmail](https://atproto.com/lexicons/com-atproto-admin#comatprotoadminupdateaccountemail)  | ❌  |
| [com.atproto.admin.updateAccountHandle](https://atproto.com/lexicons/com-atproto-admin#comatprotoadminupdateaccounthandle)  | ❌  |

<!-- ###

| Endpoint | Implemented
|----------|----------|
|   |   | -->
