// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

/*
 * This is a sandbox app for testing FishyFlip functions.
 */

using System.Text.Json.Serialization;
using FishyFlip.Lexicon.Com.Whtwnd.Blog;
using FishyFlip.Models;
using FishyFlip.Xrpc.Tools;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateSlimBuilder(args);

// Register controllers
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new ATProtoModelBinderProvider());
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapGet("/", () => "Hello, Sandbox!");

// Map controller endpoints
app.MapControllers();

app.Run();

public class TestComWhtwndBlog : FishyFlip.Xrpc.Lexicon.Com.Whtwnd.Blog.BlogController
{
    public override Task<Results<Ok<GetAuthorPostsOutput>, BadRequest>> GetAuthorPostsAsync(ATDid author, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<Results<Ok<GetEntryMetadataByNameOutput>, BadRequest>> GetEntryMetadataByNameAsync(ATIdentifier author, string entryTitle, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<Results<Ok<GetMentionsByEntryOutput>, BadRequest>> GetMentionsByEntryAsync(ATUri postUri, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<Results<Ok<NotifyOfNewEntryOutput>, BadRequest>> NotifyOfNewEntryAsync(ATUri entryUri, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}