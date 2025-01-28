// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using CustomTypes;
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

var debugLog = new DebugLoggerProvider();

var atProtocolBuilder = new ATProtocolBuilder([new StatusConverter(SourceGenerationContext.Default)])
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

var (listRecords, error) = await atProtocol.Repo.ListRecordsAsync(ATIdentifier.Create("pfrazee.com")!, Status.RecordType);

if (error != null)
{
    Console.WriteLine($"Error: {error}");
    return;
}

foreach (var record in listRecords!.Records)
{
    Console.WriteLine($"Record: {record}");
}

