// <copyright file="ISessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.Com.Atproto.Server;
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
    /// Fires when a session is updated.
    /// </summary>
    public event EventHandler<SessionUpdatedEventArgs>? SessionUpdated;

    /// <summary>
    /// Gets the HttpClient used to make calls into ATProtocol.
    /// </summary>
    public ATProtocolHttpClient Client { get; }

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
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    public Task<Result<RefreshSessionOutput?>> RefreshSessionAsync(CancellationToken cancellationToken = default);
}
