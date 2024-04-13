// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotMake.CommandLine;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using PeterO.Cbor;
using System.Reflection;

await Cli.RunAsync<RootCommand>(args);

/// <summary>
/// Root CLI command.
/// </summary>
[CliCommand(Description = "livecardecoder", ShortFormAutoGenerate = false)]
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
        /// Gets or sets the Instance.
        /// </summary>
        [CliArgument(Description = "BSky Instance")]
        public string? Instance { get; set; } = "https://bsky.social";

        public async Task RunAsync()
        {
            if (Uri.TryCreate(this.Instance, UriKind.Absolute, out var uri) == false)
            {
                Console.WriteLine("Invalid instance URL");
                return;
            }

            var protocolBuilder = new ATProtocolBuilder().WithInstanceUrl(uri);
            var protocol = protocolBuilder.Build();

            // First, create an ATIdentifier from the handle.
            var handle = ATIdentifier.Create(this.Handle);

            // If it's invalid then print an error message.
            if (handle == null)
            {
                Console.WriteLine("Invalid handle");
                return;
            }

            // Fetch the repo. Decode the CAR as we download.
            await protocol.Sync.GetRepoAsync(handle, this.HandleProgressStatus);
        }

        /// <summary>
        /// Handle the progress of the CAR download.
        /// </summary>
        /// <param name="e"><see cref="CarProgressStatusEvent"/>.</param>
        private void HandleProgressStatus(CarProgressStatusEvent e)
        {
            var cid = e.Cid;
            var bytes = e.Bytes;
            var test = CBORObject.DecodeFromBytes(bytes);
            var record = ATRecord.FromCBORObject(test);

            // Prints the type of the record.
            if (!string.IsNullOrEmpty(record?.Type))
            {
                Console.WriteLine(record.Type);
            }
        }
    }
}