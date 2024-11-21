# Send Post

```csharp
// Creates a text post of "Hello, World!" to the signed in users account.
var postResult = await atProtocol.Repo.CreatePostAsync("Hello, World!", DateTime.UtcNow);
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