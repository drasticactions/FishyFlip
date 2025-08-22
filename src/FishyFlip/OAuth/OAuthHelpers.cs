// <copyright file="OAuthHelpers.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FishyFlip.OAuth;

/// <summary>
/// Helper methods for OAuth operations.
/// </summary>
internal static class OAuthHelpers
{
    private const string PkceCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";

    /// <summary>
    /// Generates a PKCE code verifier and challenge pair.
    /// </summary>
    /// <returns>A tuple containing the verifier and challenge.</returns>
    public static (string Verifier, string Challenge) GeneratePKCE()
    {
        // Generate code verifier (43-128 characters)
        var verifier = GenerateRandomString(128);

        // Generate code challenge using S256 method
        using var sha256 = SHA256.Create();
        var challengeBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(verifier));
        var challenge = Base64UrlEncoder.Encode(challengeBytes);

        return (verifier, challenge);
    }

    /// <summary>
    /// Generates a random state parameter for OAuth.
    /// </summary>
    /// <returns>A random state string.</returns>
    public static string GenerateState()
    {
        return GenerateRandomString(32);
    }

    /// <summary>
    /// Generates a client assertion JWT for OAuth client authentication.
    /// </summary>
    /// <param name="clientId">The client ID.</param>
    /// <param name="audience">The audience (issuer URL).</param>
    /// <param name="signingKey">The signing key.</param>
    /// <returns>The client assertion JWT.</returns>
    public static string GenerateClientAssertion(string clientId, string audience, SecurityKey signingKey)
    {
        var now = DateTime.UtcNow;
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = clientId,
            Audience = audience,
            IssuedAt = now,
            NotBefore = now,
            Expires = now.AddMinutes(5),
            Claims = new Dictionary<string, object>
            {
                ["sub"] = clientId,
                ["jti"] = Guid.NewGuid().ToString(),
            },
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.EcdsaSha256),
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(descriptor);

        // Add kid to header if available
        if (!string.IsNullOrEmpty(signingKey.KeyId))
        {
            token.Header.Add("kid", signingKey.KeyId);
        }

        return handler.WriteToken(token);
    }

    /// <summary>
    /// Parses a JWT to extract the subject (DID).
    /// </summary>
    /// <param name="jwt">The JWT token.</param>
    /// <returns>The subject claim if found.</returns>
    public static string? ExtractSubFromJwt(string jwt)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Subject;
        }
        catch
        {
            return null;
        }
    }

    private static string GenerateRandomString(int length)
    {
        var result = new StringBuilder(length);
        using var rng = RandomNumberGenerator.Create();
        var buffer = new byte[length];
        rng.GetBytes(buffer);

        foreach (var b in buffer)
        {
            result.Append(PkceCharacters[b % PkceCharacters.Length]);
        }

        return result.ToString();
    }
}