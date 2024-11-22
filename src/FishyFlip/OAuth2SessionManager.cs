﻿// <copyright file="OAuth2SessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.Com.Atproto.Repo;
using FishyFlip.Tools;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.DPoP;
using IdentityModel.OidcClient.Results;
using Microsoft.Extensions.Logging;

namespace FishyFlip;

/// <summary>
/// OAuth2 Session Manager.
/// </summary>
internal class OAuth2SessionManager : ISessionManager
{
    private bool disposedValue;
    private HttpClient client;
    private OidcClient? oidcClient;
    private AuthorizeState? state;
    private ILogger? logger;
    private ATProtocol protocol;
    private Session? session;
    private RefreshTokenDelegatingHandler? delegatingHandler;
    private string? proofKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2SessionManager"/> class.
    /// </summary>
    /// <param name="protocol">Protocol.</param>
    public OAuth2SessionManager(ATProtocol protocol)
    {
        this.protocol = protocol;
        this.client = this.protocol.Options.GenerateHttpClient();
        this.logger = this.protocol.Options.Logger;
    }

    /// <inheritdoc/>
    public event EventHandler<SessionUpdatedEventArgs>? SessionUpdated;

    /// <inheritdoc/>
    public HttpClient Client => this.client;

    /// <inheritdoc/>
    public bool IsAuthenticated => this.session != null;

    /// <inheritdoc/>
    public Session? Session => this.session;

    /// <summary>
    /// Gets the OAuth2 Session. If null, the session is not authenticated.
    /// </summary>
    public AuthSession? OAuthSession => this.session is null || this.proofKey is null ? null : new AuthSession(this.session, this.proofKey);

    /// <summary>
    /// Starts an existing OAuth2 Session.
    /// These should be the same parameters as used when creating the previous session.
    /// </summary>
    /// <param name="session">Previous Auth Session.</param>
    /// <param name="clientId">ClientID, must be a URL.</param>
    /// <param name="instanceUrl">InstanceUrl, must be a URL. If null, uses https://bsky.social.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <exception cref="OAuth2Exception">Thrown if missing OAuth2 information.</exception>
    /// <returns>Task.</returns>
    public Task<AuthSession> StartSessionAsync(AuthSession session, string clientId, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        instanceUrl ??= Constants.Urls.ATProtoServer.SocialApi;
        var options = new OidcClientOptions
        {
            Authority = instanceUrl,
            ClientId = clientId,
            LoadProfile = false,
        };

        this.proofKey = session.ProofKey;

        if (this.proofKey is null)
        {
            throw new OAuth2Exception("ProofKey is null. This must be set from the previous session.");
        }

        if (string.IsNullOrEmpty(session.Session.RefreshJwt))
        {
            throw new OAuth2Exception("RefreshJwt is null. This must be set from the previous session.");
        }

        options.ConfigureDPoP(this.proofKey);
        this.oidcClient = new OidcClient(options);
        this.oidcClient.Options.Policy.Discovery.DiscoveryDocumentPath = ".well-known/oauth-authorization-server";

        var handler = this.oidcClient.CreateDPoPHandler(this.proofKey, session.Session.RefreshJwt);

        this.delegatingHandler = (RefreshTokenDelegatingHandler)handler;
        this.delegatingHandler.TokenRefreshed += this.DelegatingHandler_TokenRefreshed;

        this.SetSession(session.Session);
        return Task.FromResult(session);
    }

    /// <summary>
    /// Starts an OAuth2 Session.
    /// Once called, the session state is kept in memory.
    /// If you call this method again and try using the outputted URL, it will fail.
    /// </summary>
    /// <param name="clientId">ClientID, must be a URL.</param>
    /// <param name="redirectUrl">RedirectUrl.</param>
    /// <param name="scopes">ATProtocol Scopes.</param>
    /// <param name="instanceUrl">InstanceUrl, must be a URL. If null, uses https://bsky.social.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Authorization URL to call.</returns>
    public async Task<string> StartAuthorizationAsync(string clientId, string redirectUrl, IEnumerable<string> scopes, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        instanceUrl ??= Constants.Urls.ATProtoServer.SocialApi;
        var options = new OidcClientOptions
        {
            Authority = instanceUrl,
            ClientId = clientId,
            Scope = string.Join(" ", scopes),
            RedirectUri = redirectUrl,
            LoadProfile = false,
        };
        this.proofKey = JsonWebKeys.CreateRsaJson();
        options.ConfigureDPoP(this.proofKey);

        this.oidcClient = new OidcClient(options);

        this.oidcClient.Options.Policy.Discovery.DiscoveryDocumentPath = ".well-known/oauth-authorization-server";
        this.state = await this.oidcClient.PrepareLoginAsync(cancellationToken: cancellationToken);
        if (this.state == null)
        {
            throw new OAuth2Exception("Failed to prepare login.");
        }

        if (this.state.IsError)
        {
            throw new OAuth2Exception(this.state.Error);
        }

        return this.state.StartUrl;
    }

