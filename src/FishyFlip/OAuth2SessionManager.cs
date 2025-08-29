// <copyright file="OAuth2SessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Web;
using FishyFlip.Lexicon.Com.Atproto.Repo;
using FishyFlip.Lexicon.Com.Atproto.Server;
using FishyFlip.OAuth;
using FishyFlip.OAuth.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging;

namespace FishyFlip;

/// <summary>
/// OAuth2 Session Manager.
/// </summary>
public class OAuth2SessionManager : ISessionManager
{
    private bool disposedValue;
    private HttpClient client;
    private OAuthClient? oauthClient;
    private RefreshTokenHandler? refreshTokenHandler;
    private ILogger? logger;
    private ATProtocol protocol;
    private Session? session;
    private string? proofKey;
    private string? clientId;

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2SessionManager"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    public OAuth2SessionManager(ILogger? logger = null)
        : this(new ATProtocol(new ATProtocolOptions { Logger = logger }))
    {
        this.protocol.SessionManager = this;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2SessionManager"/> class.
    /// </summary>
    /// <param name="protocol">Protocol.</param>
    public OAuth2SessionManager(ATProtocol protocol)
    {
        this.protocol = protocol;
        this.client = this.protocol.Options.GenerateHttpClient(this.protocol);
        this.logger = this.protocol.Options.Logger;
        this.oauthClient = new OAuthClient(this.logger);
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
    /// Gets the ATProtocol instance.
    /// </summary>
    public ATProtocol Protocol => this.protocol;

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
    public async Task<Result<AuthSession?>> StartSessionAsync(AuthSession session, string clientId, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        instanceUrl ??= Constants.Urls.ATProtoServer.SocialApi;

        this.proofKey = session.ProofKey;
        this.clientId = clientId;

        if (this.proofKey is null)
        {
            return new ATError(new OAuth2Exception("ProofKey is null. This must be set from the previous session."));
        }

        if (string.IsNullOrEmpty(session.Session.RefreshJwt))
        {
            return new ATError(new OAuth2Exception("RefreshJwt is null. This must be set from the previous session."));
        }

        // Setup OAuth client with existing session
        this.oauthClient = new OAuthClient(this.logger);
        this.oauthClient.SetupFromSession(this.proofKey, instanceUrl);

        // Discover endpoints
        var config = await this.oauthClient.DiscoverEndpointsAsync(instanceUrl, cancellationToken);
        if (config == null)
        {
            return new ATError(new OAuth2Exception("Failed to discover OAuth endpoints."));
        }

#if NET5_0_OR_GREATER
        var tokenEndpoint = config.GetValueOrDefault("token_endpoint")?.ToString();
#else
        var tokenEndpoint = config.TryGetValue("token_endpoint", out var tokenEp) ? tokenEp?.ToString() : null;
#endif
        if (string.IsNullOrEmpty(tokenEndpoint))
        {
            return new ATError(new OAuth2Exception("Missing token endpoint."));
        }

        // Create refresh token handler
        var keyPair = this.oauthClient.KeyPair;
        if (keyPair == null)
        {
            return new ATError(new OAuth2Exception("Failed to setup DPoP key pair."));
        }

        var innerHandler = new HttpClientHandler();
        this.refreshTokenHandler = new RefreshTokenHandler(
            this.oauthClient,
            clientId,
            session.Session.AccessJwt!,
            session.Session.RefreshJwt,
            3600, // Default expiry
            keyPair,
            innerHandler,
            this.logger);

        this.refreshTokenHandler.TokenRefreshed += this.RefreshTokenHandler_TokenRefreshed;

        this.SetSession(session.Session);
        return session;
    }

    /// <summary>
    /// Starts an OAuth2 Session.
    /// Once called, the session state is kept in memory.
    /// If you call this method again and try using the outputted URL, it will fail.
    /// </summary>
    /// <param name="clientId">ClientID, must be a URL.</param>
    /// <param name="redirectUrl">RedirectUrl.</param>
    /// <param name="scopes">ATProtocol Scopes.</param>
    /// <param name="loginHint">LoginHint.</param>
    /// <param name="instanceUrl">InstanceUrl, must be a URL. If null, uses https://bsky.social.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Authorization URL to call.</returns>
    public async Task<Result<string?>> StartAuthorizationAsync(string clientId, string redirectUrl, IEnumerable<string> scopes, string? loginHint = default, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        instanceUrl ??= Constants.Urls.ATProtoServer.SocialApi;
        this.clientId = clientId;

        this.oauthClient = new OAuthClient(this.logger);

        var authUrl = await this.oauthClient.PrepareAuthorizationAsync(
            clientId,
            redirectUrl,
            string.Join(" ", scopes),
            instanceUrl,
            loginHint,
            cancellationToken);

        if (string.IsNullOrEmpty(authUrl))
        {
            return new ATError(new OAuth2Exception("Failed to prepare authorization."));
        }

        // Store the proof key for later use
        var keyPair = this.oauthClient.KeyPair;
        if (keyPair != null)
        {
            this.proofKey = keyPair.JwkJson ?? string.Empty;
        }

        return authUrl;
    }

    /// <summary>
    /// Handles the callback from the OAuth2 Server.
    /// </summary>
    /// <param name="data">Data URI from the callback.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    /// <exception cref="OAuth2Exception">Thrown if Login fails.</exception>
    public async Task<Result<Session?>> CompleteAuthorizationAsync(string data, CancellationToken cancellationToken = default)
    {
        if (this.oauthClient is null)
        {
            return new ATError(new OAuth2Exception("Client is null. Call StartAuthorizationAsync first."));
        }

        if (string.IsNullOrEmpty(data))
        {
            return new ATError(new OAuth2Exception("Data is null or empty."));
        }

        // Parse the callback URL to extract code and state
        // Handle both full URI and raw query string
        Uri? uri;
        string queryString;

        if (Uri.TryCreate(data, UriKind.Absolute, out uri))
        {
            queryString = uri.Query;
        }
        else
        {
            // Assume data is a raw query string (possibly starting with '?')
            queryString = data.StartsWith("?") ? data : "?" + data;
            uri = new Uri("http://127.0.0.1/" + queryString);
        }

        var query = HttpUtility.ParseQueryString(queryString);
        var code = query["code"];
        var state = query["state"];

        if (string.IsNullOrEmpty(code))
        {
            var errorCode = query["error"];
            var errorDescription = query["error_description"];
            return new ATError(new OAuth2Exception($"Authorization failed: {errorCode} - {errorDescription}"));
        }

        // Verify state matches
        if (this.oauthClient.State?.State != state)
        {
            return new ATError(new OAuth2Exception("State mismatch. Possible CSRF attack."));
        }

        // Get the redirect URI from the original request
        var redirectUri = uri.GetLeftPart(UriPartial.Path);

        // Exchange code for tokens
        var tokenResponse = await this.oauthClient.ExchangeCodeForTokensAsync(
            code,
            this.clientId ?? string.Empty,
            redirectUri,
            cancellationToken);

        if (tokenResponse == null)
        {
            return new ATError(new OAuth2Exception("Failed to exchange code for tokens."));
        }

        // Extract DID from token
        var sub = tokenResponse.Sub ?? OAuthHelpers.ExtractSubFromJwt(tokenResponse.AccessToken!);
        if (string.IsNullOrEmpty(sub))
        {
            return new ATError(new OAuth2Exception("Failed to extract subject from token."));
        }

        var didSub = ATDid.Create(sub!)!;
        (var describeRepo, var error) = await this.protocol.DescribeRepoAsync(didSub, cancellationToken);
        if (error is not null)
        {
            return error;
        }

        var expiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
        var session = new Session(
            describeRepo!.Did!,
            describeRepo.DidDoc,
            describeRepo.Handle!,
            null,
            tokenResponse.AccessToken ?? string.Empty,
            tokenResponse.RefreshToken ?? string.Empty,
            expiresAt);

        // Setup refresh token handler
        var keyPair = this.oauthClient.KeyPair;
        if (keyPair != null && !string.IsNullOrEmpty(tokenResponse.RefreshToken))
        {
            var innerHandler = new HttpClientHandler();
            this.refreshTokenHandler = new RefreshTokenHandler(
                this.oauthClient,
                this.clientId ?? string.Empty,
                tokenResponse.AccessToken!,
                tokenResponse.RefreshToken,
                tokenResponse.ExpiresIn,
                keyPair,
                innerHandler,
                this.logger);

            this.refreshTokenHandler.TokenRefreshed += this.RefreshTokenHandler_TokenRefreshed;
        }

        this.SetSession(session);

        return session;
    }

    /// <inheritdoc/>
    public async Task<Result<RefreshSessionOutput?>> RefreshSessionAsync(CancellationToken cancellationToken = default)
    {
        if (this.session is null)
        {
            throw new OAuth2Exception("Session is null.");
        }

        if (this.oauthClient is null)
        {
            throw new OAuth2Exception("OAuth client is null.");
        }

        if (string.IsNullOrEmpty(this.session.RefreshJwt))
        {
            throw new OAuth2Exception("Refresh token is null.");
        }

        var tokenResponse = await this.oauthClient.RefreshTokenAsync(
            this.session.RefreshJwt,
            this.clientId ?? string.Empty,
            cancellationToken);

        if (tokenResponse == null)
        {
            return new ATError(401, new ErrorDetail("OAuth Error", "Failed to refresh token"));
        }

        var refreshSessionOutput = new RefreshSessionOutput();
        refreshSessionOutput.Active = true;
        refreshSessionOutput.AccessJwt = tokenResponse.AccessToken ?? string.Empty;
        refreshSessionOutput.RefreshJwt = tokenResponse.RefreshToken ?? this.session.RefreshJwt;
        refreshSessionOutput.Did = this.session.Did;
        refreshSessionOutput.DidDoc = this.session.DidDoc;

        // Update session with new tokens
        var expiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
        this.session = new Session(
            this.session.Did,
            this.session.DidDoc,
            this.session.Handle,
            null,
            tokenResponse.AccessToken ?? string.Empty,
            tokenResponse.RefreshToken ?? this.session.RefreshJwt,
            expiresAt);

        return refreshSessionOutput;
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
                    this.client = this.protocol.Options.GenerateHttpClient(this.protocol, this.refreshTokenHandler);
                    logger?.LogInformation($"UseServiceEndpointUponLogin enabled, switching to {uriResult}.");
                }
            }
        }

