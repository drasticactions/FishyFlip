# FishyFlip.Xrpc - a .NET ATProtocol/Bluesky Library for XRPC endpoints

[![NuGet Version](https://img.shields.io/nuget/v/FishyFlip.Xrpc.svg)](https://www.nuget.org/packages/FishyFlip.Xrpc/) ![License](https://img.shields.io/badge/License-MIT-blue.svg)

![FishyFlip.Xrpc Logo](https://user-images.githubusercontent.com/898335/253740405-4b0ae177-cc49-4c26-b6b0-ab8e835a0e62.png)

FishyFlip.Xrpc is an experimental implmentation of the [ATProtocol XRPC specification](https://atproto.com/specs/xrpc) through source generation. It maps the XRPC entries listed in Bluesky and other ATProtocol lexicons to ASP.NET MVC controllers. As it binds to FishyFlip's existing classes, it can handle model and parameter validation, and being source generated along with FishyFlip can map to the latest changes in the lexicons whenever a value changes. This should make it easier to keep up with the upstream sources.  

## ATErrorResult

ATErrorResult is an `IResult` that maps to the listed documented expected error results, with a JSON object in a format that's expected. There is no direct list of expected response Error messages and detail messages, but the format should at least map to what Bluesky itself uses.