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
    /// <param name="eventId">A unique, sequentially increasing ID that can be used to correlate this raw message to its future corresponding <see cref="JetStreamATWebSocketRecordEventArgs"/>.</param>
    public JetStreamRawMessageEventArgs(string messageJson, long eventId)
    {
        this.MessageJson = messageJson;
        this.EventId = eventId;
    }

    /// <summary>
    /// Gets the Message JSON.
    /// </summary>
    public string MessageJson { get; }

    /// <summary>
    /// Gets the unique, sequentially increasing ID that can be used to correlate this raw message to its future corresponding <see cref="JetStreamATWebSocketRecordEventArgs">.
    /// </summary>
    public long EventId { get; }
}