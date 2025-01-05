// <copyright file="ATProtocolHttpClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using FishyFlip.Lexicon.Com.Atproto.Server;

namespace FishyFlip.Tools;

/// <summary>
/// ATProtocol HttpClient.
/// </summary>
public class ATProtocolHttpClient : HttpClient
{
    private ATProtocol atProtocol;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocolHttpClient"/> class.
    /// </summary>
    /// <param name="atProtocol"><see cref="ATProtocol"/>.</param>
    /// <param name="handler">HttpClientHandler.</param>
    public ATProtocolHttpClient(ATProtocol atProtocol, HttpMessageHandler handler)
        : base(handler)
    {
        this.atProtocol = atProtocol;
    }

    /// <inheritdoc/>
    public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var result = await base.SendAsync(request, cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            if (request.RequestUri?.AbsolutePath.Contains(ServerEndpoints.RefreshSession) ?? false)
            {
                return result;
            }

#if NETSTANDARD
            string response = await result.Content.ReadAsStringAsync();
#else
            string response = await result.Content.ReadAsStringAsync(cancellationToken);
#endif

            var atError = JsonSerializer.Deserialize<ATError>(response, SourceGenerationContext.Default.ATError);
            if (atError is not null)
            {
                if (atError is { StatusCode: 400, Detail: { Error: Constants.ExpiredToken, Message: Constants.TokenHasExpired } })
                {
                    await this.atProtocol.SessionManager.RefreshSessionAsync(cancellationToken);
                    return await base.SendAsync(request, cancellationToken);
                }
            }
        }

        return result;
    }
}
