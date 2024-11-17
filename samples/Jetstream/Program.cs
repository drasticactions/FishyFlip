// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
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

atWebProtocol.OnRecordReceived += (sender, args) =>
{
    Console.WriteLine($"Record Received: {args.Record.Kind}");
    switch (args.Record.Kind)
    {
        case ATWebSocketEvent.Commit:
            Console.WriteLine($"Commit: {args.Record.Commit?.Operation}");
            switch (args.Record.Commit?.Operation)
            {
                case ATWebSocketCommitType.Create:
                    Console.WriteLine($"Create: {args.Record.Commit?.Collection}");
                    switch (args.Record.Commit?.Collection)
                    {
                        case FishyFlip.Lexicon.App.Bsky.Feed.Post.RecordType:
                            Console.WriteLine($"Post: {args.Record.Commit?.Collection}");
                            var t = args.Record.Commit?.Record as FishyFlip.Lexicon.App.Bsky.Feed.Post;
                            if (t?.Embed is not null)
                            {
                                Console.WriteLine($"Embed: {t.Embed}");
                            }
                            break;
                        case FishyFlip.Lexicon.App.Bsky.Feed.Threadgate.RecordType:
                            Console.WriteLine($"ThreadGate: {args.Record.Commit?.Collection}");
                            var r = args.Record.Commit?.Record as FishyFlip.Lexicon.App.Bsky.Feed.Threadgate;
                            if (r != null && (r.HiddenReplies?.Any() ?? false))
                            {
                                Console.WriteLine($"Post: {r.Post}");
                            }

                            break;
                        default:
                            Console.WriteLine($"Unknown Collection: {args.Record.Commit?.Collection}");
                            break;
                    }

                    break;
                case ATWebSocketCommitType.Update:
                    Console.WriteLine($"Update: {args.Record.Commit?.Collection}");
                    break;
                case ATWebSocketCommitType.Delete:
                    Console.WriteLine($"Delete: {args.Record.Commit?.Collection}");
                    break;
                case ATWebSocketCommitType.Unknown:
                default:
                    Console.WriteLine($"Unknown: {args.Record.Commit?.Operation}");
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
            Console.WriteLine($"Unknown: {args.Record.Kind}");
            break;
    }
};

await atWebProtocol.ConnectAsync();

var key = Console.ReadKey();

await atWebProtocol.CloseAsync();