        this.session = session;
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
                if (this.refreshTokenHandler is not null)
                {
                    this.refreshTokenHandler.TokenRefreshed -= this.RefreshTokenHandler_TokenRefreshed;
                    this.refreshTokenHandler.Dispose();
                }
            }

            this.disposedValue = true;
        }
    }

    private void RefreshTokenHandler_TokenRefreshed(object? sender, TokenRefreshedEventArgs e)
    {
        if (this.session is null)
        {
            throw new NullReferenceException("Session should not be null if RefreshToken handler is enabled");
        }

        if (string.IsNullOrEmpty(e.RefreshToken) && string.IsNullOrEmpty(this.session.RefreshJwt))
        {
            throw new OAuth2Exception("RefreshToken is null or empty.");
        }

        if (string.IsNullOrEmpty(e.AccessToken))
        {
            throw new OAuth2Exception("AccessToken is null or empty.");
        }

        lock (this.session)
        {
            var expiresIn = e.ExpiresIn;
            var expiresInDatetimeFromNow = DateTime.UtcNow.AddSeconds(expiresIn);
            this.session = new Session(
                this.session.Did,
                this.session.DidDoc,
                this.session.Handle,
                null,
                e.AccessToken,
                e.RefreshToken ?? this.session.RefreshJwt,
                expiresInDatetimeFromNow);
            this.SessionUpdated?.Invoke(this, new SessionUpdatedEventArgs(this.OAuthSession!, this.protocol.Options.Url));
        }
    }
}
