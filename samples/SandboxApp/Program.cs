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