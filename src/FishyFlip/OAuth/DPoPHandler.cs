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
#if NETSTANDARD2_0 || NETSTANDARD2_1
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.Pkcs;
using ECCurveNet = System.Security.Cryptography.ECCurve;
using ECPointBC = Org.BouncyCastle.Math.EC.ECPoint;
using ECPointNet = System.Security.Cryptography.ECPoint;
#endif

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
        var keyPair = new DPoPKeyPair();

#if NET5_0_OR_GREATER
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        keyPair.PublicKey = ecdsa.ExportSubjectPublicKeyInfoPem();
        keyPair.PrivateKey = ecdsa.ExportECPrivateKeyPem();

        // Also generate JWK JSON for compatibility with existing code
        var exportParams = ecdsa.ExportParameters(false);
        var jwk = new Dictionary<string, string>
        {
            ["kty"] = "EC",
            ["crv"] = "P-256",
            ["x"] = Base64UrlEncoder.Encode(exportParams.Q.X!),
            ["y"] = Base64UrlEncoder.Encode(exportParams.Q.Y!),
            ["use"] = "sig",
            ["alg"] = "ES256",
        };
#else
        // TODO: This was vibe coded. I picked bouncycastle because I knew it could do this,
        // but otherwise this is all LLM for this section.
        // I'm going to assume it's wrong, but it compiles and it's netstandard in OAuth.
        // If you see it fail and wonder why this exists, now you know!
        var curve = SecNamedCurves.GetByName("secp256r1");
        var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);

        var keyGenParams = new ECKeyGenerationParameters(domainParams, new SecureRandom());
        var keyGenerator = new ECKeyPairGenerator();
        keyGenerator.Init(keyGenParams);

        var bcKeyPair = keyGenerator.GenerateKeyPair();
        var privateKey = (ECPrivateKeyParameters)bcKeyPair.Private;
        var publicKey = (ECPublicKeyParameters)bcKeyPair.Public;

        // Store keys as base64 encoded DER for netstandard
        var privateKeyInfo = Org.BouncyCastle.Pkcs.PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
        var publicKeyInfo = Org.BouncyCastle.X509.SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);

        keyPair.PrivateKey = Convert.ToBase64String(privateKeyInfo.GetDerEncoded());
        keyPair.PublicKey = Convert.ToBase64String(publicKeyInfo.GetDerEncoded());

        // Generate JWK JSON
        var qPoint = publicKey.Q.Normalize();
        var xBytes = qPoint.XCoord.GetEncoded();
        var yBytes = qPoint.YCoord.GetEncoded();

        var jwk = new Dictionary<string, string>
        {
            ["kty"] = "EC",
            ["crv"] = "P-256",
            ["x"] = Base64UrlEncoder.Encode(xBytes),
            ["y"] = Base64UrlEncoder.Encode(yBytes),
            ["use"] = "sig",
            ["alg"] = "ES256",
        };
#endif

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
#if NET5_0_OR_GREATER
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportFromPem(keyPair.PrivateKey);
        var securityKey = new ECDsaSecurityKey(ecdsa);
        var parameters = ecdsa.ExportParameters(false);
#else
        // Use BouncyCastle for netstandard
        var privateKeyBytes = Convert.FromBase64String(keyPair.PrivateKey);
        var privateKeyInfo = PrivateKeyInfo.GetInstance(privateKeyBytes);
        var bcPrivateKey = (ECPrivateKeyParameters)PrivateKeyFactory.CreateKey(privateKeyInfo);

        // Convert BouncyCastle key to .NET ECDsa
        using var ecdsa = ECDsa.Create();
        var curve = ECCurveNet.NamedCurves.nistP256;
        var d = bcPrivateKey.D.ToByteArrayUnsigned();

        // Pad d to 32 bytes if needed
        if (d.Length < 32)
        {
            var paddedD = new byte[32];
            Array.Copy(d, 0, paddedD, 32 - d.Length, d.Length);
            d = paddedD;
        }

        var qPoint = bcPrivateKey.Parameters.G.Multiply(bcPrivateKey.D).Normalize();
        var x = qPoint.XCoord.GetEncoded();
        var y = qPoint.YCoord.GetEncoded();

        // Pad coordinates to 32 bytes if needed
        if (x.Length < 32)
        {
            var paddedX = new byte[32];
            Array.Copy(x, 0, paddedX, 32 - x.Length, x.Length);
            x = paddedX;
        }

        if (y.Length < 32)
        {
            var paddedY = new byte[32];
            Array.Copy(y, 0, paddedY, 32 - y.Length, y.Length);
            y = paddedY;
        }

        var parameters = new ECParameters
        {
            Curve = curve,
            D = d,
            Q = new ECPointNet { X = x, Y = y },
        };
        ecdsa.ImportParameters(parameters);

        var securityKey = new ECDsaSecurityKey(ecdsa);
#endif

        var claims = new Dictionary<string, object>
        {
            ["jti"] = Guid.NewGuid().ToString(),
            ["htm"] = method.ToUpperInvariant(),
            ["htu"] = url,
            ["iat"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        };

        if (!string.IsNullOrEmpty(nonce))
        {
            claims["nonce"] = nonce!;
        }

        if (!string.IsNullOrEmpty(accessTokenHash))
        {
            claims["ath"] = accessTokenHash!;
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
#if NET5_0_OR_GREATER
            ["x"] = Base64UrlEncoder.Encode(parameters.Q.X!),
            ["y"] = Base64UrlEncoder.Encode(parameters.Q.Y!),
#else
            ["x"] = Base64UrlEncoder.Encode(x),
            ["y"] = Base64UrlEncoder.Encode(y),
#endif
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
#if NET5_0_OR_GREATER
        var hash = SHA256.HashData(Encoding.ASCII.GetBytes(accessToken));
#else
        byte[] hash;
        using (var sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
        }
#endif
        return Base64UrlEncoder.Encode(hash);
    }
}