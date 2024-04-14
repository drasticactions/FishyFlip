// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using FishyFlip.Models;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, SourceGenerationContext.Default);
});

var app = builder.Build();

var xrpcApis = app.MapGroup("/xrpc");
xrpcApis.MapGet("/app.bsky.feed.getFeedSkeleton", (string feed) =>
{
    var feedPost = SkeletonFeedPost.Create(ATUri.Create("at://did:example:1234/app.bsky.feed.post/1"));
    return Results.Ok(new[] { feedPost });
});

app.MapGet("/", () => Results.Ok("Welcome to the Fishy Flip Feed Generator!"));

app.Run();

/// <summary>
/// ATProtocol Message Source Generation Context.
/// </summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
[JsonSerializable(typeof(ATUri))]
[JsonSerializable(typeof(SkeletonFeedPost))]
[JsonSerializable(typeof(SkeletonFeedPost[]))]
[JsonSerializable(typeof(SkeletonReasonRepost))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
