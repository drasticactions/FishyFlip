// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using Microsoft.Extensions.Logging.Debug;

Console.WriteLine("Hello, Jetstream!");

var debugLog = new DebugLoggerProvider();

// You can set a custom url with WithInstanceUrl
var jetstreamBuilder = new ATJetStreamBuilder()
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atWebProtocol = jetstreamBuilder.Build();

atWebProtocol.OnConnectionUpdated += (sender, args) =>
{
    Console.WriteLine($"Connection Updated: {args.State}");
};

atWebProtocol.OnRecordReceived += (sender, args) =>
{
    Console.WriteLine($"Record Received: {args.Record.Kind}");
};

await atWebProtocol.ConnectAsync();

var key = Console.ReadKey();

await atWebProtocol.CloseAsync();