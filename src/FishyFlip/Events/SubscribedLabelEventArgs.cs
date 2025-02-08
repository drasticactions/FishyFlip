// <copyright file="SubscribedLabelEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

/// <summary>
/// Subscribed Label Event Args.
/// Fires when a labeler sends an update.
/// </summary>
public class SubscribedLabelEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubscribedLabelEventArgs"/> class.
    /// </summary>
    /// <param name="message"><see cref="SubscribeLabelMessage"/>.</param>
    public SubscribedLabelEventArgs(SubscribeLabelMessage message)
    {
        this.Message = message;
    }

    /// <summary>
    /// Gets the repo message.
    /// </summary>
    public SubscribeLabelMessage Message { get; }
}
