// <copyright file="ATProtocolHttpClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

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
            // If token has expired, refresh it and return the request.
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await this.atProtocol.SessionManager.RefreshSessionAsync(cancellationToken);
                return await base.SendAsync(request, cancellationToken);
            }
        }

        return result;
    }
}
