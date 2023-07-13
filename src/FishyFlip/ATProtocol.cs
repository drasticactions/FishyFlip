// <copyright file="ATProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtocol : IDisposable
{
    private ATProtocolOptions options;
    private HttpClient client;
    private ATWebSocketProtocol webSocketProtocol;
    private bool disposedValue;
    private SessionManager sessionManager;

    public ATProtocol(ATProtocolOptions options)
    {
        this.options = options;
        this.client = options.HttpClient ?? throw new NullReferenceException(nameof(options.HttpClient));
        this.webSocketProtocol = new ATWebSocketProtocol(this);
        this.sessionManager = new SessionManager(this);
    }

    public event EventHandler<SubscribedRepoEventArgs>? OnSubscribedRepoMessage;

    public ATProtocolOptions Options => this.options;

    public ATProtoServer Server => new(this);

    internal HttpClient Client => this.client;

    internal SessionManager SessionManager => this.sessionManager;

    public Session? Session => this.sessionManager?.Session;

    public ATProtoAdmin Admin => new(this);

    public ATProtoSync Sync => new(this);

    public ATProtoRepo Repo => new(this);

    public BlueskyActor Actor => new(this);

    public ATProtoLabel Label => new(this);

    public ATProtoModeration Moderation => new(this);

    public ATProtoDebug Debug => new(this);

    public BlueskyFeed Feed => new(this);

    public BlueskyGraph Graph => new(this);

    public BlueskyNotification Notification => new(this);

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

    void IDisposable.Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    internal void OnSubscribedRepoMessageInternal(SubscribedRepoEventArgs e)
        => this.OnSubscribedRepoMessage?.Invoke(this, e);

    internal void OnUserLoggedIn(Session session)
    {
        if (this.sessionManager is null)
        {
            this.sessionManager = new SessionManager(this, session);
        }
        else
        {
            this.sessionManager.SetSession(session);
        }
    }

    internal void SetSession(Session session)
        => this.sessionManager?.SetSession(session);

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