    /// <summary>
    /// Handles the callback from the OAuth2 Server.
    /// </summary>
    /// <param name="data">Data URI from the callback.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    /// <exception cref="OAuth2Exception">Thrown if Login fails.</exception>
    public async Task<Session> CompleteAuthorizationAsync(string data, CancellationToken cancellationToken = default)
    {
        if (this.oidcClient is null)
        {
            throw new OAuth2Exception("Client is null. Call StartAuthorizationAsync first.");
        }

        if (this.state is null)
        {
            throw new OAuth2Exception("State is null. Call StartAuthorizationAsync first.");
        }

        if (string.IsNullOrEmpty(data))
        {
            throw new OAuth2Exception("Data is null or empty.");
        }

        var result = await this.oidcClient.ProcessResponseAsync(data, this.state);
        if (result.IsError)
        {
            throw new OAuth2Exception(result.Error);
        }

        var sub = result.TokenResponse!.Json!.Value!.TryGetValue("sub");
        var subValue = sub!.ToString();
        var didSub = ATDid.Create(subValue)!;
        var describeRepo = (await this.protocol.DescribeRepoAsync(didSub, cancellationToken)).HandleResult();
        var session = new Session(describeRepo!.Did!, describeRepo.DidDoc, describeRepo.Handle!, null, result.AccessToken, result.RefreshToken);
        this.delegatingHandler = (RefreshTokenDelegatingHandler)result.RefreshTokenHandler;
        this.delegatingHandler.TokenRefreshed += this.DelegatingHandler_TokenRefreshed;
        this.SetSession(session);

        return session;
    }

    /// <inheritdoc/>
    public Task RefreshSessionAsync(CancellationToken cancellationToken = default)
        => this.RefreshTokenAsync(cancellationToken);

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
        if (this.protocol.Options.UseServiceEndpointUponLogin)
        {
            var logger = this.protocol.Options.Logger;
            var serviceUrl = session.DidDoc?.Service?.FirstOrDefault()?.ServiceEndpoint;
            if (string.IsNullOrEmpty(serviceUrl))
            {
                logger?.LogWarning($"UseServiceEndpointUponLogin enabled, but session missing Service Endpoint.");
            }
            else
            {
                var result2 = Uri.TryCreate(serviceUrl, UriKind.Absolute, out Uri? uriResult);
                if (!result2 || uriResult is null)
                {
                    logger?.LogWarning($"UseServiceEndpointUponLogin enabled, but session missing Service Endpoint.");
                }
                else
                {
                    this.protocol.Options.Url = uriResult;
                    this.client.Dispose();
                    this.client = this.protocol.Options.GenerateHttpClient(this.delegatingHandler);
                    logger?.LogInformation($"UseServiceEndpointUponLogin enabled, switching to {uriResult}.");
                }
            }
        }

        this.session = session;
    }

    /// <summary>
    /// Refresh Token.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Token result.</returns>
    internal async Task<RefreshTokenResult?> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        if (this.oidcClient is null)
        {
            this.logger?.LogWarning("OdicClient is null.Start OAuth Session first.");
            return null;
        }

        var refreshResult = await this.oidcClient.RefreshTokenAsync(this.session!.RefreshJwt, backChannelParameters: null, scope: null, cancellationToken: cancellationToken);
        if (refreshResult.IsError)
        {
            throw new OAuth2Exception($"Failed to refresh token: {refreshResult.Error} {refreshResult.ErrorDescription}");
        }

        if (this.session is null)
        {
            throw new OAuth2Exception("Session should not be null if RefreshToken handler is enabled");
        }

        lock (this.session)
        {
            this.session = new Session(this.session.Did, this.session.DidDoc, this.session.Handle, null, refreshResult.AccessToken, refreshResult.RefreshToken);
        }

        return refreshResult;
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
                if (this.delegatingHandler is not null)
                {
                    this.delegatingHandler.TokenRefreshed -= this.DelegatingHandler_TokenRefreshed;
                }

                this.delegatingHandler?.Dispose();
            }

            this.disposedValue = true;
        }
    }

    private void DelegatingHandler_TokenRefreshed(object? sender, TokenRefreshedEventArgs e)
    {
        if (this.session is null)
        {
            throw new NullReferenceException("Session should not be null if RefreshToken handler is enabled");
        }

        if (string.IsNullOrEmpty(e.RefreshToken))
        {
            throw new OAuth2Exception("RefreshToken is null or empty.");
        }

        if (string.IsNullOrEmpty(e.AccessToken))
        {
            throw new OAuth2Exception("AccessToken is null or empty.");
        }

        lock (this.session)
        {
            this.session = new Session(this.session.Did, this.session.DidDoc, this.session.Handle, null, e.AccessToken, e.RefreshToken);
            this.SessionUpdated?.Invoke(this, new SessionUpdatedEventArgs(this.OAuthSession!, this.protocol.Options.Url));
        }
    }
}
