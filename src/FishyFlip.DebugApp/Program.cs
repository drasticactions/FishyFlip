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

string userName = Environment.GetEnvironmentVariable("BLUESKY_USERNAME")!;
string password = Environment.GetEnvironmentVariable("BLUESKY_PASSWORD")!;

await atProtocol.StartSubscribeReposAsync();

while (true)
{
}