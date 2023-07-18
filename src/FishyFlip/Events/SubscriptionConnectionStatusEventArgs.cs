// <copyright file="SubscriptionConnectionStatusEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

public class SubscriptionConnectionStatusEventArgs : EventArgs
{
    public SubscriptionConnectionStatusEventArgs(WebSocketState connected)
    {
        this.State = connected;
    }

    public WebSocketState State { get; }
}
