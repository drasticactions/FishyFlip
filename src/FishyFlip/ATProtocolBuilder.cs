// <copyright file="ATProtocolBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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

    /// <summary>
    /// Set the instance url to connect to.
    /// </summary>
    /// <param name="url">Instance Url.</param>
    /// <returns><see cref="ATProtocolBuilder"/></returns>
    public ATProtocolBuilder WithInstanceUrl(Uri url)
    {
        this.atProtocolOptions.Url = url;
        return this;
    }

    /// <summary>
    /// Sets the user agent.
    /// </summary>
    /// <param name="userAgent"></param>
    /// <returns><see cref="ATProtocolBuilder"/></returns>
    public ATProtocolBuilder WithUserAgent(string userAgent)
    {
        this.atProtocolOptions.UserAgent = userAgent;
        return this;
    }

    /// <summary>
    /// Enable auto renewing sessions.
    /// </summary>
    /// <param name="autoRenewSession"></param>
    /// <returns><see cref="ATProtocolBuilder"/></returns>
    public ATProtocolBuilder EnableAutoRenewSession(bool autoRenewSession)
    {
        this.atProtocolOptions.AutoRenewSession = autoRenewSession;
        return this;
    }

    /// <summary>
    /// Adds a logger.
    /// </summary>
    /// <param name="logger"></param>
    /// <returns><see cref="ATProtocolBuilder"/></returns>
    public ATProtocolBuilder WithLogger(ILogger? logger)
    {
        this.atProtocolOptions.Logger = logger;
        return this;
    }

    /// <summary>
    /// Sets the session refresh interval.
    /// </summary>
    /// <param name="interval"></param>
    /// <returns><see cref="ATProtocolBuilder"/></returns>
    public ATProtocolBuilder WithSessionRefreshInterval(TimeSpan interval)
    {
        this.atProtocolOptions.SessionRefreshInterval = interval;
        return this;
    }

    /// <summary>
    /// Set the ATProtocol to use admin credentials.
    /// </summary>
    /// <param name="password">Admin password.</param>
    /// <param name="username">Admin username. Defaults to "admin".</param>
    /// <returns><see cref="ATProtocolBuilder"/></returns>
    public ATProtocolBuilder AsAdmin(string password, string username = "admin")
    {
        string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        this.atProtocolOptions.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
        return this;
    }

    public ATProtocolOptions BuildOptions()
    {
        if (this.setHttpClientDefaults)
        {
            this.atProtocolOptions.HttpClient.DefaultRequestHeaders.Add(Constants.HeaderNames.UserAgent, this.atProtocolOptions.UserAgent);
            this.atProtocolOptions.HttpClient.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
            this.atProtocolOptions.HttpClient.BaseAddress = this.atProtocolOptions.Url;
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