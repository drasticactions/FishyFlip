// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Http.Extensions;
using SandboxWebMvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapGet("/client-metadata.json", async (x) =>
{
    var oauth = new FishyFlip.Models.OAuthClientMetadata();
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
    var redirectUri = $"{uri.Scheme}://{uri.Host}:{uri.Port}/callback";
    oauth.RedirectUris = new[] { redirectUri };
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
