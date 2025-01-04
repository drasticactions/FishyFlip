// <copyright file="Constants.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

#pragma warning disable SA1600 // Elements should be documented
public static class Constants
{
    internal const string DidJson = ".well-known/did.json";
    internal const string AtprotoPersonalDataServer = "AtprotoPersonalDataServer";
    internal const string AtprotoPersonalDataServerId = "#atproto_pds";
    internal const string BlueskyApiClient = "FishyFlip";
    internal const string ContentMediaType = "application/json";
    internal const string AcceptedMediaType = "application/json";
    internal const string AtProtoAcceptLabelers = "atproto-accept-labelers";
    internal const string AtProtoContentLabelers = "atproto-content-labelers";
    internal const string AtProtoProxy = "atproto-proxy";

    internal const string AtLabeler = "#atproto_labeler";
    internal const string BlueskyChatProxy = "did:web:api.bsky.chat#bsky_chat";
    internal const string BlueskyModerationServiceDid = "did:plc:ar7c4by46qjdydhdevvrndac";

    internal const string RedactParameter = "redact";

    public static class Urls
    {
        public static class ATProtoServer
        {
            public const string PublicApi = "https://public.api.bsky.app";
            public const string NetworkApi = "https://bsky.network";
            public const string SocialApi = "https://bsky.social";
        }
    }

    internal class HeaderNames
    {
        public const string UserAgent = "user-agent";
    }
}
#pragma warning restore SA1600 // Elements should be documented