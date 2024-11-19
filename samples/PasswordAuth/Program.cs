// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using ConsoleAppFramework;
using FishyFlip;
using Microsoft.Extensions.Logging.Debug;

var app = ConsoleApp.Create();
app.Add<AppCommands>();
app.Run(args);

/// <summary>
/// App Commands.
/// </summary>
#pragma warning disable SA1649 // File name should match first type name
public class AppCommands
#pragma warning restore SA1649 // File name should match first type name
{
    /// <summary>
    /// Authenticate with the ATProtocol using a password.
    /// </summary>
    /// <param name="identifier">The user's identifier.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    [Command("authenticate")]
    public async Task AuthenticateAsync([Argument] string identifier, [Argument] string password, CancellationToken cancellationToken = default)
    {
        var protocol = new ATProtocolBuilder()
            .WithLogger(new DebugLoggerProvider().CreateLogger("FishyFlip"))
            .Build();

        var session = await protocol.AuthenticateWithPasswordAsync(identifier, password, cancellationToken);
        if (session is null)
        {
            Console.WriteLine("Failed to authenticate.");
            return;
        }

        Console.WriteLine("Authenticated.");
        Console.WriteLine($"Session Did: {session.Did}");
        Console.WriteLine($"Session Email: {session.Email}");
        Console.WriteLine($"Session Handle: {session.Handle}");
        Console.WriteLine($"Session Token: {session.AccessJwt}");
    }
}