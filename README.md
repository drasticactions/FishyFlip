# FishyFlip - a .NET ATProtocol/Bluesky Library

[![NuGet Version](https://img.shields.io/nuget/v/FishyFlip.svg)](https://www.nuget.org/packages/FishyFlip/) ![License](https://img.shields.io/badge/License-MIT-blue.svg)

![FishyFlip Logo](https://user-images.githubusercontent.com/898335/253740405-4b0ae177-cc49-4c26-b6b0-ab8e835a0e62.png)

FishyFlip is an implementation of [ATProtocol](https://atproto.com/) for .NET, forked from [bluesky-net](https://github.com/dariogriffo/bluesky-net).

For documentation, check out https://drasticactions.github.io/FishyFlip

### Code Flow

The code flow for this repo is:

- `develop` for the newest tip
- `main` for the newest "stable" release build
- `release-version` for servicing previous releases, should it be needed

For 99.9% of PRs, you should target `develop`

### Bump the Bindings

If you wish to bump the bindings (or run the source generator), use the `build_bindings.sh` script, which will build FFSourceGen and run it against the listed repos.

### Third-Party Libraries

- [GitVersion](https://github.com/GitTools/GitVersion)

FishyFlip

- Forked from [bluesky-net](https://github.com/dariogriffo/bluesky-net).
- [CBOR](https://github.com/peteroupc/CBOR)
- [net-ipfs-core](https://github.com/ipfs-shipyard/net-ipfs-core)
- [OneOf](https://github.com/mcintyre321/OneOf)
- [Duende.IdentityModel](https://github.com/duendesoftware/foss)
- [ZstdSharp.Port](https://github.com/oleg-st/ZstdSharp)