// <copyright file="OAuthState.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.OAuth.Models;

/// <summary>
/// Represents the state of an OAuth authorization flow.
/// </summary>
internal class OAuthState
{
    /// <summary>
    /// Gets or sets the state identifier.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the PKCE code verifier.
    /// </summary>
    public string CodeVerifier { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the PKCE code challenge.
    /// </summary>
    public string CodeChallenge { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the DPoP key pair.
    /// </summary>
    public DPoPKeyPair? KeyPair { get; set; }

    /// <summary>
    /// Gets or sets the authorization endpoint URL.
    /// </summary>
    public string? AuthorizationEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the token endpoint URL.
    /// </summary>
    public string? TokenEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the issuer URL.
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Gets or sets the pushed authorization request endpoint.
    /// </summary>
    public string? PushedAuthorizationRequestEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the revocation endpoint.
    /// </summary>
    public string? RevocationEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the nonce for DPoP.
    /// </summary>
    public string? Nonce { get; set; }
}