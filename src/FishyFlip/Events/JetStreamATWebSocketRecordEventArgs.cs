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
    public JetStreamATWebSocketRecordEventArgs(ATWebSocketRecord record)
    {
        this.Record = record;
    }

    /// <summary>
    /// Gets the AT WebSocket Record.
    /// </summary>
    public ATWebSocketRecord Record { get; }
}