// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommandLine;
using static ProgramCommands;

/// <summary>
/// Program Root.
/// </summary>
internal class Program
{
    private static async Task Main(string[] args)
        => await RunParser(args);

    internal static Task RunParser(string[] args)
    {
        return Parser.Default.ParseArguments<GetRecordOptions, PostOptions>(args)
        .MapResult(
          (GetRecordOptions options) => ProgramHelpers.AppDispatcher(ProgramCommands.GetRecordAsync(options)),
          (PostOptions options) => ProgramHelpers.AppDispatcher(ProgramCommands.PostAsync(options)),
          errs => Task.FromResult(-1));
    }

    internal static class Constants
    {
#if DEBUG
        public static string BlueskyUsername => "BLUESKY_DEBUG_USERNAME";

        public static string BlueskyPassword => "BLUESKY_DEBUG_PASSWORD";

        public static string BlueskyHost => "BLUESKY_DEBUG_HOST";
#else
        public static string BlueskyUsername => "BLUESKY_USERNAME";

        public static string BlueskyPassword => "BLUESKY_PASSWORD";

        public static string BlueskyHost => "BLUESKY_HOST";
#endif
    }
}