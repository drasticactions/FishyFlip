// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;

Console.WriteLine("PlcDirectoryGet");

var protocolBuilder = new ATProtocolBuilder();
var protocol = protocolBuilder.Build();

var did = ATDid.Create("did:plc:j67mwmangcbxch7knfm7jo2b")!;
Console.WriteLine($"Did: {did}");

var (result, error) = await protocol.PlcDirectory.GetDidDocAsync(did);
if (error != null)
{
    Console.WriteLine($"Error: {error}");
    return;
}

if (result == null)
{
    Console.WriteLine("Result is null.");
    return;
}

Console.WriteLine($"Result: {result}");