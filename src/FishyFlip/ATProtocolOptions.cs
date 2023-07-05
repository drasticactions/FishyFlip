// <copyright file="ATProtocolOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Text.Json;
using FishyFlip.Tools;
using FishyFlip.Tools.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            Converters =
            {
                new AtUriJsonConverter(),
            },
        };
    }

    public HttpClient HttpClient { get; internal set; }

    public ILogger? Logger { get; internal set; }

    public string Url { get; internal set; } = "https://bsky.social";

    public string UserAgent { get; internal set; } = "FishyFlip";

    public bool TrackSession { get; internal set; } = true;

    public bool AutoRenewSession { get; internal set; } = false;

    public TimeSpan? SessionRefreshInterval { get; internal set; }

    public JsonSerializerOptions JsonSerializerOptions { get; internal set; }
}