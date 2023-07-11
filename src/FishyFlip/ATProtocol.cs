// <copyright file="ATProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace FishyFlip;

public sealed class ATProtocol : IDisposable
{
    private ATProtocolOptions options;
    private HttpClient client;
    private ATWebSocketProtocol webSocketProtocol;
    private bool disposedValue;

    public ATProtocol(ATProtocolOptions options)
    {
        this.options = options;
        this.client = options.HttpClient ?? throw new NullReferenceException(nameof(options.HttpClient));
        this.webSocketProtocol = new ATWebSocketProtocol(this);
    }

    public event EventHandler<SubscribedRepoEventArgs>? OnSubscribedRepoMessage;

    public ATProtocolOptions Options => this.options;

    internal HttpClient Client => this.client;

    /// <summary>
    /// Start the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartSubscribeReposAsync(CancellationToken? token = default)
        => this.webSocketProtocol.ConnectAsync(token);

    /// <summary>
    /// Stops the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StopSubscribeReposAsync(CancellationToken? token = default)
    {
        this.webSocketProtocol.CloseAsync(token: token).FireAndForgetSafeAsync();
        return Task.CompletedTask;
    }

    internal void OnSubscribedRepoMessageInternal(SubscribedRepoEventArgs e)
        => this.OnSubscribedRepoMessage?.Invoke(this, e);

    void IDisposable.Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.client.Dispose();
            }

            this.disposedValue = true;
        }
    }
}