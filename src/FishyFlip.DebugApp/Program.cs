// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text.Json;
using CommunityToolkit.Mvvm.DependencyInjection;
using FishyFlip;
using FishyFlip.Commands;
using FishyFlip.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Debug;
using Sharprompt;

Console.WriteLine("Hello, ATProtocol!");

var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATProtocolBuilder()
    .EnableAutoRenewSession(true)
    .WithSessionRefreshInterval(TimeSpan.FromSeconds(30))
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

string userName = Environment.GetEnvironmentVariable("BLUESKY_USERNAME")!;
string password = Environment.GetEnvironmentVariable("BLUESKY_PASSWORD")!;

Login command = new(userName, password);
Result<Session> result = await atProtocol.LoginAsync(command, CancellationToken.None);

await result.SwitchAsync(
    async session =>
{
    Console.WriteLine("Logged in");
    Console.WriteLine(JsonSerializer.Serialize(session, atProtocol.Options.JsonSerializerOptions));

    var exit = false;

    while (!exit)
    {
        var option = Prompt.Select<Menu>("Menu");

        switch (option)
        {
            case Menu.GetProfile:
                // Get the logged in user's profile
                Result<Profile?> profileResult = await atProtocol.GetProfileAsync(CancellationToken.None);

                profileResult.Switch(
                    profile =>
                {
                    Console.WriteLine("User profile");
                    Console.WriteLine(JsonSerializer.Serialize(profile, atProtocol.Options.JsonSerializerOptions));
                },
                    _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.ResolveHandle:
                // Resolve a user's DID
                Result<HandleResolution?> resolvedHandle = await atProtocol.ResolveHandleAsync(session.Handle!.Identifier!, CancellationToken.None);
                resolvedHandle.Switch(
                    handleResolved =>
                {
                    Console.WriteLine("Handle resolved");
                    Console.WriteLine(JsonSerializer.Serialize(handleResolved, atProtocol.Options.JsonSerializerOptions));
                },
                    _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.RefreshToken:
                // Refresh the token
                Result<Session> result1 = await atProtocol.RefreshSessionAsync(session, CancellationToken.None);
                result1.Switch(
                    refresh =>
                {
                    Console.WriteLine("Token refreshed");
                    Console.WriteLine(JsonSerializer.Serialize(refresh, atProtocol.Options.JsonSerializerOptions));
                },
                    _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            default:
                exit = true;
                break;
        }
    }
},
    _ =>
{
    Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions));
    return Task.CompletedTask;
});

Console.WriteLine("Press any key to exit...");

internal enum Menu
{
    GetProfile,
    ResolveHandle,
    RefreshToken,

    // GetLikes,
    // GetRepostedBy,
    // GetPosts,
    // GetAuthorFeed,
    // GetTimeline,
    // CreateLike,
    // CreatePost,
    Exit,
}