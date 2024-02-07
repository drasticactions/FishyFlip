# Access the Firehose

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