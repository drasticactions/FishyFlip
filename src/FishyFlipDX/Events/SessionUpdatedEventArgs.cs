// <copyright file="SessionUpdatedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events;

/// <summary>
/// Session Updated Event Args.
/// Fires when a session updates.
/// </summary>
public class SessionUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SessionUpdatedEventArgs"/> class.
    /// </summary>
    /// <param name="session"><see cref="Session"/>.</param>
    /// <param name="uri">The Instance Uri.</param>
    public SessionUpdatedEventArgs(AuthSession session, Uri? uri)
    {
        this.Session = session;
        this.InstanceUri = uri;
    }

    /// <summary>
    /// Gets the Session.
    /// </summary>
    public AuthSession Session { get; }

    /// <summary>
    /// Gets the Instance Uri.
    /// </summary>
    public Uri? InstanceUri { get; }
}
