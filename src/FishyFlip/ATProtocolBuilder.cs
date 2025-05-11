// <copyright file="ATProtocolBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Security.Cryptography.X509Certificates;
using FishyFlip.Tools.Json;

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
    /// <param name="customAtObjectConverters">Customer JSON Converters for ATObject.</param>
    public ATProtocolBuilder(IReadOnlyList<ICustomATObjectJsonConverter>? customAtObjectConverters = null)
    {
        this.atProtocolOptions = new ATProtocolOptions(customAtObjectConverters);
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
    /// Set the instance url to connect to by resolving the ATDid to a <see cref="DidDoc"/>.
    /// </summary>
    /// <param name="atDid"><see cref="ATDid"/>.</param>
    /// <param name="serviceType">Service Type.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithInstanceUrl(ATDid atDid, string serviceType = Constants.AtprotoPersonalDataServerId, CancellationToken? cancellationToken = null)
        => this.WithInstanceUrlAsync(atDid, serviceType).GetAwaiter().GetResult();

    /// <summary>
    /// Set the instance url to connect to by resolving the ATDid to a <see cref="DidDoc"/>.
    /// </summary>
    /// <param name="atDid"><see cref="ATDid"/>.</param>
    /// <param name="serviceType">Service Type, defaults to ATProtocol PDS.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public async Task<ATProtocolBuilder> WithInstanceUrlAsync(ATDid atDid, string serviceType = Constants.AtprotoPersonalDataServerId, CancellationToken? cancellationToken = null)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add(Constants.HeaderNames.UserAgent, this.atProtocolOptions.UserAgent);
        var (didDoc, error) = await httpClient.GetDidDocAsync(atDid, cancellationToken, this.atProtocolOptions.Logger);
        if (error is not null)
        {
            throw new Exception($"Failed to resolve ATDid {atDid} to a DidDoc. {error}");
        }

        var url = didDoc!.GetServiceEndpointUrl(serviceType, this.atProtocolOptions.Logger);
        if (url is null)
        {
            throw new Exception($"Failed to resolve ATDid {atDid} to a DidDoc. DidDoc does not contain a service endpoint for {serviceType}");
        }

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
    /// Enable auto-renewing sessions.
    /// </summary>
    /// <param name="autoRenewSession">Auto Renew Session.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder EnableAutoRenewSession(bool autoRenewSession)
    {
        this.atProtocolOptions.AutoRenewSession = autoRenewSession;
        return this;
    }

    /// <summary>
    /// Include the Bluesky Moderation service label by default.
    /// </summary>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder EnableBlueskyModerationService()
    {
        this.atProtocolOptions.LabelParameters.Add(LabelParameter.BlueskyModeration);
        return this;
    }

    /// <summary>
    /// Sets the ATProtocol Ozone Proxy header.
    /// </summary>
    /// <param name="did">The ATDid of the labeler service. This DID should include an #at_labeler that points to the ozone instance to resolve to.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithOzoneProxy(ATDid did)
    {
        this.atProtocolOptions.OzoneProxyHeader = $"{did}{Constants.AtLabeler}";
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
    /// Adds a cache set of ATProxy values with their respective service labels.
    /// The key should resolve to the group namespace of the endpoint, ex. com.whtwnd.blog.
    /// or the subset of it, ex. com.whtwnd.
    /// The value should be the the DID followed by the service endpoint identifier, ex. did:web:whtwnd.com#whitewind_blog.
    /// More information can be found at https://atproto.com/specs/xrpc#service-proxying.
    /// </summary>
    /// <param name="proxyCache">Proxy value.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithATProxyCache(Dictionary<string, string> proxyCache)
    {
        foreach (var item in proxyCache)
        {
            this.atProtocolOptions.ATProxyCache[item.Key] = item.Value;
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
    [Obsolete("Value is no longer used. The session with refresh automatically when the token has expired and EnableAutoRenewSession is enabled")]
    public ATProtocolBuilder WithSessionRefreshInterval(TimeSpan interval)
    {
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