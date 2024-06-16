FishyFlip ChangeLog

## 1.7.11-alpha
- Supports `net8.0`
- Supports NativeAOT. You should be able to use `PublishAOT` and it have it work correctly. There may be limitations with the Firehose or Source Generated JSON bits I have missed.
- More documentation and comments.

## 1.6.16
### What's Changed
* Bump PeterO.Cbor from 4.5.2 to 4.5.3 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/21
* Bump StyleCop.Analyzers from 1.2.0-beta.435 to 1.2.0-beta.556 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/22
* Bump Microsoft.NET.Test.Sdk from 17.6.0 to 17.8.0 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/23
* Bump IpfsShipyard.Ipfs.Core from 0.0.5 to 0.1.0 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/24
* Bump System.IdentityModel.Tokens.Jwt from 7.2.0 to 7.3.1 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/25


**Full Changelog**: https://github.com/drasticactions/FishyFlip/compare/1.5.25...v1.6.16

## 1.5.25
Add New Endpoint support, including
- `GetActorFeeds`
- `GetSuggestedFeeds`
- `ListBlobs`
- `SearchPosts`

### What's Changed
* Bump xunit.runner.visualstudio from 2.4.5 to 2.5.6 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/20
* Bump coverlet.collector from 3.2.0 to 6.0.0 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/19
* Bump Microsoft.Extensions.Logging.Abstractions from 7.0.1 to 8.0.0 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/18
* Bump xunit from 2.4.2 to 2.6.6 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/17
* Bump Microsoft.Extensions.DependencyInjection from 7.0.0 to 8.0.0 by @dependabot in https://github.com/drasticactions/FishyFlip/pull/16

**Full Changelog**: https://github.com/drasticactions/FishyFlip/compare/1.4...1.5.25

## 1.4.16
- Implemented more `Graph` endpoints, including those for handling `list` and `listitem` types. 
- Fixed `DeleteRecord` to delete records. 
 - The Original `repo` `DeleteRecord` method was public, but it didn't work as I didn't pass the object in to be deleted. Since the other methods to delete records were already there and public, and the other base methods were private, I made `DeleteRecord` private to match. This is a breaking API change, but I think everyone will be okay with it since it didn't work. 

## 1.2.x
- First stable-ish Releases
- Introduce `WithServiceEndpointUponLogin` builder option. With this option enabled, whenever you log into ATProtocol, the internal `HttpClient` will switch its base address to match the users Service Endpoint. Defaults to `true`

