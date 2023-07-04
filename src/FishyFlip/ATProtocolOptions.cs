// <copyright file="ATProtocolOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;

namespace FishyFlip;

public class ATProtocolOptions
{
    public ATProtocolOptions()
    {
        this.HttpClient = new HttpClient();
        this.HttpClient.DefaultRequestHeaders.Add("User-Agent", "FishyFlip");
    }

    public HttpClient HttpClient { get; internal set; }
}