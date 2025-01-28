// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

/*
 * This is a sandbox app for testing FishyFlip functions.
 */

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FishyFlip;
using FishyFlip.Lexicon;
using FishyFlip.Lexicon.App.Bsky.Actor;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.Com.Atproto.Repo;
using FishyFlip.Lexicon.Com.Atproto.Sync;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;
using PeterO.Cbor;

Console.WriteLine("Hello, Sandbox!");

var debugLog = new DebugLoggerProvider();

// You can set a custom url with WithInstanceUrl
var atWebProtocolBuilder = new ATWebSocketProtocolBuilder()
 .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atWebProtocol = atWebProtocolBuilder.Build();

var atProtocolBuilder = new ATProtocolBuilder()
 .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

var (test, error) = await atProtocol.Repo.ListRecordsAsync(ATIdentifier.Create("drasticactions.xn--q9jyb4c")!, "com.mackerelmedia.media");

if (error != null)
{
 Console.WriteLine($"Error: {error}");
 return;
}

if (test == null)
{
 Console.WriteLine("No records found.");
 return;
}

foreach (var record in test.Records)
{
 Console.WriteLine($"Record: {record.Value}");
}
