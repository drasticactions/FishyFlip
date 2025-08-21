// <copyright file="DPoPHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using FishyFlip.OAuth.Models;
using Microsoft.IdentityModel.Tokens;

namespace FishyFlip.OAuth;

/// <summary>
/// Handles DPoP (Demonstrating Proof of Possession) operations for OAuth.
/// </summary>
internal static class DPoPHandler
{
    /// <summary>
    /// Generates a new DPoP key pair using EC256.
    /// </summary>
    /// <returns>A new DPoP key pair.</returns>
    public static DPoPKeyPair GenerateKeyPair()
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

        var keyPair = new DPoPKeyPair
        {
            PublicKey = ecdsa.ExportSubjectPublicKeyInfoPem(),
            PrivateKey = ecdsa.ExportECPrivateKeyPem(),
        };

        // Also generate JWK JSON for compatibility with existing code
        var parameters = ecdsa.ExportParameters(false);
        var jwk = new Dictionary<string, string>
        {
            ["kty"] = "EC",
            ["crv"] = "P-256",
            ["x"] = Base64UrlEncoder.Encode(parameters.Q.X!),
            ["y"] = Base64UrlEncoder.Encode(parameters.Q.Y!),
            ["use"] = "sig",
            ["alg"] = "ES256",
        };

        keyPair.JwkJson = JsonSerializer.Serialize(jwk, SourceGenerationContext.Default.DictionaryStringString);
        return keyPair;
    }

    /// <summary>
    /// Generates a DPoP proof JWT.
    /// </summary>
    /// <param name="keyPair">The DPoP key pair.</param>
    /// <param name="method">The HTTP method.</param>
    /// <param name="url">The target URL.</param>
    /// <param name="nonce">Optional DPoP nonce.</param>
    /// <param name="accessTokenHash">Optional access token hash for binding.</param>
    /// <returns>The DPoP proof JWT.</returns>
    public static string GenerateDPoPProof(
        DPoPKeyPair keyPair,
        string method,
        string url,
        string? nonce = null,
        string? accessTokenHash = null)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportFromPem(keyPair.PrivateKey);

        var securityKey = new ECDsaSecurityKey(ecdsa);
        var parameters = ecdsa.ExportParameters(false);

        var claims = new Dictionary<string, object>
        {
            ["jti"] = Guid.NewGuid().ToString(),
            ["htm"] = method.ToUpperInvariant(),
            ["htu"] = url,
            ["iat"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        };

        if (!string.IsNullOrEmpty(nonce))
        {
            claims["nonce"] = nonce;
        }

        if (!string.IsNullOrEmpty(accessTokenHash))
        {
            claims["ath"] = accessTokenHash;
        }

        var descriptor = new SecurityTokenDescriptor
        {
            Claims = claims,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.EcdsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false },
            },
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(descriptor);

        // Add JWK to header
        var jwk = new Dictionary<string, object>
        {
            ["kty"] = "EC",
            ["crv"] = "P-256",
            ["x"] = Base64UrlEncoder.Encode(parameters.Q.X!),
            ["y"] = Base64UrlEncoder.Encode(parameters.Q.Y!),
        };

        token.Header.Add("jwk", jwk);
        token.Header.Remove("typ");
        token.Header.Add("typ", "dpop+jwt");

        return handler.WriteToken(token);
    }

    /// <summary>
    /// Creates an access token hash for DPoP binding.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <returns>The base64url-encoded hash.</returns>
    public static string CreateAccessTokenHash(string accessToken)
    {
        var hash = SHA256.HashData(Encoding.ASCII.GetBytes(accessToken));
        return Base64UrlEncoder.Encode(hash);
    }
}