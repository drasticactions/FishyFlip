// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Ipfs;
using Microsoft.Extensions.Logging.Debug;
using System.Security.Cryptography;
using System.Text;
using FishyFlip.Lexicon.App.Bsky.Embed;
using FishyFlip.Lexicon.App.Bsky.Feed;

Console.WriteLine("Hello, ATProtocol Firehose!");

var debugLog = new DebugLoggerProvider();

// You can set a custom url with WithInstanceUrl
var atWebProtocolBuilder = new ATWebSocketProtocolBuilder()
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atWebProtocol = atWebProtocolBuilder.Build();

atWebProtocol.OnSubscribedRepoMessage += (sender, args) =>
{
    Task.Run(() => HandleMessageAsync(args.Message)).FireAndForget();
};

await atWebProtocol.StartSubscribeReposAsync();

var key = Console.ReadKey();

await atWebProtocol.StopSubscriptionAsync();

async Task HandleMessageAsync(SubscribeRepoMessage message)
{
    if (message.Commit is null)
    {
        return;
    }

    var orgId = message.Commit.Repo;

    if (orgId is null)
    {
        return;
    }

    if (message.Record is not null)
    {
        Console.WriteLine($"Record: {message.Record.Type}");

        if (message.Record is Post post)
        {
            // The Actor Did.
            var did = message.Commit.Repo;

            // Commit.Ops are the actions used when creating the message.
            // In this case, it's a create record for the post.
            // The path contains the post action and path, we need the path, so we split to get it.
            var url = $"https://bsky.app/profile/{did}/post/{message.Commit.Ops![0]!.Path!.Split('/').Last()}";
           // Console.WriteLine($"Post URL: {url}, from {repo.Handle}");

            if (post.Reply is not null)
            {
                Console.WriteLine($"Reply Root: {post.Reply.Root}");
                Console.WriteLine($"Reply Parent: {post.Reply.Parent}");
            }

            if (post.Embed is EmbedVideo videoEmbed)
            {
                // https://video.bsky.app/watch/did%3Aplc%3Acxe5e4ldjfvryf5dqvopdq3v/bafkreiefakrdmclohastskuauwurbtx3tnu2drjpnirsoroyalq5nqr73a/playlist.m3u8
               // var link = videoEmbed.Video?.Ref?.Link;
               // var linkString = link.ToString();
               // Console.WriteLine($"Video Link: https://video.bsky.app/watch/{did}/{linkString}/playlist.m3u8");
            }
        }

        static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}

public static class TaskExtensions
{
    public static void FireAndForget(this Task task, Action<Exception> errorHandler = null)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        task.ContinueWith(t =>
        {
            if (errorHandler != null && t.IsFaulted)
                errorHandler(t.Exception);
        }, TaskContinuationOptions.OnlyOnFaulted);

        // Avoiding warning about not awaiting the fire-and-forget task.
        // However, since the method is intended to fire and forget, we don't actually await it.
#pragma warning disable CS4014
        task.ConfigureAwait(false);
#pragma warning restore CS4014
    }
}