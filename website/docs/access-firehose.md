# The ATProtocol Firehose

There are two ways to access the Firehose: Directly, or through [Jetstream](https://github.com/bluesky-social/jetstream). 

## Direct Access

- This method uses websockets to connect to ATProtocol. It returns CBOR objects that can be parsed into C# objects through helper methods.
- You can access it by creating a `ATWebSocketProtocol` and using `SubscribeRepos`. This can be seen in the `Firehose` sample. SubscribeRepos uses Websockets to connect to a given instead and get messages whenever a new one is posted. Messages need to be handled outside of the general WebSocket stream; if anything blocks the stream from returning messages, you may see errors from the protocol saying your connection is too slow.

```csharp
var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATWebSocketProtocolBuilder()
// Defaults to bsky.network.
    .WithInstanceUrl(new Uri("https://bsky.network"))
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
        Console.WriteLine($"Record: {message.Record.ToJson()}");
    }
}
```

- `Sync` endpoints generally encode their output as [IPFS Car](https://car.ipfs.io/) files. Here, we can process them as they are streaming so instead of needing to download a whole file to process it, we can do it as it is downloading. This is done by using the `OnCarDecoded` delegate.

```csharp
// Downloading Repo files requires the PDS of the given user.
// You can get that from https://plc.directory
// Or it is automatically set if downloading your own Repo.
var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATProtocolBuilder()
 .WithInstanceUrl(new Uri("https://puffball.us-east.host.bsky.network"))
 .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

// drasticactions.jp
var did = ATDid.Create("did:plc:6d3q55s45v6o57gwrxnwzhlz");
var checkoutResult = await atProtocol.Sync.GetRepoAsync(did!, HandleProgressStatus);

async void HandleProgressStatus(CarProgressStatusEvent e)
{
 var cid = e.Cid;
 var bytes = e.Bytes;
 var cborObject = CBORObject.DecodeFromBytes(bytes);
 // These objects can be Frames.
 // We can check in advance if this is an ATObject.
 if (cborObject.IsATObject())
 {
  var record = cborObject.ToATObject();

  // Print the record as JSON.
  Console.WriteLine($"Record: {record.ToJson()}");
 }
}
```

## Jetstream

- Jetstream is a new method for accessing the Firehose. It still uses Websockets, but returns JSON objects.
- You can access it by creating a `ATJetStreamBuilder` and using `ConnectAsync`. This can be seen in the `Jetstream` sample. 

```csharp
var debugLog = new DebugLoggerProvider();

// You can set a custom url with WithInstanceUrl
var jetstreamBuilder = new ATJetStreamBuilder()
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = jetstreamBuilder.Build();

atWebProtocol.OnConnectionUpdated += (sender, args) =>
{
    Console.WriteLine($"Connection Updated: {args.State}");
};

// OnRecordReceived returns ATObjectWebSocket records,
// Which contain ATObject records.
// If you wish to receive all records being returned,
// subscribe to OnRawMessageReceived.
atWebProtocol.OnRecordReceived += (sender, args) =>
{
    Console.WriteLine($"Record Received: {args.Record.Kind}");
    switch (args.Record.Kind)
    {
        case ATWebSocketEvent.Commit:
            Console.WriteLine($"Commit: {args.Record.Commit?.Operation}");
            switch (args.Record.Commit?.Operation)
            {
                // Create is when a new record is created.
                case ATWebSocketCommitType.Create:
                    Console.WriteLine($"Create: {args.Record.Commit?.Collection}");

                    // Record is an ATWebSocketRecord, which contains the actual record inside Commit.
                    switch (args.Record.Commit?.Record)
                    {
                        case FishyFlip.Lexicon.App.Bsky.Feed.Post post:
                            Console.WriteLine($"Post: {post.ToJson()}");
                            break;
                        case FishyFlip.Lexicon.App.Bsky.Feed.Threadgate threadgate:
                            Console.WriteLine($"ThreadGate: {threadgate.ToJson()}");
                            break;
                        default:
                            if (args.Record.Commit?.Record is { } obj)
                            {
                                Console.WriteLine($"ATObject: {obj.ToJson()}");
                            }

                            break;
                    }
                    break;
            }

            break;
        case ATWebSocketEvent.Identity:
            Console.WriteLine($"Identity: {args.Record.Identity?.Did}");
            break;
        case ATWebSocketEvent.Account:
            Console.WriteLine($"Account: {args.Record.Account?.Did}");
            break;
        case ATWebSocketEvent.Unknown:
        default:
            Console.WriteLine($"{args.Record.Kind}");
            break;
    }
}
```