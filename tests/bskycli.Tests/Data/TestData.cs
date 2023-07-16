// <copyright file="TestData.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using static ProgramCommands;

namespace bskycli.Tests.Data;

public static class TestData
{
    public static string Username => Environment.GetEnvironmentVariable("BLUESKY_DEBUG_USERNAME") ?? throw new Exception("Missing Username");

    public static string Host => Environment.GetEnvironmentVariable("BLUESKY_DEBUG_HOST") ?? throw new Exception("Missing Host");

    public static string Password => Environment.GetEnvironmentVariable("BLUESKY_DEBUG_PASSWORD") ?? throw new Exception("Missing Password");

    internal static void SetBaseOptions(BaseOptions options)
    {
        options.Username = Username;
        options.Host = Host;
        options.Password = Password;
    }
}
