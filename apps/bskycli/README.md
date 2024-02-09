# bskycli

`bskycli` is a simple command line program for interacting with Bluesky and ATProtocol. Written in .NET with NativeAOT enabled, it's intended to run without needing the .NET Runtime installed and limited dependencies.

## Third-Party Libraries

- [DotMake Command-Line](https://github.com/dotmake-build/command-line)

## How to build

- After installing the .NET 8 SDK, run `dotnet publish bskycli.csproj --configuration Release --output build`
- This will create a native build in the output "`build`" folder. This is built for the specific operating system and architecture you are running on.

## Commands

### `post`

```console
post: Post a message

Usage:
  bskycli post [options]

Options:
  --text <text>                    Text to post [required]
  --link-card-url <link-card-url>  Link Card Url. Embed into the post as external.
  --languages <languages>          Languages used in post. Can use two or four letter ISO (Ex. 'th' 'en-US')
  --identifier <identifier>        Bluesky Identifier [required]
  --password <password>            Bluesky App-Password [required]
  --instance <instance>            Bluesky Instance URL. [default: https://bsky.social]
  -?, -h, --help                   Show help and usage information
```

### `post-message`

```console
image-post: Post a message with images

Usage:
  bskycli image-post [options]

Options:
  --text <text>              Text to post
  --images <images>          Images to post. Max of 4. [required]
  --alt-text <alt-text>      Alt Text for images. Max of 4. Text is mapped to images in order of entry.
  --languages <languages>    Languages used in post. Can use two or four letter ISO (Ex. 'th' 'en-US')
  --identifier <identifier>  Bluesky Identifier [required]
  --password <password>      Bluesky App-Password [required]
  --instance <instance>      Bluesky Instance URL. [default: https://bsky.social]
  -?, -h, --help             Show help and usage information
```

## Todo

- JSON result output
- Handle at-mentions and resolving handles
- Handle hashtags

