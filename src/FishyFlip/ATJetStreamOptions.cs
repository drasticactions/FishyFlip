// <copyright file="ATJetStreamOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT JetStream Options.
/// </summary>
public class ATJetStreamOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATJetStreamOptions"/> class.
    /// </summary>
    public ATJetStreamOptions()
    {
        this.Url = new Uri("https://jetstream.atproto.tools");
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