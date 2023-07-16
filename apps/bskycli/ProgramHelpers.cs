// <copyright file="ProgramHelpers.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Reflection;
using bskycli;
using FishyFlip;
using FishyFlip.Models;
using static ProgramCommands;

internal static class ProgramHelpers
{

    public static StreamContent StreamContentFromFilePath(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found.", filePath);
        }

        var stream = File.OpenRead(filePath);
        var content = new StreamContent(stream);
        content.Headers.ContentLength = stream.Length;
        content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(filePath));
        return content;
    }

    internal static async Task AppDispatcher(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    internal static async Task<Session> AuthAsync(ATProtocol protocol, BaseOptions options)
    {
        if (string.IsNullOrEmpty(options.Username))
        {
            options.Username = Environment.GetEnvironmentVariable(Program.Constants.BlueskyUsername);
        }

        if (string.IsNullOrEmpty(options.Password))
        {
            options.Password = Environment.GetEnvironmentVariable(Program.Constants.BlueskyPassword);
        }

        if (string.IsNullOrEmpty(options.Username))
        {
            throw new Exception("Username is required.");
        }

        if (string.IsNullOrEmpty(options.Password))
        {
            throw new Exception("Password is required.");
        }

        var result = await protocol.Server.CreateSessionAsync(options.Username, options.Password);
        var session = HandleResult(result);
        Console.WriteLine($"Session created for {session.Handle}.");
        return session;
    }

    internal static string GetAssemblyVersion()
    {
        // Get the current assembly
        Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

        // Get the assembly version
        var version = assembly.GetName().Version;

        return version?.ToString() ?? "0.0.0";
    }

    internal static async Task<Embed?> GetEmbed(ATProtocol atProtocol, PostOptions options)
    {
        if (options.Images?.Any() ?? false)
        {
            var imageList = new List<Image>();
            foreach (string file in options.Images)
            {
                StreamContent content;
                try
                {
                    content = StreamContentFromFilePath(file);
                }
                catch (FileNotFoundException e)
                {
                    throw new Exception($"File not found: {e.FileName}");
                }

                var imageResult = await atProtocol.Repo.UploadBlobAsync(content);
                var image = HandleResult(imageResult);
                imageList.Add(image.Blob.ToImage());
            }

            var imageEmbedList = new List<ImageEmbed>();
            var altImages = options.AltText?.ToArray() ?? Array.Empty<string>();
            for (var i = 0; i < imageList.Count; i++)
            {
                var img = imageList[i];
                var altText = altImages[i] ?? string.Empty;
                imageEmbedList.Add(new ImageEmbed(img, altText));
            }

            return new ImagesEmbed(imageEmbedList.ToArray());
        }

        return null;
    }

    internal static Uri? GetHost(BaseOptions options)
    {
        if (string.IsNullOrEmpty(options.Host))
        {
            options.Host = Environment.GetEnvironmentVariable(Program.Constants.BlueskyHost);
        }

        var host = options.Host ?? "bsky.social";

        if (!Uri.TryCreate(host, UriKind.Absolute, out Uri? uri))
        {
            uri = new Uri($"https://{host}");
        }

        return uri;
    }

    internal static ATProtocol GetProtocol(BaseOptions options)
    {
        var uri = GetHost(options);
        if (uri == null)
        {
            throw new Exception("Host is required.");
        }

        var builder = new ATProtocolBuilder();

        var protocol = builder
            .WithInstanceUrl(uri)
            .WithUserAgent($"bskycli/{GetAssemblyVersion()}")
            .Build();

        return protocol;
    }

    internal static T HandleResult<T>(Result<T> result)
    {
        if (result.IsT1)
        {
            throw new Exception($"Network Error: {result.AsT1.StatusCode} {result.AsT1.Detail}");
        }

        return result.AsT0;
    }
}