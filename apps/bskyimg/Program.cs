// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommandLine;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;
using Error = CommandLine.Error;

internal class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(async (options) => await RunOptions(options))
            .WithNotParsed(HandleParseError);
    }

    static async Task RunOptions(Options options)
    {
        var username = options.Username ?? System.Environment.GetEnvironmentVariable("BLUESKY_USERNAME");
        var password = options.Password ?? System.Environment.GetEnvironmentVariable("BLUESKY_PASSWORD");

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("You must provide a username and password.");
            return;
        }

        var host = options.Host ?? "bsky.social";

        if (!Uri.TryCreate(host, UriKind.Absolute, out Uri? uri))
        {
            uri = new Uri($"https://{host}");
        }
        
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(true)
            .WithUrl(uri)
            .WithUserAgent("bskyimg")
            .WithLogger(debugLog.CreateLogger("bskyimg"));
        
        var atProtocol = atProtocolBuilder.Build();

        var login = await atProtocol.Server.CreateSessionAsync(username, password);
        if (login.IsT1)
        {
            Console.WriteLine($"{login.AsT1.StatusCode}: {login.AsT1.Detail}");
            return;
        }
        
        var imageList = new List<Image>();
        foreach (string file in options.Images)
        {
            StreamContent content;
            try
            {
                content = StreamContentHelpers.FromFilePath(file);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            var image = await atProtocol.Repo.UploadBlobAsync(content);
            image.Switch(
                success =>
                {
                    imageList.Add(success.Blob.ToImage());
                },
                error =>
                {
                    Console.WriteLine($"{error.StatusCode}, {error.Detail}");
                }
            );

            if (image.IsT1)
            {
                // Failed, break.
                return;
            }
        }

        if (!imageList.Any())
        {
            Console.WriteLine("No Images Uploaded.");
            return;
        }

        var imageEmbedList = new List<ImageEmbed>();
        var altImages = options.AltText?.ToArray() ?? Array.Empty<string>();
        for (var i = 0; i < imageList.Count; i++)
        {
            var img = imageList[i];
            var altText = altImages[i] ?? string.Empty;
            imageEmbedList.Add(new ImageEmbed(img, altText));
        }

        var post = await atProtocol.Repo.CreatePostAsync(options.Text ?? string.Empty, null, new ImagesEmbed(imageEmbedList.ToArray()));
        await post.SwitchAsync(
            async postSuccess =>
            {
                Console.WriteLine($"Post Created: {postSuccess.Uri}");
            },
            async postError =>
            {
                Console.WriteLine($"{postError.StatusCode}, {postError.Detail}");
            }
        );
    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
    }
}

class Options
{
    [Option('i', "images", Required = true, HelpText = "Images to be uploaded and attached. Max of four.")]
    public IEnumerable<string> Images { get; set; }

    [Option('a', "alt-text", HelpText = "Alt Text for Images, attached in order they are given.")]
    public IEnumerable<string>? AltText { get; set; }

    [Option('t', "text", HelpText = "Text to be posted with the images.")]
    public string? Text { get; set; }

    [Option('u', "username", HelpText = "Your username. If empty, it will be filled in by the BLUESKY_USERNAME environment variable.")]
    public string? Username { get; set; }

    [Option('p', "password", HelpText = "Your password. If empty, it will be filled in by the BLUESKY_PASSWORD environment variable.")]
    public string? Password { get; set; }

    [Option('h', "host", Default = "bsky.social", HelpText = "Your host. Defaults to bsky.social")]
    public string? Host { get; set; }
}