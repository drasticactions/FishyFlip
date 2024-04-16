// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotMake.CommandLine;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;
using PeterO.Cbor;

await Cli.RunAsync<RootCommand>(args);

/// <summary>
/// Root CLI command.
/// </summary>
[CliCommand(Description = "fetchrepliestopost", ShortFormAutoGenerate = false)]
#pragma warning disable SA1649 // File name should match first type name
public class RootCommand
#pragma warning restore SA1649 // File name should match first type name
{
    /// <summary>
    /// Gets or sets the ATUri.
    /// </summary>
    [CliArgument(Description = "Post URI")]
    public required string PostUri { get; set; }

    /// <summary>
    /// Gets or sets the Instance.
    /// Only override if you know the PDS you want to connect to.
    /// Defaults to https://public.api.bsky.app.
    /// </summary>
    [CliOption(Description = "BSky Instance")]
    public string? Instance { get; set; } = "https://public.api.bsky.app";

    public async Task RunAsync()
    {
        if (Uri.TryCreate(this.Instance, UriKind.Absolute, out var uri) == false)
        {
            Console.WriteLine("Invalid instance URL");
            return;
        }

        if (ATUri.TryCreate(this.PostUri, out var postUri) == false)
        {
            Console.WriteLine("Invalid AT URI");
            return;
        }

        var debugFactory = new DebugLoggerProvider();
        var logger = debugFactory.CreateLogger("ATProtocolBuilder");

        var protocolBuilder = new ATProtocolBuilder().WithLogger(logger).WithInstanceUrl(uri);
        using var protocol = protocolBuilder.Build();

        var (post, error) = await protocol.Feed.GetPostThreadAsync(postUri);
        if (error != null)
        {
            Console.WriteLine($"Error: {error}");
            return;
        }

        this.PrintPost(post!.Thread.Post!);

        if (!post!.Thread.Replies?.Any() ?? true)
        {
            return;
        }

        foreach (var reply in post!.Thread.Replies)
        {
            Console.WriteLine("Reply:");
            this.PrintPost(reply.Post!);
        }
    }

    private void PrintPost(PostView postView)
    {
        Console.WriteLine($"Post: {postView.Uri}");
        Console.WriteLine($"Author: {postView.Author?.DisplayName ?? "Unknown"}");
        Console.WriteLine($"Content: {postView.Record!.Text}");
        Console.WriteLine($"Replies: {postView.ReplyCount}");
        Console.WriteLine($"Likes: {postView.LikeCount}");
        Console.WriteLine($"Created At: {postView.Record!.CreatedAt}");
    }
}