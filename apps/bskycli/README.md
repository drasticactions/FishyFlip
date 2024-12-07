# bskycli

bskycli is a .NET Console application for accessing Bluesky/ATProtocol, designed as an easy way to automate posting for bots. Written with NativeAOT in mind, it can be built and run independent of the .NET Runtime.

## ffmpeg

Updating images and video require `ffmpeg` and `ffprobe` to be installed. `bskycli` will automatically handle setting the correct aspect ratio for the video and image upload by leveraging `ffprobe`

## Markdown Post

Post text can be written using a subset of Markdown, allowing for easy embedding of text with Hashtags, URLs, and Mentions. For example:

```
Markdown Test: [FishyFlip](https://drasticactions.github.io/FishyFlip), #FishyFlip, [@drasticactions.dev](did:plc:yhgc5rlqhoezrx6fbawajxlh)
```

will become

"Markdown Test: [FishyFlip](https://drasticactions.github.io/FishyFlip), [#FishyFlip](FishyFlip), [@drasticactions.dev](did:plc:yhgc5rlqhoezrx6fbawajxlh)"

## CI Artifacts

The CI artifacts are currently not signed. You may see alerts when running this on Windows and macOS as a result.

To run the CI artifacts on macOS, you can remove the quarantine on it with the following terminal command:

`xattr -rd com.apple.quarantine path/to/bskycli`

## Third Party Libraries

- ConsoleAppFramework