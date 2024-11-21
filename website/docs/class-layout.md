# Class Layout

There are two ways to access the API endpoints: Extension methods and classes within `ATProtocol`.
The class methods call into the extension methods, so either path should call into the same code.

## Extension Methods

Every ATProtocol Endpoint that's generated is an extension method for `ATProtocol`, located within the namespace of where it's localed in the Lexicon. For example:

```csharp
// Exposes the Actor Lexicon entries to ATProtocol.
using FishyFlip.Lexicon.App.Bsky.Actor;

// peepthisbot.bsky.social DID: did:plc:nrfz3bngz57p7g7yg6pbkyqr
var did = ATDid.Create("did:plc:nrfz3bngz57p7g7yg6pbkyqr");
var result = await atProtocol.GetProfileAsync(did!);
result.Switch(
 success =>
 {
    // Peep This Bot
    Console.WriteLine($"Profile: {success!.DisplayName}");
 },
 error =>
 {
    Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
 }
);
```

## Class Methods

Each endpoint is also available as classes within the `ATProtocol` class. This doesn't require bringing in using statements, but since the names can overlap, they have been crafted to better fit the schema.

- `com.atprotocol` and `app.bsky` methods are rooted
- The rest contain their full names

For example:

```csharp
// peepthisbot.bsky.social DID: did:plc:nrfz3bngz57p7g7yg6pbkyqr
var did = ATDid.Create("did:plc:nrfz3bngz57p7g7yg6pbkyqr");
// Actor goes to app.bsky.actor
var result = await atProtocol.Actor.GetProfileAsync(did!);
result.Switch(
 success =>
 {
    // Peep This Bot
    Console.WriteLine($"Profile: {success!.DisplayName}");
 },
 error =>
 {
    Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
 }
);
```