// <copyright file="SubscriptionConnectionStatusEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

/// <summary>
/// Represents the event arguments for the subscription connection status.
/// </summary>
public class SubscriptionConnectionStatusEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionConnectionStatusEventArgs"/> class.
    /// </summary>
    /// <param name="connected">The state of the WebSocket connection.</param>
    public SubscriptionConnectionStatusEventArgs(WebSocketState connected)
    {
        this.State = connected;
    }

    /// <summary>
    /// Gets the state of the WebSocket connection.
    /// </summary>
    public WebSocketState State { get; }
}