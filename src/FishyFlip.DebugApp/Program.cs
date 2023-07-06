// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.DependencyInjection;
using FishyFlip;
using FishyFlip.Commands;
using FishyFlip.DebugApp;
using FishyFlip.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Debug;
using Sharprompt;

Console.WriteLine("Hello, ATProtocol!");

var debugLog = new DebugLoggerProvider();
var atProtocolBuilder = new ATProtocolBuilder()
    .EnableAutoRenewSession(true)

    // .WithSessionRefreshInterval(TimeSpan.FromSeconds(30))
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
            case Menu.UploadBlob:
                var fileLocation = Prompt.Input<string>("File Location");
                if (File.Exists(fileLocation))
                {
                    var file = System.IO.File.OpenRead(fileLocation);
                    var content = new StreamContent(file);
                    content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(fileLocation));
                    Result<UploadBlobResponse> uploadResult = await atProtocol.UploadBlobAsync(content, CancellationToken.None);
                    uploadResult.SwitchAsync(
                    async upload =>
                    {
                        Console.WriteLine("Upload successful");
                        Console.WriteLine(JsonSerializer.Serialize(upload, atProtocol.Options.JsonSerializerOptions));
                        var postResponse = Prompt.Input<bool>("Post to feed?");
                        if (postResponse)
                        {
                            var newPostText = Prompt.Input<string>("Post text", "Upload Test");
                            var imageEmbeds = new EmbedRecordImage[] { new EmbedRecordImage() { Image = upload.blob } };
                            var embed = new EmbedRecord() { Type = FishyFlip.Tools.Constants.EmbedTypes.Images, Images = imageEmbeds };
                            Result<CreatePostResponse> created2 = await atProtocol.CreatePostAsync(newPostText, embed: embed);
                            created2.Switch(x =>
                            {
                                Console.WriteLine(JsonSerializer.Serialize(x, atProtocol.Options.JsonSerializerOptions));
                            }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                        }

                    },
                     async _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                }
                break;
            case Menu.GetProfile:
                // Get the logged in user's profile
                Result<Profile?> profileResult = await atProtocol.GetProfileAsync(session.Did!, CancellationToken.None);

                profileResult.Switch(
                    profile =>
                {
                    Console.WriteLine("User profile");
                    Console.WriteLine(JsonSerializer.Serialize(profile, atProtocol.Options.JsonSerializerOptions));
                },
                    _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.ResolveHandle:
                // Resolve a user's DID via their handle.
                var handler = session.Handle!;
                Result<HandleResolution?> resolvedHandle = await atProtocol.ResolveHandleAsync(handler, CancellationToken.None);
                resolvedHandle.Switch(
                    handleResolved =>
                {
                    Console.WriteLine("Handle resolved");
                    Console.WriteLine(JsonSerializer.Serialize(handleResolved, atProtocol.Options.JsonSerializerOptions));
                },
                    _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.GetLikes:
                var likes = await atProtocol.GetLikesAsync(new AtUri("at://did:plc:yhgc5rlqhoezrx6fbawajxlh/app.bsky.feed.post/3jzqnuatyjn26"), cancellationToken: CancellationToken.None);
                likes.Switch(
                    refresh =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(refresh, atProtocol.Options.JsonSerializerOptions));
                },
                    _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.GetRepostedBy:
                var reposted = await atProtocol.GetRepostedByAsync(new AtUri("at://did:plc:4g5yti2ybgwdgpa3n4zgh36b/app.bsky.feed.post/3jzmgbpvq5e2f"), 100);
                reposted.Switch(
                    refresh =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(refresh, atProtocol.Options.JsonSerializerOptions));
                }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.GetAuthorFeed:
                var authorHandle = Prompt.Input<string>("Enter Author handle", defaultValue: "drasticactions.dev");
                Result<Timeline> authorFeed = await atProtocol.GetAuthorFeedAsync(AtHandler.Create(authorHandle), 100);
                authorFeed.Switch(
                    refresh =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(refresh, atProtocol.Options.JsonSerializerOptions));
                }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.CreateLike:
                var cid = "bafyreiawb5zegsyxf4m2vgmvhqurat6w2rsam5craret5eegdzxifwgldi";
                var uri = "at://did:plc:4g5yti2ybgwdgpa3n4zgh36b/app.bsky.feed.post/3jzmgbpvq5e2f";

                Result<CreatePostResponse> likeCreated = await atProtocol.CreateLikeAsync(cid, new AtUri(uri));
                likeCreated.Switch(
                    x =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(x, atProtocol.Options.JsonSerializerOptions));
                }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.GetTimeline:
                Result<Timeline> timeline = await atProtocol.GetTimelineAsync(limit: 15);
                timeline.Switch(
                    refresh =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(refresh, atProtocol.Options.JsonSerializerOptions));
                }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.CreateRepost:
                var cid1 = "bafyreiawb5zegsyxf4m2vgmvhqurat6w2rsam5craret5eegdzxifwgldi";
                var uri1 = "at://did:plc:4g5yti2ybgwdgpa3n4zgh36b/app.bsky.feed.post/3jzmgbpvq5e2f";

                Result<CreatePostResponse> repostCreated = await atProtocol.CreateRepostAsync(cid1, new AtUri(uri1));
                repostCreated.Switch(
                    x =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(x, atProtocol.Options.JsonSerializerOptions));
                }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.GetPostThread:
                //var post = new AtUri("at://did:plc:4edsja5x3gixvokf4exuy4to/app.bsky.feed.post/3jzrmx5hh3j2w");
                var post = new AtUri("at://did:plc:qwqccbclvz3v2z5r6cpnogpo/app.bsky.feed.post/3jzrnyd42552x");
                var postsResult = await atProtocol.GetPostThreadAsync(post);
                postsResult.Switch(
                    refresh =>
                    {
                        Console.WriteLine(JsonSerializer.Serialize(refresh, atProtocol.Options.JsonSerializerOptions));
                    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.GetPosts:
                var posts = await atProtocol.GetPostsAsync(new List<AtUri>() { new AtUri("at://did:plc:juem56avlegp5z4hctyxzg7z/app.bsky.feed.post/3jzlxh3fay52u") });
                posts.Switch(
                    refresh =>
                {
                    Console.WriteLine(JsonSerializer.Serialize(refresh, atProtocol.Options.JsonSerializerOptions));
                }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                break;
            case Menu.CreatePost:
                var prompt = Prompt.Input<string>("Enter post text", "Testing in Production, ignore me.");
                if (!string.IsNullOrEmpty(prompt))
                {
                    var facetList = new List<Facet>();
                    var addLink = Prompt.Input<bool>("Add link?");
                    
                    if (addLink)
                    {
                        var linkText = Prompt.Input<string>("Enter link text", "Link to Google");
                        prompt = $"{prompt} {linkText}";
                        int promptStart = prompt.IndexOf(linkText, StringComparison.InvariantCulture);
                        int promptEnd = promptStart + Encoding.Default.GetBytes(linkText).Length;
                        var linkUrl = Prompt.Input<string>("Enter link url", defaultValue: "https://www.google.com");
                        var facet = new Facet(new ByteSlice(promptStart, promptEnd));
                        facet.AddFeature(new Link(linkUrl));
                        facetList.Add(facet);
                    }
                    
                    var addMention = Prompt.Input<bool>("Add Mention?");
                    if (addMention)
                    {
                        var mentionText = Prompt.Input<string>("Enter mention text", "Test Mention");
                        prompt = $"{prompt} {mentionText}";
                        int promptStart = prompt.IndexOf(mentionText, StringComparison.InvariantCulture);
                        int promptEnd = promptStart + Encoding.Default.GetBytes(mentionText).Length;
                        var facet = new Facet(new ByteSlice(promptStart, promptEnd));
                        facet.AddFeature(new Mention(session.Did));
                        facetList.Add(facet);
                    }
                    
                    var test = JsonSerializer.Serialize(facetList.ToArray(), atProtocol.Options.JsonSerializerOptions);
                    Result<CreatePostResponse> created = await atProtocol.CreatePostAsync(prompt, facetList.ToArray());
                    created.Switch(x =>
                    {
                        Console.WriteLine(JsonSerializer.Serialize(x, atProtocol.Options.JsonSerializerOptions));
                    }, _ => Console.WriteLine(JsonSerializer.Serialize(_, atProtocol.Options.JsonSerializerOptions)));
                }
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
            case Menu.Exit:
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

internal enum Menu
{
    UploadBlob,
    GetPostThread,
    GetProfile,
    ResolveHandle,
    RefreshToken,
    GetLikes,
    GetRepostedBy,
    GetPosts,
    GetAuthorFeed,
    GetTimeline,
    CreateRepost,
    CreateLike, 
    CreatePost,
    Exit,
}