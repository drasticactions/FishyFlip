// <copyright file="ATProtoAdmin.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoAdmin
{
    private ATProtocol proto;
    private HttpClient adminClient;
    private bool disposedValue;

    internal ATProtoAdmin(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;
}
