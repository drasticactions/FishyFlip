// <copyright file="ProgramCommands.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommandLine;

internal static class ProgramCommands
{
    internal static async Task GetRecordAsync(GetRecordOptions options)
    {
    }

    internal static async Task PostAsync(PostOptions options)
    {
        if (string.IsNullOrEmpty(options.Text) && !options.Images.Any())
        {
            throw new Exception("Text or Images are required.");
        }

        var protocol = ProgramHelpers.GetProtocol(options);
        await ProgramHelpers.AuthAsync(protocol, options);
        var prompt = options.Text ?? string.Empty;
        var embed = await ProgramHelpers.GetEmbed(protocol, options);
        var result = await protocol.Repo.CreatePostAsync(prompt, null, embed);
        var post = ProgramHelpers.HandleResult(result);
        Console.WriteLine($"Post Created: {post.Uri}");
    }

    [Verb("record", HelpText = "Get record from Bluesky.")]
    internal class GetRecordOptions : BaseOptions
    {
    }

    [Verb("post", HelpText = "Post to Bluesky.")]
    internal class PostOptions : BaseOptions
    {
        [Option('t', "text", HelpText = "Text to be posted.")]
        public string? Text { get; set; }

        [Option('i', "images", HelpText = "Images to be uploaded and attached. Max of four.")]
        public IEnumerable<string>? Images { get; set; }

        [Option('a', "alt-text", HelpText = "Alt Text for Images, attached in order they are given.")]
        public IEnumerable<string>? AltText { get; set; }
    }

    internal class BaseOptions
    {
#if DEBUG
        [Option('u', "username", HelpText = $"Your username. If empty, it will be filled in by the BLUESKY_DEBUG_USERNAME environment variable.")]
#else
        [Option('u', "username", HelpText = $"Your username. If empty, it will be filled in by the BLUESKY_USERNAME environment variable.")]
#endif
        public string? Username { get; set; }

#if DEBUG
        [Option('p', "password", HelpText = "Your password. If empty, it will be filled in by the BLUESKY_DEBUG_PASSWORD environment variable.")]
#else
        [Option('p', "password", HelpText = "Your password. If empty, it will be filled in by the BLUESKY_PASSWORD environment variable.")]
#endif
        public string? Password { get; set; }

#if DEBUG
        [Option('h', "host", HelpText = "Your host. If empty, it will be filled in by the BLUESKY_DEBUG_HOST. If that's empty, defaults to bsky.social")]
#else
        [Option('h', "host", HelpText = "Your host. If empty, it will be filled in by the BLUESKY_HOST. If that's empty, defaults to bsky.social")]
#endif
        public string? Host { get; set; }
    }
}