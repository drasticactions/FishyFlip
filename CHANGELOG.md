FishyFlip ChangeLog

## 1.2.x
- First stable-ish Releases
- Introduce `WithServiceEndpointUponLogin` builder option. With this option enabled, whenever you log into ATProtocol, the internal `HttpClient` will switch its base address to match the users Service Endpoint. Defaults to `true`

