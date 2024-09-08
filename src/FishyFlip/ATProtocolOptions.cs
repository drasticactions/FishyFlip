// <copyright file="ATProtocolOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    public ATProtocolOptions(IReadOnlyList<ICustomEmbedConverter>? customEmbedConverters = default, IReadOnlyList<ICustomATRecordConverter>? customAtRecordConverters = default)
    {
        this.Url = new Uri(Constants.Urls.ATProtoServer.SocialApi);
        this.JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
            Converters =
            {
                new AtUriJsonConverter(),
                new AtHandlerJsonConverter(),
                new AtDidJsonConverter(),
                new EmbedConverter(customEmbedConverters),
                new ATRecordJsonConverter(customAtRecordConverters),
                new ATCidConverter(),
            },
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
    /// Gets a value indicating whether to auto renew sessions.
    /// </summary>
    public bool AutoRenewSession { get; internal set; } = false;

    /// <summary>
    /// Gets the session refresh interval.
    /// </summary>
    public TimeSpan? SessionRefreshInterval { get; internal set; }

    /// <summary>
    /// Gets the JsonSerializerOptions.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether to switch to the service endpoint upon login, if available.
    /// If it's not available, the original instance URL will be used.
    /// </summary>
    public bool UseServiceEndpointUponLogin { get; internal set; } = true;

    /// <summary>
    /// Gets the source generation context.
    /// </summary>
    internal SourceGenerationContext SourceGenerationContext { get; }

    /// <summary>
    /// Generates an HttpClient based on the options.
    /// </summary>
    /// <param name="handler">Handler.</param>
    /// <returns><see cref="HttpClient"/>.</returns>
    internal HttpClient GenerateHttpClient(HttpMessageHandler? handler = default)
    {
        var httpClient = new HttpClient(handler ?? new HttpClientHandler { MaxRequestContentBufferSize = int.MaxValue });
        httpClient.DefaultRequestHeaders.Add(Constants.HeaderNames.UserAgent, this.UserAgent);
        httpClient.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
        httpClient.BaseAddress = this.Url;
        return httpClient;
    }
}