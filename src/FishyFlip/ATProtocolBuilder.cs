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
    private bool setHttpClientDefaults = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocolBuilder"/> class.
    /// </summary>
    /// <param name="customEmbedConverters">Customer JSON Converters for Embed.</param>
    /// <param name="customAtRecordConverters">Customer JSON Converters for ATRecord.</param>
    public ATProtocolBuilder(IReadOnlyList<ICustomEmbedConverter>? customEmbedConverters = default, IReadOnlyList<ICustomATRecordConverter>? customAtRecordConverters = default)
    {
        this.atProtocolOptions = new ATProtocolOptions(customEmbedConverters, customAtRecordConverters);
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
    /// Set a custom HttpClient.
    /// </summary>
    /// <param name="client">HttpClient.</param>
    /// <param name="setDefaults">Enables the default values to be set.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithHttpClient(HttpClient client, bool setDefaults = true)
    {
        this.atProtocolOptions.HttpClient = client;
        this.setHttpClientDefaults = setDefaults;
        return this;
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
    /// Sets an initial session to use.
    /// </summary>
    /// <param name="session"><see cref="Session"/>.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithInitialSession(Session session)
    {
        this.atProtocolOptions.Session = session;
        return this;
    }

    /// <summary>
    /// Set the ATProtocol to use admin credentials.
    /// </summary>
    /// <param name="password">Admin password.</param>
    /// <param name="username">Admin username. Defaults to "admin".</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder AsAdmin(string password, string username = "admin")
    {
        string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        this.atProtocolOptions.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
        return this;
    }

    /// <summary>
    /// Returns the ATProtocolOptions.
    /// </summary>
    /// <returns>ATProtocolOptions.</returns>
    public ATProtocolOptions BuildOptions()
    {
        if (this.setHttpClientDefaults)
        {
            this.atProtocolOptions.UpdateHttpClient(this.atProtocolOptions.Url);
        }

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