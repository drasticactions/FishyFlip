// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;

Console.WriteLine("Hello, ATProtocol Firehose!");

var debugLog = new DebugLoggerProvider();

// You can set a custom url with WithInstanceUrl
var atProtocolBuilder = new ATWebSocketProtocolBuilder()
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

atProtocol.OnSubscribedRepoMessage += (sender, args) =>
{
    Task.Run(() => HandleMessageAsync(args.Message)).FireAndForgetSafeAsync();
};

await atProtocol.StartSubscribeReposAsync();

var key = Console.ReadKey();

await atProtocol.StopSubscriptionAsync();

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
        Console.WriteLine($"Record: {message.Record.Type}");

        if (message.Record is Post post)
        {
            // The Actor Did.
            var did = message.Commit.Repo;
            // Commit.Ops are the actions used when creating the message.
            // In this case, it's a create record for the post.
            // The path contains the post action and path, we need the path, so we split to get it.
            var url = $"https://bsky.app/profile/{did}/post/{message.Commit.Ops![0]!.Path!.Split("/").Last()}";
            Console.WriteLine($"Post URL: {url}");
        }
    }
}