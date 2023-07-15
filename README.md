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
// You can get around this by using `.AsT0` to ignore the error object, but I would handle it where possible.
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
var atProtocolBuilder = new ATProtocolBuilder()
    .EnableAutoRenewSession(true)
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
| [com.atproto.label.queryLabels](https://atproto.com/lexicons/com-atproto-label#comatprotolabelquerylabels)  | ❌  |
| [com.atproto.label.subscribeLabels](https://atproto.com/lexicons/com-atproto-label#comatprotolabelsubscribelabels)  | ❌  |

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

## Why "FishyFlip?"

"FishyFlip" is a reference to the [Your Kickstarter Sucks](https://open.spotify.com/episode/5upEtr0tHBf6SoXjJwG5UJ) episode of the same name.

![Discord Image](https://user-images.githubusercontent.com/898335/253739935-c6127d97-bd4a-44a6-9c95-5e0db5ce9e23.png)