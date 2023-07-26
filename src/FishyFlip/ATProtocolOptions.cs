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
    public ATProtocolOptions()
    {
        // HACK: Decodes a message to load the default Cid protocols.
        Cid.Decode("bafyreiezjt5bqt2xpcdfvisud7jrd4zuxygz4ssnuge3ddjcoptanvcnsa");
        this.HttpClient = new HttpClient(new HttpClientHandler { MaxRequestContentBufferSize = int.MaxValue });
        this.JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
        };
        this.Url = new Uri("https://bsky.social");
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
                new EmbedConverter(),
                new ATRecordJsonConverter(),
                new CidConverter(),
            },
        };

        this.UserAgent = $"FishyFlip {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
    }

    /// <summary>
    /// Gets the HttpClient.
    /// </summary>
    public HttpClient HttpClient { get; internal set; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    public ILogger? Logger { get; internal set; }

    /// <summary>
    /// Gets the instance Url.
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
}