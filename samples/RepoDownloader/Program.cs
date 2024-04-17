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
[CliCommand(Description = "repodownloader", ShortFormAutoGenerate = false)]
#pragma warning disable SA1649 // File name should match first type name
public class RootCommand
#pragma warning restore SA1649 // File name should match first type name
{
    /// <summary>
    /// Use Handle for fetching CAR.
    /// </summary>
    [CliCommand(Description = "handle", ShortFormAutoGenerate = false)]
    public class HandleCommand
    {
        /// <summary>
        /// Gets or sets the BSky Handle.
        /// </summary>
        [CliArgument(Description = "BSky Handle")]
        public required string Handle { get; set; }

        /// <summary>
        /// Gets or sets the Path.
        /// </summary>
        [CliOption(Description = "Repo CAR Download Path")]
        public string? Path { get; set; } = string.Empty;

        public async Task RunAsync()
        {
            var debugFactory = new DebugLoggerProvider();
            var logger = debugFactory.CreateLogger("ATProtocolBuilder");

            var protocolBuilder = new ATProtocolBuilder().WithLogger(logger);
            using var protocol = protocolBuilder.Build();

            // First, create an ATIdentifier from the handle.
            var handle = ATIdentifier.Create(this.Handle);

            // If it's invalid then print an error message.
            if (handle == null)
            {
                Console.WriteLine("Invalid handle");
                return;
            }

            var path = this.Path;
            if (string.IsNullOrEmpty(path))
            {
                path = $"{handle}.car";
            }

            var filename = System.IO.Path.GetFileName(path);
            if (string.IsNullOrEmpty(filename))
            {
                filename = $"{handle}.car";
            }

            var directory = System.IO.Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            else
            {
                directory = System.IO.Directory.GetCurrentDirectory();
            }

            // Fetch the repo.
            await protocol.Sync.DownloadRepoAsync(handle, directory, filename);
            Console.WriteLine($"Wrote {filename} to {directory}");
        }
    }

    /// <summary>
    /// Use Did for fetching CAR.
    /// </summary>
    [CliCommand(Description = "did", ShortFormAutoGenerate = false)]
    public class DidCommand
    {
        /// <summary>
        /// Gets or sets the BSky Did.
        /// </summary>
        [CliArgument(Description = "BSky Did")]
        public required string Did { get; set; }

        /// <summary>
        /// Gets or sets the Path.
        /// </summary>
        [CliOption(Description = "Repo CAR Download Path")]
        public string? Path { get; set; } = string.Empty;

        public async Task RunAsync()
        {
            var debugFactory = new DebugLoggerProvider();
            var logger = debugFactory.CreateLogger("ATProtocolBuilder");

            var protocolBuilder = new ATProtocolBuilder().WithLogger(logger);
            using var protocol = protocolBuilder.Build();

            // First, create an ATIdentifier from the Did.
            var did = ATIdentifier.Create(this.Did);

            // If it's invalid then print an error message.
            if (did == null)
            {
                Console.WriteLine("Invalid handle");
                return;
            }

            var path = this.Path;
            if (string.IsNullOrEmpty(path))
            {
                path = $"{did}.car";
            }

            var filename = System.IO.Path.GetFileName(path);
            if (string.IsNullOrEmpty(filename))
            {
                filename = $"{did}.car";
            }

            var directory = System.IO.Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            else
            {
                directory = System.IO.Directory.GetCurrentDirectory();
            }

            // Fetch the repo.
            await protocol.Sync.DownloadRepoAsync(did, directory, filename);
            Console.WriteLine($"Wrote {filename} to {directory}");
        }
    }
}
