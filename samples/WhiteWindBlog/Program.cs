// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection.Metadata;
using ConsoleAppFramework;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;
using WhiteWindLib;
using WhiteWindLib.Models.WhiteWind;

var app = ConsoleApp.Create();

app.Add("create-entry", async (
    string handle,
    string password,
    string title,
    string content,
    CancellationToken cancellationToken = default) =>
{
    var atProtocol = await CreateSessionAsync(handle, password, cancellationToken);

    var blog = new WhiteWindBlog(atProtocol);

    var (result, error) = await blog.CreateEntryAsync(title, content, cancellationToken: cancellationToken);
    if (error is not null)
    {
        Console.WriteLine($"Error: {error}");
        return;
    }

    Console.WriteLine($"Entry created: {result!.Uri}");
});

app.Add("delete-entry", async (
    string handle,
    string password,
    string atUrl,
    CancellationToken cancellationToken = default) =>
{
    if (!ATUri.TryCreate(atUrl, out var atUri))
    {
        Console.WriteLine("Invalid atUrl.");
        return;
    }

    var atProtocol = await CreateSessionAsync(handle, password, cancellationToken);

    var blog = new WhiteWindBlog(atProtocol);

    var (result, error) = await blog.DeleteEntryAsync(atUri!.Did!, atUri.Rkey, cancellationToken: cancellationToken);
    if (error is not null)
    {
        Console.WriteLine($"Error: {error}");
        return;
    }

    Console.WriteLine($"Entry deleted: {atUrl}");
});

app.Add("list-entries", async (string handle = "", string did = "", CancellationToken cancellationToken = default) =>
{
    var debugLogger = new DebugLoggerProvider();
    var atProtocolBuilder = new ATProtocolBuilder();
    var atProtocol = atProtocolBuilder
        .WithInstanceUrl(new Uri("https://bsky.social"))
        .WithLogger(debugLogger.CreateLogger("WhiteWindLib"))
        .Build();

    var blog = new WhiteWindBlog(atProtocol);

    ATDid? atDid = null;
    if (!string.IsNullOrWhiteSpace(handle))
    {
        if (!ATHandle.TryCreate(handle, out var atHandle))
        {
            Console.WriteLine("Invalid handle.");
            return;
        }

        var (handleResolution, handleError) = await atProtocol.Identity.ResolveHandleAsync(atHandle!, cancellationToken);
        if (handleError is not null)
        {
            Console.WriteLine($"Error: {handleError}");
            return;
        }

        atDid = handleResolution?.Did;
    }

    if (!string.IsNullOrWhiteSpace(did))
    {
        if (!ATDid.TryCreate(did, out var atDid2))
        {
            Console.WriteLine("Invalid did.");
            return;
        }

        atDid = atDid2;
    }

    if (atDid is null)
    {
        Console.WriteLine("No handle or did provided.");
        return;
    }

    var (result, error) = await blog.GetAuthorEntriesAsync(atDid, cancellationToken: cancellationToken);
    if (error is not null)
    {
        Console.WriteLine($"Error: {error}");
        return;
    }

    if (result?.Records.Length == 0)
    {
        Console.WriteLine("No entries found.");
        return;
    }

    foreach (var entry in result!.Records)
    {
        Console.WriteLine($"Title: {entry.Value!.Title}");
        Console.WriteLine($"Visibility: {entry.Value.Visibility}");
        Console.WriteLine($"Created: {entry.Value.CreatedAt}");
        Console.WriteLine($"Uri: {entry.Uri!}");
        Console.WriteLine($"Did: {entry.Uri!.Did}");
        Console.WriteLine($"Rkey: {entry.Uri!.Rkey}");
        if (entry.Value.Ogp is not null)
        {
            Console.WriteLine($"Ogp: {entry.Value.Ogp}");
        }

        Console.WriteLine("----");
        Console.WriteLine();
    }
});

app.Run(args);

static async Task<ATProtocol> CreateSessionAsync(string handle, string password, CancellationToken cancellationToken = default)
{
    var debugLogger = new DebugLoggerProvider();
    var atProtocolBuilder = new ATProtocolBuilder();
    var atProtocol = atProtocolBuilder
        .WithInstanceUrl(new Uri("https://bsky.social"))
        .WithLogger(debugLogger.CreateLogger("WhiteWindLib"))
        .Build();

    var (result, error) = await atProtocol.Server.CreateSessionAsync(handle, password, cancellationToken);
    if (error is not null)
    {
        throw new Exception($"Error: {error}");
    }

    return atProtocol;
}