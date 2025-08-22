# BSkyOAuth.ClientMetadata

A simple ASP.NET Core Application for generating custom ATProtocol [`client-metadata.json`](https://atproto.com/specs/oauth). It can be split between hosts so multiple domains can point to a single server and generate custom `client-metadata.json` depending on the context.

This is designed for "public" OAuth instances, and does not handle the extra keys required for the "secure" solution. This is intended for native applications where the bulk of the requests are happening on the client directly to a users PDS, rather than through the server itself.