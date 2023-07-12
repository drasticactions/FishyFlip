// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;

Console.WriteLine("Hello, ATProtocol!");

var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATProtocolBuilder()
    .EnableAutoRenewSession(true)

    // .WithSessionRefreshInterval(TimeSpan.FromSeconds(30))
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

atProtocol.OnSubscribedRepoMessage += (sender, args) =>
{
    Task.Run(() => HandleMessageAsync(args.Message)).FireAndForgetSafeAsync();
};

await atProtocol.StartSubscribeReposAsync();

var key = Console.ReadKey();

await atProtocol.StopSubscribeReposAsync();

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
        switch (message.Record.Type)
        {
            case Constants.FeedType.Like:
                break;
        }

        Console.WriteLine($"Record: {message.Record.Type}");
    }
}