// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
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
    if (args.Message.Record is not null)
    {
        Console.WriteLine($"Record: {args.Message.Record.Type}");
    }
};

await atProtocol.StartSubscribeReposAsync();

var key = Console.ReadKey();

await atProtocol.StopSubscribeReposAsync();