// <copyright file="ConsoleLog.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;

namespace OAuth;

/// <summary>
/// Console Log.
/// </summary>
public class ConsoleLog
{
    private ILogger logger;
    private bool verbose;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleLog"/> class.
    /// </summary>
    /// <param name="verbose">Verbose logging.</param>
    public ConsoleLog(bool verbose)
    {
        this.verbose = verbose;
        this.logger = LoggerFactory.Create(builder =>
        {
            if (verbose)
            {
                builder.AddSimpleConsole(options => { options.SingleLine = true; });
                builder.SetMinimumLevel(LogLevel.Debug);
            }
        }).CreateLogger("cli");
    }

    /// <summary>
    /// Log a message.
    /// </summary>
    /// <param name="message">Message to log.</param>
    public void Log(string message)
    {
        if (!this.verbose)
        {
            Console.WriteLine(message);
        }

        this.logger.LogInformation(message);
    }

    /// <summary>
    /// Log a error message.
    /// </summary>
    /// <param name="message">Message to log.</param>
    public void LogError(string message)
    {
        if (!this.verbose)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        this.logger.LogError(message);
    }

    /// <summary>
    /// Log a warning message.
    /// </summary>
    /// <param name="message">Message to log.</param>
    public void LogWarning(string message)
    {
        if (!this.verbose)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        this.logger.LogWarning(message);
    }

    /// <summary>
    /// Log a debug message.
    /// </summary>
    /// <param name="message">Message to log.</param>
    public void LogDebug(string message)
    {
        this.logger.LogDebug(message);
    }
}