// <copyright file="OAuthClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
#if NET
using System.Net.Http.Json;
#endif
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FishyFlip.OAuth.Models;
using Microsoft.Extensions.Logging;

namespace FishyFlip.OAuth;

/// <summary>
/// OAuth client for ATProtocol authentication.
/// </summary>
internal class OAuthClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger? logger;
    private OAuthState? currentState;
    private DPoPKeyPair? keyPair;

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="logger">Optional logger.</param>
    public OAuthClient(ILogger? logger = null)
    {
        this.httpClient = new HttpClient();
        this.logger = logger;
    }

    /// <summary>
    /// Gets the current DPoP key pair.
    /// </summary>
    public DPoPKeyPair? KeyPair => this.keyPair;

    /// <summary>
    /// Gets the current OAuth state.
    /// </summary>
    public OAuthState? State => this.currentState;

    /// <summary>
    /// Discovers OAuth endpoints from the authorization server.
    /// </summary>
    /// <param name="issuerUrl">The issuer URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The OAuth configuration.</returns>
    public async Task<Dictionary<string, object>?> DiscoverEndpointsAsync(
        string issuerUrl,
        CancellationToken cancellationToken = default)
    {
        var discoveryUrl = $"{issuerUrl.TrimEnd('/')}/.well-known/oauth-authorization-server";

        try
        {
            var response = await this.httpClient.GetAsync(discoveryUrl, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                this.logger?.LogWarning($"Failed to discover OAuth endpoints from {discoveryUrl}: {response.StatusCode}");
                return null;
            }

#if NET
            return await response.Content.ReadFromJsonAsync(SourceGenerationContext.Default.DictionaryStringObject, cancellationToken);
#else
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize(json, SourceGenerationContext.Default.DictionaryStringObject);
#endif
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error discovering OAuth endpoints");
            return null;
        }
    }

    /// <summary>
    /// Prepares an authorization request.
    /// </summary>
    /// <param name="clientId">The client ID.</param>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <param name="scope">The requested scope.</param>
    /// <param name="issuerUrl">The issuer URL.</param>
    /// <param name="loginHint">Optional login hint.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authorization URL.</returns>
    public async Task<string?> PrepareAuthorizationAsync(
        string clientId,
        string redirectUri,
        string scope,
        string issuerUrl,
        string? loginHint = null,
        CancellationToken cancellationToken = default)
    {
        // Discover endpoints
        var config = await this.DiscoverEndpointsAsync(issuerUrl, cancellationToken);
        if (config == null)
        {
            return null;
        }

#if NET5_0_OR_GREATER
        var authEndpoint = config.GetValueOrDefault("authorization_endpoint")?.ToString();
        var tokenEndpoint = config.GetValueOrDefault("token_endpoint")?.ToString();
        var parEndpoint = config.GetValueOrDefault("pushed_authorization_request_endpoint")?.ToString();
        var revocationEndpoint = config.GetValueOrDefault("revocation_endpoint")?.ToString();
#else
        var authEndpoint = config.TryGetValue("authorization_endpoint", out var authEp) ? authEp?.ToString() : null;
        var tokenEndpoint = config.TryGetValue("token_endpoint", out var tokenEp) ? tokenEp?.ToString() : null;
        var parEndpoint = config.TryGetValue("pushed_authorization_request_endpoint", out var parEp) ? parEp?.ToString() : null;
        var revocationEndpoint = config.TryGetValue("revocation_endpoint", out var revEp) ? revEp?.ToString() : null;
#endif

        if (string.IsNullOrEmpty(authEndpoint) || string.IsNullOrEmpty(tokenEndpoint))
        {
            this.logger?.LogError("Missing required OAuth endpoints");
            return null;
        }

        // Generate PKCE
        var (verifier, challenge) = OAuthHelpers.GeneratePKCE();

        // Generate DPoP key pair
        this.keyPair = DPoPHandler.GenerateKeyPair();

        // Generate state
        var state = OAuthHelpers.GenerateState();

        // Store state
        this.currentState = new OAuthState
        {
            State = state,
            CodeVerifier = verifier,
            CodeChallenge = challenge,
            KeyPair = this.keyPair,
            AuthorizationEndpoint = authEndpoint,
            TokenEndpoint = tokenEndpoint,
            Issuer = issuerUrl,
            PushedAuthorizationRequestEndpoint = parEndpoint,
            RevocationEndpoint = revocationEndpoint,
        };

        // If PAR endpoint is available, use it
        if (!string.IsNullOrEmpty(parEndpoint))
        {
            var requestUri = await this.PushAuthorizationRequestAsync(
                parEndpoint!,
                clientId,
                redirectUri,
                scope,
                state,
                challenge,
                loginHint,
                cancellationToken);

            if (!string.IsNullOrEmpty(requestUri))
            {
                // Build URL with request_uri
                var uriBuilder = new UriBuilder(authEndpoint);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["client_id"] = clientId;
                query["request_uri"] = requestUri;
                uriBuilder.Query = query.ToString();
                return uriBuilder.ToString();
            }
        }

        // Fallback to direct authorization request
        var authUriBuilder = new UriBuilder(authEndpoint);
        var authQuery = HttpUtility.ParseQueryString(authUriBuilder.Query);
        authQuery["response_type"] = "code";
        authQuery["client_id"] = clientId;
        authQuery["redirect_uri"] = redirectUri;
        authQuery["scope"] = scope;
        authQuery["state"] = state;
        authQuery["code_challenge"] = challenge;
        authQuery["code_challenge_method"] = "S256";

        if (!string.IsNullOrEmpty(loginHint))
        {
            authQuery["login_hint"] = loginHint;
        }

        authUriBuilder.Query = authQuery.ToString();
        return authUriBuilder.ToString();
    }

    /// <summary>
    /// Exchanges an authorization code for tokens.
    /// </summary>
    /// <param name="code">The authorization code.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The token response.</returns>
    public async Task<TokenResponse?> ExchangeCodeForTokensAsync(
        string code,
        string clientId,
        string redirectUri,
        CancellationToken cancellationToken = default)
    {
        if (this.currentState == null || this.keyPair == null)
        {
            this.logger?.LogError("No current authorization state");
            return null;
        }

        var tokenEndpoint = this.currentState.TokenEndpoint ?? string.Empty;
        if (string.IsNullOrEmpty(tokenEndpoint))
        {
            return null;
        }

        // Prepare token request
        var tokenRequest = new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["code"] = code,
            ["redirect_uri"] = redirectUri,
            ["client_id"] = clientId,
            ["code_verifier"] = this.currentState.CodeVerifier,
        };

        // Create DPoP proof
        var dpopProof = DPoPHandler.GenerateDPoPProof(
            this.keyPair,
            "POST",
            tokenEndpoint,
            this.currentState.Nonce);

        var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint)
        {
            Content = new FormUrlEncodedContent(tokenRequest),
        };
        request.Headers.Add("DPoP", dpopProof);

        try
        {
            var response = await this.httpClient.SendAsync(request, cancellationToken);

            // Check for DPoP nonce
            if (response.Headers.TryGetValues("DPoP-Nonce", out var nonceValues))
            {
                this.currentState.Nonce = nonceValues.FirstOrDefault();
            }

            if (!response.IsSuccessStatusCode)
            {
#if NET
                var error = await response.Content.ReadAsStringAsync(cancellationToken);
#else
                var error = await response.Content.ReadAsStringAsync();
#endif
                this.logger?.LogError($"Token exchange failed: {response.StatusCode} - {error}");
                return null;
            }

#if NET
            return await response.Content.ReadFromJsonAsync<TokenResponse>(SourceGenerationContext.Default.TokenResponse, cancellationToken);
#else
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TokenResponse>(json, SourceGenerationContext.Default.TokenResponse);
#endif
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error exchanging code for tokens");
            return null;
        }
    }

    /// <summary>
    /// Refreshes an access token.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The token response.</returns>
    public async Task<TokenResponse?> RefreshTokenAsync(
        string refreshToken,
        string clientId,
        CancellationToken cancellationToken = default)
    {
        if (this.currentState == null || this.keyPair == null)
        {
            this.logger?.LogError("No current authorization state");
            return null;
        }

        var tokenEndpoint = this.currentState.TokenEndpoint ?? string.Empty;
        if (string.IsNullOrEmpty(tokenEndpoint))
        {
            return null;
        }

        // Prepare refresh request
        var refreshRequest = new Dictionary<string, string>
        {
            ["grant_type"] = "refresh_token",
            ["refresh_token"] = refreshToken,
            ["client_id"] = clientId,
        };

        // Create DPoP proof
        var dpopProof = DPoPHandler.GenerateDPoPProof(
            this.keyPair,
            "POST",
            tokenEndpoint,
            this.currentState.Nonce);

        var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint)
        {
            Content = new FormUrlEncodedContent(refreshRequest),
        };
        request.Headers.Add("DPoP", dpopProof);

        try
        {
            var response = await this.httpClient.SendAsync(request, cancellationToken);

            // Check for DPoP nonce
            if (response.Headers.TryGetValues("DPoP-Nonce", out var nonceValues))
            {
                this.currentState.Nonce = nonceValues.FirstOrDefault();
            }

            if (!response.IsSuccessStatusCode)
            {
#if NET
                var error = await response.Content.ReadAsStringAsync(cancellationToken);
#else
                var error = await response.Content.ReadAsStringAsync();
#endif
                this.logger?.LogError($"Token refresh failed: {response.StatusCode} - {error}");
                return null;
            }

#if NET
            return await response.Content.ReadFromJsonAsync<TokenResponse>(SourceGenerationContext.Default.TokenResponse, cancellationToken);
#else
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TokenResponse>(json, SourceGenerationContext.Default.TokenResponse);
#endif
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error refreshing token");
            return null;
        }
    }

    /// <summary>
    /// Sets up the OAuth client from an existing session.
    /// </summary>
    /// <param name="keyPairJson">The DPoP key pair JSON.</param>
    /// <param name="issuerUrl">The issuer URL.</param>
    public void SetupFromSession(string keyPairJson, string issuerUrl)
    {
        // For compatibility with existing RSA JSON format
        // We'll need to handle conversion or generate new EC keys
        this.keyPair = DPoPHandler.GenerateKeyPair();
        this.keyPair.JwkJson = keyPairJson;

        this.currentState = new OAuthState
        {
            KeyPair = this.keyPair,
            Issuer = issuerUrl,
        };
    }

    private async Task<string?> PushAuthorizationRequestAsync(
        string parEndpoint,
        string clientId,
        string redirectUri,
        string scope,
        string state,
        string codeChallenge,
        string? loginHint,
        CancellationToken cancellationToken)
    {
        var parRequest = new Dictionary<string, string>
        {
            ["response_type"] = "code",
            ["client_id"] = clientId,
            ["redirect_uri"] = redirectUri,
            ["scope"] = scope,
            ["state"] = state,
            ["code_challenge"] = codeChallenge,
            ["code_challenge_method"] = "S256",
        };

        if (!string.IsNullOrEmpty(loginHint))
        {
            parRequest["login_hint"] = loginHint!;
        }

        // Create DPoP proof for PAR
        var dpopProof = DPoPHandler.GenerateDPoPProof(
            this.keyPair!,
            "POST",
            parEndpoint,
            this.currentState?.Nonce);

        var request = new HttpRequestMessage(HttpMethod.Post, parEndpoint)
        {
            Content = new FormUrlEncodedContent(parRequest),
        };
        request.Headers.Add("DPoP", dpopProof);

        try
        {
            var response = await this.httpClient.SendAsync(request, cancellationToken);

            // Check for DPoP nonce
            if (response.Headers.TryGetValues("DPoP-Nonce", out var nonceValues))
            {
                if (this.currentState != null)
                {
                    this.currentState.Nonce = nonceValues.FirstOrDefault();
                }
            }

            if (!response.IsSuccessStatusCode)
            {
                this.logger?.LogWarning($"PAR request failed: {response.StatusCode}");
                return null;
            }

#if NET
            var parResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, JsonElement>>(SourceGenerationContext.Default.DictionaryStringJsonElement, cancellationToken);
            return parResponse?.GetValueOrDefault("request_uri").GetString();
#else
            var json = await response.Content.ReadAsStringAsync();
            var parResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, SourceGenerationContext.Default.DictionaryStringJsonElement);
            return parResponse != null && parResponse.TryGetValue("request_uri", out var uri) ? uri.GetString() : null;
#endif
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error pushing authorization request");
            return null;
        }
    }
}