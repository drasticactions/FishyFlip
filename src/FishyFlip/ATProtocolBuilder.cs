// <copyright file="ATProtocolBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Security.Cryptography.X509Certificates;

namespace FishyFlip;

/// <summary>
/// AT Protocol Builder.
/// </summary>
public class ATProtocolBuilder
{
    private readonly ATProtocolOptions atProtocolOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocolBuilder"/> class.
    /// </summary>
    public ATProtocolBuilder()
    {
        this.atProtocolOptions = new ATProtocolOptions();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocolBuilder"/> class.
    /// </summary>
    /// <param name="options">ATProtocolOptions.</param>
    public ATProtocolBuilder(ATProtocolOptions options)
    {
        this.atProtocolOptions = options;
    }

    /// <summary>
    /// Set the instance url to connect to.
    /// </summary>
    /// <param name="url">Instance Url.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithInstanceUrl(Uri url)
    {
        this.atProtocolOptions.Url = url;
        return this;
    }

    /// <summary>
    /// Sets the user agent.
    /// </summary>
    /// <param name="userAgent">User Agent.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithUserAgent(string userAgent)
    {
        this.atProtocolOptions.UserAgent = userAgent;
        return this;
    }

    /// <summary>
    /// Sets UseServiceEndpointUponLogin.
    /// </summary>
    /// <param name="serviceEndpointUponLogin">Value for UseServiceEndpointUponLogin.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.s.</returns>
    public ATProtocolBuilder WithServiceEndpointUponLogin(bool serviceEndpointUponLogin)
    {
        this.atProtocolOptions.UseServiceEndpointUponLogin = serviceEndpointUponLogin;
        return this;
    }

    /// <summary>
    /// Enable auto renewing sessions.
    /// </summary>
    /// <param name="autoRenewSession">Auto Renew Session.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder EnableAutoRenewSession(bool autoRenewSession)
    {
        this.atProtocolOptions.AutoRenewSession = autoRenewSession;
        return this;
    }

    /// <summary>
    /// Adds a logger.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithLogger(ILogger? logger)
    {
        this.atProtocolOptions.Logger = logger;
        return this;
    }

    /// <summary>
    /// Adds a cache set of ATDid values with their respective service endpoint URIs.
    /// Use this to cache the service endpoints for the ATDid values to avoid having to look them up.
    /// </summary>
    /// <param name="didCache">Cache values.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithATDidCache(Dictionary<ATDid, Uri> didCache)
    {
        foreach (var item in didCache)
        {
            this.atProtocolOptions.DidCache[item.Key.ToString()] = item.Value.ToString();
        }

        return this;
    }

    /// <summary>
    /// Adds a cache set of ATHandle values with their respective service endpoint URIs.
    /// Use this to cache the service endpoints for the ATHandle values to avoid having to look them up.
    /// </summary>
    /// <param name="didCache">Cache values.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithATHandleCache(Dictionary<ATHandle, Uri> didCache)
    {
        foreach (var item in didCache)
        {
            this.atProtocolOptions.DidCache[item.Key.ToString()] = item.Value.ToString();
        }

        return this;
    }

    /// <summary>
    /// Sets the session refresh interval.
    /// </summary>
    /// <param name="interval">Interval to refresh at.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithSessionRefreshInterval(TimeSpan interval)
    {
        if (interval.TotalMinutes < 1)
        {
            throw new ArgumentException("Session refresh interval must be at least 1 minute.");
        }

        this.atProtocolOptions.SessionRefreshInterval = interval;
        return this;
    }

    /// <summary>
    /// Returns the ATProtocolOptions.
    /// </summary>
    /// <returns>ATProtocolOptions.</returns>
    public ATProtocolOptions BuildOptions()
    {
        return this.atProtocolOptions;
    }

    /// <summary>
    /// Builds the Protocol.
    /// </summary>
    /// <returns>The <seealso cref="ATProtocol"/> build with these configs.</returns>
    public ATProtocol Build()
    {
        var options = this.BuildOptions();

        return new ATProtocol(options);
    }
}