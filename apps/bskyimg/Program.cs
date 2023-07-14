// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommandLine;

internal class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);
    }

    static void RunOptions(Options options)
    {
        var username = options.Username ?? System.Environment.GetEnvironmentVariable("BLUESKY_USERNAME");
        var password = options.Password ?? System.Environment.GetEnvironmentVariable("BLUESKY_PASSWORD");

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("You must provide a username and password.");
            return;
        }

        var host = options.Host ?? "bsky.social";

        foreach (string file in options.Images)
        {
            Console.WriteLine($"Processing file: {file}");
        }
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