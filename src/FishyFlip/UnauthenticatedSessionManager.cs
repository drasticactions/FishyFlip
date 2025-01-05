// <copyright file="UnauthenticatedSessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.Com.Atproto.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip;

/// <summary>
/// Unauthenticated Session Manager.
/// </summary>
internal class UnauthenticatedSessionManager : ISessionManager
{
    private bool disposedValue;
    private ATProtocol protocol;
    private ATProtocolHttpClient client;
    private ILogger? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthenticatedSessionManager"/> class.
    /// </summary>
    /// <param name="protocol">ATProtocol.</param>
    /// <param name="options">ATProtocol Options.</param>
    public UnauthenticatedSessionManager(ATProtocol protocol, ATProtocolOptions options)
    {
        this.protocol = protocol;
        this.client = options.GenerateHttpClient(this.protocol);
        this.logger = options.Logger;
    }

    /// <inheritdoc/>
    public event EventHandler<SessionUpdatedEventArgs>? SessionUpdated;

    /// <inheritdoc/>
    public bool IsAuthenticated => false;

    /// <inheritdoc/>
    public Session? Session => null;

    /// <inheritdoc/>
    public ATProtocolHttpClient Client => this.client;

    /// <inheritdoc/>
    public Task<Result<RefreshSessionOutput?>> RefreshSessionAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<Result<RefreshSessionOutput?>>(new RefreshSessionOutput());
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Set the session.
    /// </summary>
    /// <param name="session">Session.</param>
    internal void SetSession(Session session)
    {
        this.logger?.LogWarning("Unauthenticated session manager, session not set.");
        this.SessionUpdated?.Invoke(this, new SessionUpdatedEventArgs(new AuthSession(session), null));
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">Disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.client?.Dispose();
            }

            this.disposedValue = true;
        }
    }
}
