# FishyFlip - a .NET ATProtocol/Bluesky Library

[![NuGet Version](https://img.shields.io/nuget/v/FishyFlip.svg)](https://www.nuget.org/packages/FishyFlip/) ![License](https://img.shields.io/badge/License-MIT-blue.svg)

![FishyFlip Logo](https://user-images.githubusercontent.com/898335/253740405-4b0ae177-cc49-4c26-b6b0-ab8e835a0e62.png)

![1444070256569233](https://user-images.githubusercontent.com/898335/167266846-1ad2648f-91c1-4a04-a18d-6dd4d6c7d21c.gif)

FishyFlip is an implementation of [ATProtocol](https://atproto.com/) for .NET, forked from [bluesky-net](https://github.com/dariogriffo/bluesky-net).

It is currently under construction.

For a Blazor WASM demo, check out https://drasticactions.github.io/FishyFlip

### Third-Party Libraries

- [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning)

FishyFlip

- Forked from [bluesky-net](https://github.com/dariogriffo/bluesky-net).
- [CBOR](https://github.com/peteroupc/CBOR)
- [net-ipfs-core](https://github.com/ipfs-shipyard/net-ipfs-core)
- [OneOf](https://github.com/mcintyre321/OneOf)

bskycli

- [CommandLineParser](https://github.com/commandlineparser/commandline)


## How To Use

- Use `ATProtocolBuilder` to build a new instance of `ATProtocol`

```csharp
// Include a ILogger if you want additional logging from the base library.
var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATProtocolBuilder()
    .EnableAutoRenewSession(true)
// Set the instance URL for the PDS you wish to connect to.
// Defaults to bsky.social.
    .WithInstanceUrl(new Uri("https://drasticactions.ninja"))
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();
```

- Once created, you can now access unauthenticated APIs. For example, to get a list of posts from a user...

```csharp
// Calls com.atproto.repo.listRecords for da-admin.drasticactions.ninja.
// ATHandle and ATDid are identifiers and can be used for most endpoints,
// such as for ListRecord points like below.
var listRecords = await atProtocol.Repo.ListPostAsync(ATHandle.Create("da-admin.drasticactions.ninja"));

// Each endpoint returns a Result<T>.
// This was originally taken from bluesky-net, which itself took it from OneOf.
// This is a pattern match object which can either be the "Success" object, 
// or an "Error" object. The "Error" object will always be the type of "Error" and always be from the Bluesky API.
// This would be where you would handle things like authentication errors and the like.
listRecords.Switch(
    success => { 
        foreach(var post in success!.Records)
        {
            // Prints the CID and ATURI of each post.
            Console.WriteLine($"CID: {post.Cid} Uri: {post.Uri}");
            // Value is `ATRecord`, a base type.
            // We can check if it's a Post and get its true value.
            if (post.Value is Post atPost)
            {
                Console.WriteLine(atPost.Text);
            }
        }
    },
    error =>
    {
        Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
    }
);
```

- Instead of pattern matching, you can also use `.HandleResult()` to return the `success` object, and throw an exception upon an `error`.

```csharp
var listRecords = (await atProtocol.Repo.ListPostAsync(ATHandle.Create("da-admin.drasticactions.ninja"))).HandleResult();
```

- To log in, we need to create a session. This is applied to all `ATProtocol` calls once applied. If you need to create calls from a non-auth user session, create a new `ATProtocol` or destroy the existing session.

```csharp
// While this accepts normal passwords, you should ask users
// to create an app password from their accounts to use it instead.
Result<Session> result = await atProtocol.Server.CreateSessionAsync(userName, password, CancellationToken.None);

result.Switch(
    success =>
    {
        // Contains the session information and tokens used internally.
        Console.WriteLine($"Session: {success.Did}");
    },
    error =>
    {
        Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
    }
);
```

```csharp
// Creates a text post of "Hello, World!" to the signed in users account.
var postResult = await atProtocol.Repo.CreatePostAsync("Hello, World!");
postResult.Switch(
    success =>
    {
        // Contains the ATUri and CID.
        Console.WriteLine($"Post: {success.Uri} {success.Cid}");
    },
    error =>
    {
        Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
    }
);
```

- To upload an image, you need to first upload it as a blob, and then attach it to a post. You can also embed links in text by setting a "Link" Facet.

```csharp
var stream = File.OpenRead("path/to/image.png");
var content = new StreamContent(stream);
content.Headers.ContentLength = stream.Length;
// Bluesky uses the content type header for setting the blob type.
// As of this writing, it does not verify what kind of blob gets uploaded.
// But you should be careful about setting generic types or using the wrong one.
// If you do not set a type, it will return an error.
content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
var blobResult = await atProtocol.Repo.UploadBlobAsync(content);
await blobResult.SwitchAsync(
       async success =>
       {
           // Blob is uploaded.
           Console.WriteLine($"Blob: {success.Blob.Type}");
           // Converts the blob to an image.
           Image? image = success.Blob.ToImage();

           var prompt = "Hello, Image! Link Goes Here!";

           // To insert a link, we need to find the start and end of the link text.
           // This is done as a "ByteSlice."
           int promptStart = prompt.IndexOf("Link Goes Here!", StringComparison.InvariantCulture);
           int promptEnd = promptStart + Encoding.Default.GetBytes("Link Goes Here!").Length;
           var index = new FacetIndex(promptStart, promptEnd);
           var link = FacetFeature.CreateLink("https://drasticactions.dev");
           var facet = new Facet(index, link);

           // Create a post with the image and the link.
           var postResult = await atProtocol.Repo.CreatePostAsync(prompt, new[] { facet }, new ImagesEmbed(image, "Optional Alt Text, you should have your users set this when possible"));
       },
       async error =>
       {
            Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
       }
);
```

You should then see your image and link.

![Post Sample](https://user-images.githubusercontent.com/898335/253740484-57addcb6-523c-4b65-914d-495ddf8e1474.png)


- You can access the "Firehose" by using `SubscribeRepos`. This can be seen in the `FishyFlip.Firehose` sample. SubscribeRepos uses Websockets to connect to a given instead and get messages whenever a new one is posted. Messages need to be handled outside of the general WebSocket stream; if anything blocks the stream from returning messages, you may see errors from the protocol saying your connection is too slow.

```csharp
var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATWebSocketProtocolBuilder()
// Defaults to bsky.network.
    .WithInstanceUrl(new Uri("https://drasticactions.ninja"))
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

atProtocol.OnSubscribedRepoMessage += (sender, args) =>
{
    Task.Run(() => HandleMessageAsync(args.Message)).FireAndForgetSafeAsync();
};

await atProtocol.StartSubscribeReposAsync();

var key = Console.ReadKey();

await atProtocol.StopSubscriptionAsync();

async Task HandleMessageAsync(SubscribeRepoMessage message)
{
    if (message.Commit is null)
    {
        return;
    }

    var orgId = message.Commit.Repo;

    if (orgId is null)
    {
        return;
    }

    if (message.Record is not null)
    {
        Console.WriteLine($"Record: {message.Record.Type}");
    }
}
```

- `Sync` endpoints generally encode their output as [IPFS Car](https://car.ipfs.io/) files. Here, we can process them as they are streaming so instead of needing to download a whole file to process it, we can do it as it is downloading. This is done by using the `OnCarDecoded` delegate.

```csharp
var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATProtocolBuilder()
    .EnableAutoRenewSession(true)
    .WithInstanceUrl(new Uri("https://drasticactions.ninja"))
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

var checkoutResult = await atProtocol.Sync.GetCheckoutAsync(ATDid.Create("did:plc:yhgc5rlqhoezrx6fbawajxlh"), HandleProgressStatus);

async void HandleProgressStatus(CarProgressStatusEvent e)
{
    var cid = e.Cid;
    var bytes = e.Bytes;
    var test = CBORObject.DecodeFromBytes(bytes);
    var record = ATRecord.FromCBORObject(test);
    // Prints the type of the record.
    Console.WriteLine(record?.Type);
}
```

For more samples, check the `apps`, `samples`, and `website` directory.

## Endpoints

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
| [app.bsky.feed.searchPosts](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/searchPosts.json)  | ❌  |
| [app.bsky.feed.getPosts](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getPosts.json)  | ✅  |
| [app.bsky.feed.getFeed](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getFeed.json)  | ✅  |
| [app.bsky.feed.getFeedSkeleton](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getFeedSkeleton.json)  | ❌  |
| [app.bsky.feed.getListFeed](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getListFeed.json)  | ✅  |
| [app.bsky.feed.getSuggestedFeeds](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getSuggestedFeeds.json)  | ✅  |
| [app.bsky.feed.getActorFeeds](https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/getActorFeeds.json)  | ❌  |


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

## Why "FishyFlip?"

"FishyFlip" is a reference to the [Your Kickstarter Sucks](https://open.spotify.com/episode/5upEtr0tHBf6SoXjJwG5UJ) episode of the same name.

![Discord Image](https://user-images.githubusercontent.com/898335/253739935-c6127d97-bd4a-44a6-9c95-5e0db5ce9e23.png)