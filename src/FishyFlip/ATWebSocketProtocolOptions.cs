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
    /// <param name="converters">Converters.</param>
    public ATWebSocketProtocolOptions(IReadOnlyList<ICustomATObjectCBORConverter>? converters = null)
    {
        this.Url = new Uri("https://bsky.network");
        this.CustomConverters = converters ?? new List<ICustomATObjectCBORConverter>();
    }

    /// <summary>
    /// Gets the instance Url.
    /// </summary>
    public Uri Url { get; internal set; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    public ILogger? Logger { get; internal set; }

    /// <summary>
    /// Gets the custom converters.
    /// </summary>
    public IReadOnlyList<ICustomATObjectCBORConverter> CustomConverters { get; internal set; }

    /// <summary>
    /// Gets the TaskFactory.
    /// </summary>
    public TaskFactory TaskFactory { get; internal set; } = new TaskFactory(TaskScheduler.Default);

    /// <summary>
    /// Gets the WebSocket client factory function.
    /// </summary>
    public Func<ILogger?, IWebSocketClient> WebSocketClientFactory { get; internal set; } = logger => new WebSocketClient(logger);
}
