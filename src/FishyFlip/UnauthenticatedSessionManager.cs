// <copyright file="UnauthenticatedSessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    private HttpClient client;
    private ILogger? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthenticatedSessionManager"/> class.
    /// </summary>
    /// <param name="options">ATProtocol Options.</param>
    public UnauthenticatedSessionManager(ATProtocolOptions options)
    {
        this.client = options.GenerateHttpClient();
        this.logger = options.Logger;
    }

    /// <inheritdoc/>
    public bool IsAuthenticated => false;

    /// <inheritdoc/>
    public Session? Session => null;

    /// <inheritdoc/>
    public HttpClient Client => this.client;

    /// <inheritdoc/>
    public Task RefreshSessionAsync()
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public void SetSession(Session session)
    {
        this.logger?.LogWarning("Unauthenticated session manager, session not set.");
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
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
