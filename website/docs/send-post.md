# Send Post

To send a simple text post, all you need is to call `CreatePostAsync` after logging in.

```csharp
// Creates a text post of "Hello, World!" to the signed in users account.
var postResult = await atProtocol.Feed.CreatePostAsync("Hello, World!");
postResult.Switch(
    success =>
    {
        // Contains the ATUri and CID.
        // This links back to the post that was created.
        Console.WriteLine($"Post: {success.Uri} {success.Cid}");
    },
    error =>
    {
        Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
    }
);
```