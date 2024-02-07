# Creating a Session

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

- Once created, you can now access unauthenticated APIs. 