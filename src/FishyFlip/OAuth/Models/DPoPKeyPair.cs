// <copyright file="DPoPKeyPair.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.OAuth.Models;

/// <summary>
/// Represents a DPoP key pair for OAuth authentication.
/// </summary>
public class DPoPKeyPair
{
    /// <summary>
    /// Gets or sets the public key in PEM format.
    /// </summary>
    public string PublicKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the private key in PEM format.
    /// </summary>
    public string PrivateKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the key in JWK JSON format for compatibility.
    /// </summary>
    public string? JwkJson { get; set; }
}