// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Lexicon;
using FishyFlip.Lexicon.App.Bsky.Embed;
using FishyFlip.Models;
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

// OnRecordReceived returns ATObjectWebSocket records,
// Which contain ATObject records.
// If you wish to receive all records being returned,
// subscribe to OnRawMessageReceived.
atWebProtocol.OnRecordReceived += (sender, args) =>
{
    Console.WriteLine($"Record Received: {args.Record.Kind}");
    switch (args.Record.Kind)
    {
        case ATWebSocketEvent.Commit:
            Console.WriteLine($"Commit: {args.Record.Commit?.Operation}");
            switch (args.Record.Commit?.Operation)
            {
                // Create is when a new record is created.
                case ATWebSocketCommitType.Create:
                    Console.WriteLine($"Create: {args.Record.Commit?.Collection}");

                    // Record is an ATWebSocketRecord, which contains the actual record inside Commit.
                    switch (args.Record.Commit?.Record)
                    {
                        case FishyFlip.Lexicon.App.Bsky.Feed.Post post:
                            Console.WriteLine($"Post: {post.ToJson()}");
                            break;
                        case FishyFlip.Lexicon.App.Bsky.Feed.Threadgate threadgate:
                            Console.WriteLine($"ThreadGate: {threadgate.ToJson()}");
                            break;
                        default:
                            if (args.Record.Commit?.Record is { } obj)
                            {
                                Console.WriteLine($"ATObject: {obj.ToJson()}");
                            }

                            break;
                    }
                    break;
            }

            break;
        case ATWebSocketEvent.Identity:
            Console.WriteLine($"Identity: {args.Record.Identity?.Did}");
            break;
        case ATWebSocketEvent.Account:
            Console.WriteLine($"Account: {args.Record.Account?.Did}");
            break;
        case ATWebSocketEvent.Unknown:
        default:
            Console.WriteLine($"{args.Record.Kind}");
            break;
    }
};

await atWebProtocol.ConnectAsync();

var key = Console.ReadKey();

await atWebProtocol.CloseAsync();