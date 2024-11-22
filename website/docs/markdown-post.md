# Markdown Post

For CLI applications, manually setting metadata values like facets can be a pain. For this, FishyFlip has a helper class for making it easier to deal with, `MarkdownPost`

`MarkdownPost` supports a subset of markdown for handling URLs, At-Mentions, and Hashtags. 

```csharp
var markdownPostText = "Hello Markdown! [ATProtocol](https://atproto.com/), " +
                       "[@peepthisbot.bsky.social](did:plc:nrfz3bngz57p7g7yg6pbkyqr)," +
                       "[#FishyFlip](FishyFlip)";

var post = MarkdownPost.Parse(markdownPostText);
var result = (await atProtocol.Feed.CreatePostAsync(text: post.Post, facets: post.Facets)).HandleResult();

// Should return the ATUri for the post.
Console.WriteLine($"Result: {result!.Cid}, {result.Uri}");
```

This will result in a post with a URL to https://atproto.com/, an At-mention to `peepthisbot.bsky.social`, and a hashtag of `#FishyFlip`

**NOTE**: While this could be used for GUI based applications, you should limit this for CLI applications where you as developer have control over the input.
