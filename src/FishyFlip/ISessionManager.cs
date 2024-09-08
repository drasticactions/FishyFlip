// <copyright file="ISessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip;

/// <summary>
/// Manage ATProtocol sessions.
/// </summary>
internal interface ISessionManager : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the session is authenticated.
    /// </summary>
    public bool IsAuthenticated { get; }

    /// <summary>
    /// Gets the current session.
    /// </summary>
    public Session? Session { get; }

    /// <summary>
    /// Refresh the given session.
    /// </summary>
    /// <returns>Task.</returns>
    public Task RefreshSessionAsync();

    /// <summary>
    /// Set the current session.
    /// </summary>
    /// <param name="session">Session.</param>
    public void SetSession(Session session);
}
