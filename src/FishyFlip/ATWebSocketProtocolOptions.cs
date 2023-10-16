// <copyright file="ATWebSocketProtocolOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>


namespace FishyFlip;

/// <summary>
/// AT WebSocket Protocol Options.
/// </summary>
public class ATWebSocketProtocolOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketProtocolOptions"/> class.
    /// </summary>
    public ATWebSocketProtocolOptions()
    {
        this.Url = new Uri("https://bsky.network");
    }

    /// <summary>
    /// Gets the instance Url.
    /// </summary>
    public Uri Url { get; internal set; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    public ILogger? Logger { get; internal set; }
}
