# FishyFlipDX - The Experimenal .NET ATProtocol/Bluesky Library

[![NuGet Version](https://img.shields.io/nuget/v/FishyFlipDX.svg)](https://www.nuget.org/packages/FishyFlipDX/) ![License](https://img.shields.io/badge/License-MIT-blue.svg)

![FishyFlip Logo](https://user-images.githubusercontent.com/898335/253740405-4b0ae177-cc49-4c26-b6b0-ab8e835a0e62.png)

FishyFlipDX is an alpha version of FishyFlip, an [ATProtocol](https://atproto.com/) library for .NET. The biggest differences between it and the original version is the Lexicon. The ATProtocol Lexicon is being source generated, making it _much_ easier to upgrade as changes and improvements are made to it, and also helps make it consistant for comparing implementation details between their API documentation and this.

However, this may require breaking changes around namespaces and object names to make it work without more hackery. This library exists to work through those issues in the open.

Currently implmented is the `ATWebSocketProtocol` and `ATJetStream` for accessing the Firehose, with Source Generated Endpoints and `ATProtocol` itself to come.


### Third-Party Libraries

- [GitVersion](https://github.com/GitTools/GitVersion)

FishyFlip

- Forked from [bluesky-net](https://github.com/dariogriffo/bluesky-net).
- [CBOR](https://github.com/peteroupc/CBOR)
- [net-ipfs-core](https://github.com/ipfs-shipyard/net-ipfs-core)
- [OneOf](https://github.com/mcintyre321/OneOf)