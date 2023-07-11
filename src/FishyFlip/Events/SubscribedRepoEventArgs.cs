// <copyright file="SubscribedRepoEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

public class SubscribedRepoEventArgs : EventArgs
{
    public SubscribedRepoEventArgs(SubscribeRepoMessage message)
    {
        this.Message = message;
    }

    public SubscribeRepoMessage Message { get; }
}
