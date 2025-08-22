// <copyright file="ATProtocolDelegatingHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FishyFlip.Lexicon.Com.Atproto.Server;

namespace FishyFlip.Tools;

/// <summary>
/// ATProtocol HttpClient.
/// </summary>
public class ATProtocolDelegatingHandler
    : DelegatingHandler
{
    private ATProtocol atProtocol;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocolDelegatingHandler"/> class.
    /// </summary>
    /// <param name="atProtocol"><see cref="ATProtocol"/>.</param>
    /// <param name="handler">HttpClientHandler.</param>
    public ATProtocolDelegatingHandler(ATProtocol atProtocol)
    {
        this.atProtocol = atProtocol;
        var handle = new HttpClientHandler { MaxRequestContentBufferSize = int.MaxValue };
        if (handle.SupportsAutomaticDecompression)
        {
            this.atProtocol.Options.Logger?.LogDebug("Enabling GZip and Deflate decompression.");
            handle.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }
        else
        {
            this.atProtocol.Options.Logger?.LogDebug("Automatic decompression is not supported, disabled...");
        }

        this.InnerHandler = handle;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var result = await base.SendAsync(request, cancellationToken);
        if (result.IsSuccessStatusCode)
        {
            return result;
        }

        if (!this.atProtocol.Options.AutoRenewSession)
        {
            return result;
        }

        if (request.RequestUri?.AbsolutePath.Contains(ServerEndpoints.RefreshSession) ?? false)
        {
            return result;
        }

        var atError = await HttpClientExtensions.CreateError(result, this.atProtocol.Options.JsonSerializerOptions, cancellationToken);

        if (atError is not
            { StatusCode: 400, Detail: { Error: Constants.ExpiredToken, Message: Constants.TokenHasExpired } })
        {
            return result;
        }

        var (authResult, error) = await this.atProtocol.Server.RefreshSessionAsync(cancellationToken);
        if (error is not null)
        {
            return result;
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
