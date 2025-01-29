﻿// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CustomTypes;
using FishyFlip;
using FishyFlip.Lexicon;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;
using PeterO.Cbor;

var debugLog = new DebugLoggerProvider();

var atProtocolBuilder = new ATProtocolBuilder([new StatusConverter(SourceGenerationContext.Default)])
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

var pfrazee = ATDid.Create("did:plc:ragtjsm2j2vknwkz3zp4oxrd")!;
var (listRecords, error) = await atProtocol.Repo.ListRecordsAsync(pfrazee, Status.RecordType);

if (error != null)
{
    Console.WriteLine($"Error: {error}");
    return;
}

foreach (var record in listRecords!.Records)
{
    if (record.Value is Status status)
    {
        Console.WriteLine($"Status: {status.StatusValue}");
    }
}

// Unknown ATObject that has not been registered.
// We can still get the object and print it out.
(listRecords, error) = await atProtocol.Repo.ListRecordsAsync(pfrazee, "com.example.status");

if (error != null)
{
    Console.WriteLine($"Error: {error}");
    return;
}

foreach (var record in listRecords!.Records)
{
    if (record.Value is UnknownATObject status)
    {
        // From UnknownATObject.Json
        Console.WriteLine($"Unknown: {status.ToJson()}");
    }
}

// This does a checkout of the repo, which uses CBORObjects.
// We can get the custom type with a ICustomATObjectCBORConverter.
// And using it in the ToATObject method.
var cborConverter = new StatusCBORConverter();
var checkoutResult = await atProtocol.Sync.GetRepoAsync(pfrazee!, HandleProgressStatus);

void HandleProgressStatus(CarProgressStatusEvent e)
{
    var cid = e.Cid;
    var bytes = e.Bytes;
    var cborObject = CBORObject.DecodeFromBytes(bytes);

    // These objects can be Frames.
    // We can check in advance if this is an ATObject.
    if (cborObject.IsATObject())
    {
        var record = cborObject.ToATObject([cborConverter]);

        if (record is Status status)
        {
            Console.WriteLine($"Status: {status.StatusValue}");

            // Break out of the app
            Environment.Exit(0);
        }

        if (record is UnknownATObject unknown)
        {
            // From UnknownATObject.CBORObject
            Console.WriteLine($"Unknown: {unknown.ToJson()}");
        }
    }
}