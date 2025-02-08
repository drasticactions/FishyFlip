// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Ipfs;
using Microsoft.Extensions.Logging.Debug;
using System.Security.Cryptography;
using System.Text;
using FishyFlip.Lexicon.App.Bsky.Embed;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.Com.Atproto.Repo;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Cms;

Console.WriteLine("Hello, ATProtocol Firehose!");

var options = new ConsoleLoggerOptions();
var debugLog = new ConsoleLoggerProvider(new OptionsMonitor<ConsoleLoggerOptions>(
    new OptionsFactory<ConsoleLoggerOptions>(
        Array.Empty<IConfigureOptions<ConsoleLoggerOptions>>(),
        Array.Empty<IPostConfigureOptions<ConsoleLoggerOptions>>()),
    Array.Empty<IOptionsChangeTokenSource<ConsoleLoggerOptions>>(),
    new OptionsCache<ConsoleLoggerOptions>()));

var log = debugLog.CreateLogger("FishyFlip");

// You can set a custom url with WithInstanceUrl
var atWebProtocolBuilder = new ATWebSocketProtocolBuilder()
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atWebProtocol = atWebProtocolBuilder.Build();

var atLabelWebProtocolBuilder = new ATWebSocketProtocolBuilder()
    .WithInstanceUrl(new Uri("https://mod.bsky.app"))
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atLabelWebProtocol = atLabelWebProtocolBuilder.Build();

var atProtocolBuilder = new ATProtocolBuilder()
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

atWebProtocol.OnConnectionUpdated += (sender, e) =>
{
    log.LogInformation($"Connection Updated: {e.State}");
};

atWebProtocol.OnSubscribedRepoMessage += (sender, e) =>
{
    log.LogInformation($"Message: {DateTime.UtcNow.Ticks}");
};

atLabelWebProtocol.OnSubscribedLabelMessage += (sender, e) =>
{
    log.LogInformation($"Message: {DateTime.UtcNow.Ticks}");
};

atWebProtocol.OnMessageReceived += (sender, e) =>
{
    log.LogInformation($"Byte Message: {DateTime.UtcNow.Ticks}");
};

atWebProtocol.OnRecordReceived += (sender, e) =>
{
    log.LogInformation($"Record: {e.Record?.Type}");
};

await atWebProtocol.StartSubscribeReposAsync();

var key = Console.Read();

await atWebProtocol.StopSubscriptionAsync();

async Task HandleMessageAsync(SubscribeRepoMessage message)
{
    if (message.Commit is null)
    {
        return;
    }

    var orgId = message.Commit.Repo;

    if (orgId is null)
    {
        return;
    }

    if (message.Record is not null)
    {
        Console.WriteLine($"Record: {message.Record.ToJson()}");
    }
}

public static class TaskExtensions
{
    public static void FireAndForget(this Task task, Action<Exception> errorHandler = null)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        task.ContinueWith(t =>
        {
            if (errorHandler != null && t.IsFaulted)
                errorHandler(t.Exception);
        }, TaskContinuationOptions.OnlyOnFaulted);

        // Avoiding warning about not awaiting the fire-and-forget task.
        // However, since the method is intended to fire and forget, we don't actually await it.
#pragma warning disable CS4014
        task.ConfigureAwait(false);
#pragma warning restore CS4014
    }
}