// <copyright file="ATProtocolOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools.Json;
using System.Collections.Concurrent;
using System.Net;

namespace FishyFlip;

/// <summary>
/// AT Protocol Options.
/// </summary>
public class ATProtocolOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocolOptions"/> class.
    /// </summary>
    /// <param name="customEmbedConverters">Customer JSON Converters for Embed.</param>
    /// <param name="customAtRecordConverters">Customer JSON Converters for ATRecord.</param>
    public ATProtocolOptions()
    {
        this.Url = new Uri(Constants.Urls.ATProtoServer.PublicApi);
        this.JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
            Converters = { new ATUriJsonConverter(), new ATObjectJsonConverter() },
        };

        this.SourceGenerationContext = new SourceGenerationContext(this.JsonSerializerOptions);
        this.UserAgent = $"FishyFlip {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    public ILogger? Logger { get; internal set; }

    /// <summary>
    /// Gets the instance Url.
    /// Defaults to https://public.api.bsky.app.
    /// </summary>
    public Uri Url { get; internal set; }

    /// <summary>
    /// Gets the user agent. Defaults to FishyFlip.
    /// </summary>
    public string UserAgent { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether to auto-renew password-based sessions.
    /// This does not affect OAuth sessions. These will always be renewed.
    /// </summary>
    public bool AutoRenewSession { get; internal set; } = false;

    /// <summary>
    /// Gets the JsonSerializerOptions.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; internal set; }

    /// <summary>
    /// Gets the label parameters.
    /// </summary>
    public HashSet<LabelParameter> LabelParameters { get; internal set; } = new HashSet<LabelParameter>();

    /// <summary>
    /// Gets a value indicating whether to switch to the service endpoint upon login, if available.
    /// If it's not available, the original instance URL will be used.
    /// </summary>
    public bool UseServiceEndpointUponLogin { get; internal set; } = true;

    /// <summary>
    /// Gets the Ozone Proxy Header.
    /// </summary>
    public string OzoneProxyHeader { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets the Did Cache.
    /// </summary>
    internal ConcurrentDictionary<string, string> DidCache { get; } = new();

    /// <summary>
    /// Gets the source generation context.
    /// </summary>
    internal SourceGenerationContext SourceGenerationContext { get; }

    /// <summary>
    /// Gets the label definitions header.
    /// </summary>
    internal string LabelDefinitionsHeader => string.Join(", ", this.LabelParameters.Select(p => p.ToString()));

    /// <summary>
    /// Generates an HttpClient based on the options.
    /// </summary>
    /// <param name="protocol">Protocol.</param>
    /// <param name="handler">Handler.</param>
    /// <returns><see cref="HttpClient"/>.</returns>
    internal HttpClient GenerateHttpClient(ATProtocol protocol, HttpMessageHandler? handler = null)
    {
        var httpClient = handler is not null ? new HttpClient(handler) : new HttpClient(new ATProtocolDelegatingHandler(protocol));

        httpClient.DefaultRequestHeaders.Add(Constants.HeaderNames.UserAgent, this.UserAgent);
        httpClient.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
#if NET8_0_OR_GREATER
        // From https://github.com/drasticactions/FishyFlip/pull/107
        httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
#endif
        return httpClient;
    }
}