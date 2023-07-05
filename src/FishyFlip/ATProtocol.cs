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
                    if (!this.options.TrackSession)
                    {
                        return result;
                    }

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
                    if (!this.Options.TrackSession)
                    {
                        return result;
                    }

                    return result;
                },
                error => error!);
    }

    public async Task<Result<HandleResolution?>> ResolveHandleAsync(string identifier, CancellationToken cancellationToken)
    {
        var atIdentifier = new AtUri(identifier);
        if (atIdentifier is null)
        {
            throw new ArgumentException($"Cannot resolve identifier: {identifier}");
        }

        return await this.ResolveHandleAsync(atIdentifier.Identifier!, cancellationToken);
    }

    public async Task<Result<HandleResolution?>> ResolveHandleAsync(AtIdentifier identifier, CancellationToken cancellationToken)
    {
        if (identifier.IsDid)
        {
            throw new ArgumentException("Cannot resolve a DID, must be a handle.");
        }

        string url = $"{Constants.Urls.AtProtoIdentity.ResolveHandle}?handle={identifier}";
        return await this.client.Get<HandleResolution>(url, this.options.JsonSerializerOptions, cancellationToken);
    }

    public Task<Result<Profile?>> GetProfileAsync(CancellationToken cancellationToken)
    {
        var did = this.sessionManager?.Session?.Did;
        if (did?.Identifier is null)
        {
            // TODO: Return proper error.
            this.options.Logger?.LogError("GetProfileAsync: Missing Did for default user.");
            return Task.FromResult(new Result<Profile?>(new Error(0, new ErrorDetail("Null Reference Exception", "Missing Did for default user."))));
        }

        return this.GetProfileAsync(did.Identifier, cancellationToken);
    }

    public Task<Result<Profile?>> GetProfileAsync(AtIdentifier identifier, CancellationToken cancellationToken)
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

    private void UpdateBearerToken(Session session)
    {
        this.client
                .DefaultRequestHeaders
                .Authorization =
            new AuthenticationHeaderValue("Bearer", session.AccessJwt);
    }

    private void OnUserLoggedIn(Session session)
    {
        this.UpdateBearerToken(session);

        if (!this.Options.TrackSession)
        {
            return;
        }

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