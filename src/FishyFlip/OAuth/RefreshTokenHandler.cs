// <copyright file="RefreshTokenHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FishyFlip.OAuth.Models;
using Microsoft.Extensions.Logging;

namespace FishyFlip.OAuth;

/// <summary>
/// HTTP message handler that automatically refreshes OAuth tokens.
/// </summary>
internal class RefreshTokenHandler : DelegatingHandler
{
    private readonly OAuthClient oauthClient;
    private readonly string clientId;
    private readonly ILogger? logger;
    private readonly SemaphoreSlim refreshLock = new(1, 1);

    private string? accessToken;
    private string? refreshToken;
    private DateTime? tokenExpiry;
    private DPoPKeyPair? keyPair;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenHandler"/> class.
    /// </summary>
    /// <param name="oauthClient">The OAuth client.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="accessToken">Initial access token.</param>
    /// <param name="refreshToken">Initial refresh token.</param>
    /// <param name="expiresIn">Token expiration in seconds.</param>
    /// <param name="keyPair">DPoP key pair.</param>
    /// <param name="innerHandler">Inner HTTP handler.</param>
    /// <param name="logger">Optional logger.</param>
    public RefreshTokenHandler(
        OAuthClient oauthClient,
        string clientId,
        string accessToken,
        string? refreshToken,
        int expiresIn,
        DPoPKeyPair keyPair,
        HttpMessageHandler innerHandler,
        ILogger? logger = null)
        : base(innerHandler)
    {
        this.oauthClient = oauthClient;
        this.clientId = clientId;
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
        this.keyPair = keyPair;
        this.logger = logger;
        this.tokenExpiry = DateTime.UtcNow.AddSeconds(expiresIn - 60); // Refresh 60 seconds before expiry
    }

    /// <summary>
    /// Event raised when a token is refreshed.
    /// </summary>
    public event EventHandler<TokenRefreshedEventArgs>? TokenRefreshed;

    /// <summary>
    /// Gets the current access token.
    /// </summary>
    public string? AccessToken => this.accessToken;

    /// <summary>
    /// Gets the current refresh token.
    /// </summary>
    public string? RefreshToken => this.refreshToken;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.refreshLock?.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Check if token needs refresh
        if (this.ShouldRefreshToken())
        {
            await this.RefreshTokenAsync(cancellationToken);
        }

        // Add authorization header with DPoP
        if (!string.IsNullOrEmpty(this.accessToken) && this.keyPair != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("DPoP", this.accessToken);

            // Add DPoP proof
            var dpopProof = DPoPHandler.GenerateDPoPProof(
                this.keyPair,
                request.Method.ToString(),
                request.RequestUri?.ToString() ?? string.Empty,
                this.oauthClient.State?.Nonce,
                DPoPHandler.CreateAccessTokenHash(this.accessToken!));

            request.Headers.Add("DPoP", dpopProof);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Check for DPoP nonce
        if (response.Headers.TryGetValues("DPoP-Nonce", out var nonceValues))
        {
            var state = this.oauthClient.State;
            if (state != null)
            {
                state.Nonce = nonceValues.FirstOrDefault();
            }
        }

        // If we get a 401, try refreshing the token and retry once
        if (response.StatusCode == HttpStatusCode.Unauthorized && !string.IsNullOrEmpty(this.refreshToken))
        {
            this.logger?.LogInformation("Received 401, attempting token refresh");

            var refreshed = await this.RefreshTokenAsync(cancellationToken);
            if (refreshed)
            {
                // Clone the request and retry
                var retryRequest = await this.CloneRequestAsync(request);

                // Update authorization header with new token
                if (!string.IsNullOrEmpty(this.accessToken) && this.keyPair != null)
                {
                    retryRequest.Headers.Authorization = new AuthenticationHeaderValue("DPoP", this.accessToken);

                    // Add new DPoP proof
                    var dpopProof = DPoPHandler.GenerateDPoPProof(
                        this.keyPair,
                        retryRequest.Method.ToString(),
                        retryRequest.RequestUri?.ToString() ?? string.Empty,
                        this.oauthClient.State?.Nonce,
                        DPoPHandler.CreateAccessTokenHash(this.accessToken!));

                    retryRequest.Headers.Remove("DPoP");
                    retryRequest.Headers.Add("DPoP", dpopProof);
                }

                response = await base.SendAsync(retryRequest, cancellationToken);
            }
        }

        return response;
    }

    private bool ShouldRefreshToken()
    {
        if (string.IsNullOrEmpty(this.refreshToken))
        {
            return false;
        }

        if (this.tokenExpiry == null)
        {
            return false;
        }

        return DateTime.UtcNow >= this.tokenExpiry;
    }

    private async Task<bool> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(this.refreshToken))
        {
            return false;
        }

        await this.refreshLock.WaitAsync(cancellationToken);
        try
        {
            // Double-check after acquiring lock
            if (!this.ShouldRefreshToken())
            {
                return true;
            }

            this.logger?.LogInformation("Refreshing OAuth token");

            var tokenResponse = await this.oauthClient.RefreshTokenAsync(
                this.refreshToken!,
                this.clientId,
                cancellationToken);

            if (tokenResponse == null)
            {
                this.logger?.LogError("Failed to refresh token");
                return false;
            }

            this.accessToken = tokenResponse.AccessToken;
            if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
            {
                this.refreshToken = tokenResponse.RefreshToken;
            }

            this.tokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 60);

            // Raise event
            this.TokenRefreshed?.Invoke(
                this,
                new TokenRefreshedEventArgs(
                    this.accessToken!,
                    this.refreshToken,
                    tokenResponse.ExpiresIn));

            this.logger?.LogInformation("Token refreshed successfully");
            return true;
        }
        finally
        {
            this.refreshLock.Release();
        }
    }

    private async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri)
        {
            Version = request.Version,
        };

        // Copy headers
        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        // Copy content if present
        if (request.Content != null)
        {
            var contentBytes = await request.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(contentBytes);

            foreach (var header in request.Content.Headers)
            {
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        return clone;
    }
}