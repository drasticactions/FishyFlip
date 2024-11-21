# Upload Video

Uploading videos is similar to uploading images. Videos must be:
- MP4s
- Less than 50 MBs.

And can include captions that are `.vtt`

```csharp
var captionStream = File.OpenRead("path/to/video.vtt");
var captionContentStream = new StreamContent(captionStream);
captionContentStream.Headers.ContentLength = captionStream.Length;
captionContentStream.Headers.ContentType = new MediaTypeHeaderValue("text/vtt");

// HandleResult unwraps the result from the Result<T> type.
// It will throw if ATErrors are present.
var captionBlob = (await atProtocol.Repo.UploadBlobAsync(captionContentStream)).HandleResult();
var caption = new Caption(lang: "en", file: captionBlob!.Blob);

var videoStream = File.OpenRead("path/to/video.mp4");
var videoContentStream = new StreamContent(videoStream);
videoContentStream.Headers.ContentLength = videoStream.Length;
videoContentStream.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");

var videoBlob = (await atProtocol.Repo.UploadBlobAsync(videoContentStream)).HandleResult();

EmbedVideo embedVideo = new EmbedVideo(
 video: videoBlob!.Blob,
 alt: "Optional Alt text",
 aspectRatio: new(width: 1080, height: 1980),
 captions: new() { caption });

var postResult = (await atProtocol.Feed.CreatePostAsync(
 "Uploading a video!",
  embed: embedVideo,
  langs: new() { "en" })).HandleResult();

Console.WriteLine("Post created: " + postResult!.Uri);
```