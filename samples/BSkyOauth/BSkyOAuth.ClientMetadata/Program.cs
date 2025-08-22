// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using BSkyOAuth.ClientMetadata;
using FishyFlip.Models;
using Microsoft.AspNetCore.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", async (x) =>
{
    var uri = Uri.TryCreate(x.Request.GetEncodedUrl(), UriKind.Absolute, out var result) ? result : null;
    if (uri is null)
    {
        x.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    uri = uri.Port == 80 || uri.Port == 443 ? new UriBuilder(uri) { Port = -1 }.Uri : uri;
    switch (uri.Host)
    {
        case "localhost":
            await x.Response.WriteAsync("Hello, localhost!");
            break;
        default:
            x.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
    }
});

app.MapGet("/client-metadata.json", async (x) =>
{
    var oauth = new OAuthClientMetadata();
    var uri = Uri.TryCreate(x.Request.GetEncodedUrl(), UriKind.Absolute, out var result) ? result : null;
    if (uri is null)
    {
        x.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    // URI needs to be changed to https unless it is localhost, then it can be http
    uri = uri.Scheme == "http" && (uri.Host != "localhost" || uri.Host != "127.0.0.1") ? new UriBuilder(uri) { Scheme = "https" }.Uri : uri;

    // Remove port if it is 80 or 443
    uri = uri.Port == 80 || uri.Port == 443 ? new UriBuilder(uri) { Port = -1 }.Uri : uri;

    oauth.ClientId = uri.ToString();
    oauth.Scope = GenerateScopes(uri);
    oauth.GrantTypes = new[] { "authorization_code", "refresh_token" };
    oauth.ApplicationType = "native";
    oauth.ResponseTypes = new[] { "code" };
    oauth.TokenEndpointAuthMethod = "none";
    var uriComponents = uri.Host.Split('.').Reverse().ToArray() ?? Array.Empty<string>();
    oauth.RedirectUris = new[] { $"{string.Join(".", uriComponents)}:/callback" };
    await x.Response.WriteAsJsonAsync(oauth, SourceGenerationContext.Default.OAuthClientMetadata);
});

app.Run();

string GenerateScopes(Uri uri)
{
    // atproto, transition:generic, transition:chat.bsky
    var host = uri.Host;
    switch (host)
    {
        default:
            return "atproto";
    }
}