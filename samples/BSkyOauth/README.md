# Bluesky / ATProto OAuth

These are example native applications for logging into the Bluesky AppView through OAuth, .NET, and [FishyFlip](https://github.com/drasticactions/fishyflip). The core concepts can apply to other libraries and implementations.

## TL;DR

- Create a `client-metadata.json` and serve it on the public web. These examples use `https://drasticactions.vip/client-metadata.json`. Do _NOT_ use this for public applications. It returns one scope (`atproto`) and one redirect URI. It works for demos, but not for a full application. However, hosting your own JSON is straightforward and you can look at `BSkyOAuth.ClientMetadata` for an automated way to do it.
- Register the callbacks for your application based on the redirect URIs in the `client-metadata.json`
- Create an Authorization URI based on the ATProtocol documentation linked above. This can be generated for you in FishyFlip by calling `atProtocol.GenerateOAuth2AuthenticationUrlResultAsync`. Direct the user to this URL, either through their browser or platform specific code.
- Upon callback, get the code or error from the redirect URI and handle it. With FishyFlip, you can call `atProtocol.AuthenticateWithOAuth2CallbackResultAsync`. Once you have a session, you can call the OAuth refresh tokens to get new tokens.