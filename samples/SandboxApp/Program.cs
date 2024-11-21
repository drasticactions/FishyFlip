// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

/*
 * This is a sandbox app for testing FishyFlip functions.
 */

using FishyFlip;
using FishyFlip.Lexicon;
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

// Downloading Repo files requires the PDS of the given user.
var atProtocolBuilder = new ATProtocolBuilder()
 .WithInstanceUrl(new Uri("https://puffball.us-east.host.bsky.network"))
 .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

var checkoutResult = await atProtocol.Sync.GetRepoAsync(ATDid.Create("did:plc:6d3q55s45v6o57gwrxnwzhlz")!, HandleProgressStatus);

async void HandleProgressStatus(CarProgressStatusEvent e)
{
 var cid = e.Cid;
 var bytes = e.Bytes;
 var cborObject = CBORObject.DecodeFromBytes(bytes);
 if (cborObject.IsATObject())
 {
  var record = cborObject.ToATObject();

  // Print the record as JSON.
  Console.WriteLine($"Record: {record.ToJson()}");
 }
}