# Upload Image

- To upload an image, you need to first upload it as a blob, and then attach it to a post. You can also embed links in text by setting a "Link" Facet.

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
           Image? image = success.Blob.ToImage();

           var prompt = "Hello, Image! Link Goes Here!";

           // To insert a link, we need to find the start and end of the link text.
           // This is done as a "ByteSlice."
           int promptStart = prompt.IndexOf("Link Goes Here!", StringComparison.InvariantCulture);
           int promptEnd = promptStart + Encoding.Default.GetBytes("Link Goes Here!").Length;
           var index = new FacetIndex(promptStart, promptEnd);
           var link = FacetFeature.CreateLink("https://drasticactions.dev");
           var facet = new Facet(index, link);

           // Create a post with the image and the link.
           var postResult = await atProtocol.Repo.CreatePostAsync(prompt, new[] { facet }, new ImagesEmbed(image, "Optional Alt Text, you should have your users set this when possible"));
       },
       async error =>
       {
            Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
       }
);
```

You should then see your image and link.

![Post Sample](https://user-images.githubusercontent.com/898335/253740484-57addcb6-523c-4b65-914d-495ddf8e1474.png)