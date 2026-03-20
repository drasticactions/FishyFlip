// <copyright file="JetStreamATWebSocketRecordEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

/// <summary>
/// JetStream AT WebSocket Record Event Args.
/// </summary>
public class JetStreamATWebSocketRecordEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JetStreamATWebSocketRecordEventArgs"/> class.
    /// </summary>
    /// <param name="record"><see cref="ATWebSocketRecord"/>.</param>
    /// <param name="json">JSON.</param>
    /// <param name="eventId">An ID that can be used to correlate this parsed record to its original <see cref="JetStreamRawMessageEventArgs"/>.</param>
    public JetStreamATWebSocketRecordEventArgs(ATWebSocketRecord record, string json, long eventId)
    {
        this.Record = record;
        this.Json = json;
        this.EventId = eventId;
    }

    /// <summary>
    /// Gets the AT WebSocket Record.
    /// </summary>
    public ATWebSocketRecord Record { get; }

    /// <summary>
    /// Gets the JSON representation of the AT WebSocket Record.
    /// </summary>
    public string Json { get; }

    /// <summary>
    /// Gets an ID that can be used to correlate this parsed record to its original <see cref="JetStreamRawMessageEventArgs"/>.
    /// </summary>
    public long EventId { get; }
}