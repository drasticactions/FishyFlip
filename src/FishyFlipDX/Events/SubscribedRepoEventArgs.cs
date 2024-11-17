// <copyright file="SubscribedRepoEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

/// <summary>
/// Subscribed Repo Event Args.
/// Fires when a repo sends an update.
/// </summary>
public class SubscribedRepoEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubscribedRepoEventArgs"/> class.
    /// </summary>
    /// <param name="message"><see cref="SubscribeRepoMessage"/>.</param>
    public SubscribedRepoEventArgs(SubscribeRepoMessage message)
    {
        this.Message = message;
    }

    /// <summary>
    /// Gets the repo message.
    /// </summary>
    public SubscribeRepoMessage Message { get; }
}
