// <copyright file="ATProtocolOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace FishyFlip;

public class ATProtocolOptions
{
    public ATProtocolOptions()
    {
        this.HttpClient = new HttpClient();
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
                // new FacetJsonConverter(),
                new CidConverter(),
            },
        };
    }

    public HttpClient HttpClient { get; internal set; }

    public ILogger? Logger { get; internal set; }

    public Uri Url { get; internal set; }

    public string UserAgent { get; internal set; } = "FishyFlip";

    public bool AutoRenewSession { get; internal set; } = false;

    public TimeSpan? SessionRefreshInterval { get; internal set; }

    public JsonSerializerOptions JsonSerializerOptions { get; internal set; }
}