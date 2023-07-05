// <copyright file="ATProtocolBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools;
using Microsoft.Extensions.Logging;

namespace FishyFlip;

public class ATProtocolBuilder
{
    private readonly ATProtocolOptions atProtocolOptions;
    private bool setHttpClientDefaults = true;

    public ATProtocolBuilder()
    {
        this.atProtocolOptions = new ATProtocolOptions();
    }

    /// <summary>
    /// Set a custom HttpClient.
    /// </summary>
    /// <param name="client">HttpClient.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithHttpClient(HttpClient client, bool setDefaults = true)
    {
        this.atProtocolOptions.HttpClient = client;
        this.setHttpClientDefaults = setDefaults;
        return this;
    }

    public ATProtocolBuilder WithUrl(string url)
    {
        this.atProtocolOptions.Url = url;
        return this;
    }

    public ATProtocolBuilder WithUserAgent(string userAgent)
    {
        this.atProtocolOptions.UserAgent = userAgent;
        return this;
    }

    public ATProtocolBuilder EnableAutoRenewSession(bool autoRenewSession)
    {
        this.atProtocolOptions.AutoRenewSession = autoRenewSession;
        return this;
    }

    public ATProtocolBuilder WithLogger(ILogger logger)
    {
        this.atProtocolOptions.Logger = logger;
        return this;
    }

    public ATProtocolBuilder WithSessionRefreshInterval(TimeSpan interval)
    {
        this.atProtocolOptions.SessionRefreshInterval = interval;
        return this;
    }

    /// <summary>
    /// Builds the Protocol.
    /// </summary>
    /// <returns>The <seealso cref="ATProtocol"/> build with these configs.</returns>
    public ATProtocol Build()
    {
        if (this.setHttpClientDefaults)
        {
            this.atProtocolOptions.HttpClient.DefaultRequestHeaders.Add(Constants.HeaderNames.UserAgent, this.atProtocolOptions.UserAgent);
            this.atProtocolOptions.HttpClient.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
            this.atProtocolOptions.HttpClient.BaseAddress = new Uri(this.atProtocolOptions.Url.TrimEnd('/'));
        }

        return new ATProtocol(this.atProtocolOptions);
    }
}