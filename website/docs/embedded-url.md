# Creating an embedded URL

There are two ways to create an "embedded url" (`EmbedExternal`) inside a post.

- Manually creating an `EmbedExternal` and attaching it to a post
- Using `ATProtocol.OpenGraphParser` to generate an `EmbedExternal` from a URL

## EmbedExternal

An `EmbedExternal` consists of a

- Title
- Description (Optional)
- URL
- Image `Blob` (Optional)

These are not automatically generated when creating a URL with a post at the lexicon level, even if you include facets to make it a clickable URL. You need to add the embed to the post when creating it. The URL itself does _NOT_ need to be in the post.

## `ATProtocol.OpenGraphParser`

`OpenGraphParser` is a helper class for generating `EmbedExternal` objects. It will automatically call the given website, scrape its `Head` for OpenGraph information, and if found will return a complete `EmbedExternal` for you to use with a post.

```csharp
var embed = await atProtocol.OpenGraphParser.GenerateEmbedExternal("https://github.com/drasticactions/fishyflip");

var result = (await atProtocol.Feed.CreatePostAsync(text: "Check out this project!", embed: embed)).HandleResult();

Console.WriteLine("Post Created: " + result!.Uri);
```

## Creating an EmbedExternal object

You can also create the object yourself. Looking at the source for `OpenGraphParser` shows approaches for this:

```csharp
var ogTags = await this.ParseAsync(url);
if (ogTags.Count == 0)
{
    return null;
}

Blob? image = null;
string? title = null;
string? description = null;
string? urlEmbed = null;

if (ogTags.TryGetValue("image", out var imageUrl))
{
    var imageResult = await this.httpClient.GetByteArrayAsync(imageUrl);
    var contentType = this.fileContentTypeDetector.GetContentType(imageResult);
    if (contentType == "unsupported")
    {
        this.logger?.LogWarning($"Unsupported file type for {imageUrl}");
    }
    else
    {
        var content = new StreamContent(new MemoryStream(imageResult));
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        var blobResult = (await this.atProtocol!.Repo.UploadBlobAsync(content)).HandleResult();
        if (blobResult?.Blob == null)
        {
            this.logger?.LogError($"Failed to upload {imageUrl}");
        }
        else
        {
            this.logger?.LogDebug($"Uploaded {imageUrl} to {blobResult.Blob.Ref}");
            image = blobResult.Blob;
        }
    }
}

if (ogTags.TryGetValue("title", out var tag))
{
    title = tag;
}

if (ogTags.TryGetValue("description", out var ogTag))
{
    description = ogTag;
}

if (ogTags.TryGetValue("url", out var tag1))
{
    urlEmbed = tag1;
}

var external = new External(urlEmbed, title, description, image);
return new EmbedExternal(external);
```
