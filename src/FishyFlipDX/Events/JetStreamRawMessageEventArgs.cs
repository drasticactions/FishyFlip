// <copyright file="JetStreamRawMessageEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

/// <summary>
/// JetStream Raw Message Event Args.
/// </summary>
public class JetStreamRawMessageEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JetStreamRawMessageEventArgs"/> class.
    /// </summary>
    /// <param name="messageJson">Raw Message JSON.</param>
    public JetStreamRawMessageEventArgs(string messageJson)
    {
        this.MessageJson = messageJson;
    }

    /// <summary>
    /// Gets the Message JSON.
    /// </summary>
    public string MessageJson { get; }
}