// <copyright file="ATProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtocol : IAsyncDisposable, IDisposable
{
    private ATProtocolOptions options;
    private bool isDisposed;

    public ATProtocol(ATProtocolOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.isDisposed = true;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        this.Dispose();
    }
}