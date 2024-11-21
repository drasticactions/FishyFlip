# Facets

- Facets are metadata added to a post.

## Link Facet

Link Facets are how you create clickable links. Adding a URL to a post text isn't enough for it to be clickable, you need to add a facet to it for the length of the URL. This can also be used to add links to general text.

```csharp
var prompt = "Hello, Link Goes Here!";

// To insert a link, we need to find the start and end of the link text.
// This is done as a "ByteSlice."
int promptStart = prompt.IndexOf("Link Goes Here!", StringComparison.InvariantCulture);
int promptEnd = promptStart + Encoding.Default.GetBytes("Link Goes Here!").Length;
var facet = Facet.CreateFacetLink(promptStart, promptEnd, "https://drasticactions.dev");

// Create a post with a link.
var postResult = await atProtocol.Feed.CreatePostAsync(
prompt,
new() { facet });
```

## Mention Facet

Mention facets are for enabling At-reply mentions inside of posts. For these, you need the users DID identifier. It's expected that these are added to the users display name, but could be added to any text. 

```csharp
var prompt = "Hello, @peepthisbot.bsky.social!";

// To insert a mention, we need to find the start and end of the mention text.
// This is done as a "ByteSlice."
// We also need to have the users DID.
// peepthisbot.bsky.social DID: did:plc:nrfz3bngz57p7g7yg6pbkyqr
var did = ATDid.Create("did:plc:nrfz3bngz57p7g7yg6pbkyqr");
int promptStart = prompt.IndexOf("@peepthisbot.bsky.social", StringComparison.InvariantCulture);
int promptEnd = promptStart + Encoding.Default.GetBytes("@peepthisbot.bsky.social").Length;
var facet = Facet.CreateFacetMention(promptStart, promptEnd, did!);

// Create a post with a mention.
var postResult = await atProtocol.Feed.CreatePostAsync(
 prompt,
 new() { facet });
```

## Hashtag Facet

Hashtag facets are for enabling... hashtags. Similar to the facets above, except when adding the tag via `CreateFacetHashtag`, do not add a hashtag.

```csharp
var prompt = "Hello, World! This is #FishyFlip!";

int promptStart = prompt.IndexOf("#FishyFlip", StringComparison.InvariantCulture);
int promptEnd = promptStart + Encoding.Default.GetBytes("#FishyFlip").Length;

// Do not include the hashtag symbol in the tag below.
// If you do, it'll be doubled.
var facet = Facet.CreateFacetHashtag(promptStart, promptEnd, hashtag: "FishyFlip");

// Create a post with a hashtag.
var postResult = await atProtocol.Feed.CreatePostAsync(
 prompt,
 new() { facet });
```