# Lists and Starter Packs

Lists in Bluesky are used for grouping users and posts. They can be used for curation, reference, or as a block list. 

## Creating a List

```csharp
// Icons are optional
var iconPath = "favicon.png";
var stream = File.OpenRead(iconPath);
var content = new StreamContent(stream);
content.Headers.ContentLength = stream.Length;
content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
var blobResult = (await atProtocol.Repo.UploadBlobAsync(content)).HandleResult();

// MarkdownPost is a helper class to parse markdown text into facets.
// Hashtags are not supported for descriptions.
var descriptionPost = MarkdownPost.Parse("A great list made by [me](did:plc:nrfz3bngz57p7g7yg6pbkyqr)!" +
    " [ATProtocol](https://atproto.com/) is here!");

var label = new SelfLabel("FishyFlip");
var labels = new SelfLabels(new List<SelfLabel> { label });

/// app.bsky.graph.defs#modlist - A list of actors to apply an aggregate moderation action (mute/block) on.
/// app.bsky.graph.defs#curatelist - A list of actors used for curation purposes such as list feeds or interaction gating.
/// app.bsky.graph.defs#referencelist - A list of actors used for only for reference purposes such as within a starter pack.
// These are available as constants in ListPurpose
var list = new FishyFlip.Lexicon.App.Bsky.Graph.List(
    purpose: ListPurpose.Curatelist,
    name: "My Great List",
    description: descriptionPost.Post,
    descriptionFacets: descriptionPost.Facets,
    avatar: blobResult!.Blob,
    labels: labels,
    createdAt: DateTime.UtcNow);

var createList = (await atProtocol.Graph.CreateListAsync(list)).HandleResult();
```

## Add to a List

```csharp
// URI for the created list.
// This is an example.
var listUri = ATUri.Create("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.graph.list/3lblsfp6e7x2f");

// User to add to the list.
var follow1 = ATDid.Create("did:plc:nrfz3bngz57p7g7yg6pbkyqr");
var follow = (await atProtocol.Graph.CreateListitemAsync(follow1, listUri)).HandleResult();

Console.WriteLine($"List Add: {follow?.Cid}, {follow?.Uri}");
```

## Get a List

```csharp
// URI for the created list.
// This is an example.
var listUri = ATUri.Create("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.graph.list/3lblsfp6e7x2f");

// Defaults to 50. A cursor is returned in the response if there are more items.
// You can use the cursor to get the next page of items.
var listOutput = (await atProtocol.Graph.GetListAsync(listUri)).HandleResult();

Console.WriteLine($"List Name: {listOutput?.List?.Name ?? "Something bad happened!"}");

listOutput?.Items?.ForEach(item =>
{
    // Subject is the actor (profile) item.
    // Uri is their ATUri.
    Console.WriteLine($"Item Name: {item.Subject}, {item.Uri}");
});
```

## Remove an item from list

To remove an item from the list, you need the ATDid of the item (Ex. User), and pass in the `Rkey`. This is automatically parsed and available as a helper by the `ATDid` and `ATUri` objects. 

```csharp
var userId = ATDid.Create("did:plc:nrfz3bngz57p7g7yg6pbkyqr");
var result = (await atProtocol.Graph.DeleteListitemAsync(userId.Rkey)).HandleResult();
```


## Remove a list

To remove a list, the list must be empty. Otherwise, an `ATError` will be thrown telling you to remove every item from the list.

```csharp

// The List ATUri
var listUri = ATUri.Create("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.graph.list/3lblsfp6e7x2f");
var removeListItem = (await atProtocol.Graph.DeleteListAsync(listUri.Rkey)).HandleResult();

```

# Create or Delete a Starter Pack

To create a starter pack, you first need to create a Curated List.

```csharp
var listUri = ATUri.Create("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.graph.list/3lblsfp6e7x2f");
var starterPack = new Starterpack(
    name: "A Sample Starterpack",
    list: listUri,
    description: "A sample starterpack for testing purposes.",
    createdAt: DateTime.UtcNow);

var createStarterPack = (await atProtocol.Graph.CreateStarterpackAsync(starterPack)).HandleResult();

Console.WriteLine($"Created Starterpack: https://bsky.app/starter-pack/{createStarterPack!.Uri!.Identity}/{createStarterPack!.Uri!.Rkey}");
```

To delete a starter pack, as with a list, you need to pass in the Lists `Rkey`

```csharp
await atProtocol.Graph.DeleteStarterpackAsync(createStarterPack!.Uri!.Rkey);
```