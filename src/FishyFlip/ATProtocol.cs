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

    public Session? Session => this.sessionManager?.Session;

    public ATProtoAdmin Admin => new(this);

    public ATProtoIdentity Identity => new(this);

    public ATProtoSync Sync => new(this);

    public ATProtoRepo Repo => new(this);

    public BlueskyActor Actor => new(this);

    internal HttpClient Client => this.client;

    internal SessionManager SessionManager => this.sessionManager;

    public ATProtoLabel Label => new(this);

    public ATProtoModeration Moderation => new(this);

    public ATProtoDebug Debug => new(this);

    public BlueskyFeed Feed => new(this);

    public BlueskyGraph Graph => new(this);

    public BlueskyNotification Notification => new(this);

    public bool IsSubscriptionActive => this.webSocketProtocol.IsConnected;

    /// <summary>
    /// Start the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartSubscribeReposAsync(CancellationToken? token = default)
        => this.webSocketProtocol.ConnectAsync(Constants.Urls.ATProtoSync.SubscribeRepos, token);

    /// <summary>
    /// Start the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartSubscribeLabelsAsync(CancellationToken? token = default)
        => this.webSocketProtocol.ConnectAsync(Constants.Urls.ATProtoLabel.SubscribeLabels, token);

    /// <summary>
    /// Stops the ATProtocol Subscription session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StopSubscriptionAsync(CancellationToken? token = default)
    {
        if (this.IsSubscriptionActive)
        {
            return this.webSocketProtocol.CloseAsync(token: token);
        }

        return Task.CompletedTask;
    }

    public void Dispose()
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