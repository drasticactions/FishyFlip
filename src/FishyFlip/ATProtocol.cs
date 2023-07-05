// <copyright file="ATProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.Http.Headers;
using System.Security.Cryptography;
using FishyFlip.Commands;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging;

namespace FishyFlip;

public sealed class ATProtocol : IAsyncDisposable, IDisposable
{
    private ATProtocolOptions options;
    private bool isDisposed;
    private HttpClient client;
    private SessionManager? sessionManager;

    public ATProtocol(ATProtocolOptions options)
    {
        this.options = options;
        this.client = options.HttpClient ?? throw new NullReferenceException(nameof(options.HttpClient));
    }

    internal HttpClient Client => this.client;

    public ATProtocolOptions Options => this.options;

    public async Task<Result<Session>> LoginAsync(Login command, CancellationToken cancellationToken)
    {
        Result<Session> result =
            await this.client.Post<Login, Session>(Constants.Urls.AtProtoServer.Login, this.options.JsonSerializerOptions, command, cancellationToken);

        return
            result
                .Match(
                s =>
                {
                    this.OnUserLoggedIn(s);
                    return result;
                },
                error => error!);
    }

    public async Task<Result<Session>> RefreshSessionAsync(
        Session session,
        CancellationToken cancellationToken)
    {
        this.client
            .DefaultRequestHeaders
            .Authorization = new AuthenticationHeaderValue("Bearer", session.RefreshJwt);

        var result = await this.client.Post<Session>(Constants.Urls.AtProtoServer.RefreshSession, this.options.JsonSerializerOptions, cancellationToken);
        return
            result
                .Match(
                s =>
                {
                    this.sessionManager?.SetSession(s);
                    return result;
                },
                error => error!);
    }

    public async Task<Result<HandleResolution?>> ResolveHandleAsync(AtHandler handler, CancellationToken cancellationToken)
    {
        string url = $"{Constants.Urls.AtProtoIdentity.ResolveHandle}?handle={handler}";
        return await this.client.Get<HandleResolution>(url, this.options.JsonSerializerOptions, cancellationToken);
    }

    public Task<Result<Profile?>> GetProfileAsync(AtDid identifier, CancellationToken cancellationToken)
    {
        string url = $"{Constants.Urls.Bluesky.GetActorProfile}?actor={identifier}";
        return this.client.Get<Profile>(url, this.options.JsonSerializerOptions, cancellationToken);
    }

    public Task<Result<Profile?>> GetProfileAsync(AtHandler identifier, CancellationToken cancellationToken)
    {
        string url = $"{Constants.Urls.Bluesky.GetActorProfile}?actor={identifier}";
        return this.client.Get<Profile>(url, this.options.JsonSerializerOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.isDisposed = true;
        this.client.Dispose();
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        this.Dispose();
    }

    private void OnUserLoggedIn(Session session)
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
}