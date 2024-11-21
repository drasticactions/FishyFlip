# Upload Image

- To upload an image, you need to first upload it as a blob, and then attach it to a post via an `EmbedImages`.

```csharp
var stream = File.OpenRead("path/to/image.png");
var content = new StreamContent(stream);
content.Headers.ContentLength = stream.Length;

// Bluesky uses the content type header for setting the blob type.
// As of this writing, it does not verify what kind of blob gets uploaded.
// But you should be careful about setting generic types or using the wrong one.
// If you do not set a type, it will return an error.
content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
var blobResult = await atProtocol.Repo.UploadBlobAsync(content);
await blobResult.SwitchAsync(
       async success =>
       {
           // Blob is uploaded.
           Console.WriteLine($"Blob: {success.Blob.Type}");

           // Converts the blob to an image.
           Image image = new Image(
           image: success.Blob, 
           alt: "Optional Alt Text, you should have your users set this when possible",
           aspectRatio: new AspectRatio(width: 700, height: 584));

           var prompt = "Hello, Image!";

           // Create a post with the image.
           var postResult = await atProtocol.Feed.CreatePostAsync(
            prompt,
            embed: new EmbedImages(images: new() { image }));
       },
       async error =>
       {
            Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
       }
);
```

You should then see your image.

![Post Sample](https://user-images.githubusercontent.com/898335/253740484-57addcb6-523c-4b65-914d-495ddf8e1474.png)