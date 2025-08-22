// <copyright file="OAuthClientMetadata.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#nullable enable annotations
#nullable disable warnings

using Microsoft.IdentityModel.Tokens;

namespace FishyFlip.Models;

/// <summary>
/// Represents OAuth client metadata configuration parameters.
/// For more information on how to use these parameters, see https://github.com/bluesky-social/atproto/blob/main/packages/api/OAUTH.md
/// and https://atproto.com/specs/oauth.
/// </summary>
public class OAuthClientMetadata
{
    /// <summary>
    /// Gets or sets the client identifier. Must exactly match the full URL used to fetch the client metadata file itself.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the application type. Must be one of 'web' or 'native', with 'web' as the default if not specified.
    /// Used by the Authorization Server to enforce relevant "best current practices".
    /// </summary>
    [JsonPropertyName("application_type")]
    public string? ApplicationType { get; set; } = "web";

    /// <summary>
    /// Gets or sets the grant types. 'authorization_code' must always be included. 'refresh_token' is optional,
    /// but must be included if the client will make token refresh requests.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("grant_types")]
    public string[] GrantTypes { get; set; }

    /// <summary>
    /// Gets or sets the space-separated scope values. All scope values which might be requested by this client
    /// must be declared here. The 'atproto' scope is required.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    /// <summary>
    /// Gets or sets the response types. 'code' must be included.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("response_types")]
    public string[] ResponseTypes { get; set; }

    /// <summary>
    /// Gets or sets the redirect URIs. At least one redirect URI is required.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("redirect_uris")]
    public string[] RedirectUris { get; set; }

    /// <summary>
    /// Gets or sets the token endpoint authentication method. Confidential clients must set this to 'private_key_jwt'.
    /// </summary>
    [JsonPropertyName("token_endpoint_auth_method")]
    public string? TokenEndpointAuthMethod { get; set; }

    /// <summary>
    /// Gets or sets the token endpoint authentication signing algorithm. 'none' is never allowed.
    /// The current recommended and most-supported algorithm is 'ES256'.
    /// </summary>
    [JsonPropertyName("token_endpoint_auth_signing_alg")]
    public string? TokenEndpointAuthSigningAlg { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether DPoP bound access tokens are required. DPoP is mandatory for all clients,
    /// so this must be present and true.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("dpop_bound_access_tokens")]
    public bool DPoPBoundAccessTokens { get; set; } = true;

    /// <summary>
    /// Gets or sets the JWKS container. Confidential clients must supply at least one public key in JWK format
    /// for use with JWT client authentication. Either this or JwksUri must be provided for confidential clients, but not both.
    /// </summary>
    [JsonPropertyName("jwks")]
    public JsonWebKeySet? Jwks { get; set; }

    /// <summary>
    /// Gets or sets the JWKS URI. URL pointing to a JWKS JSON object. Either this or Jwks must be provided
    /// for confidential clients, but not both.
    /// </summary>
    [JsonPropertyName("jwks_uri")]
    public string? JwksUri { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the client.
    /// </summary>
    [JsonPropertyName("client_name")]
    public string? ClientName { get; set; }

    /// <summary>
    /// Gets or sets the client URI. Not to be confused with client_id, this is a homepage URL for the client.
    /// If provided, must have the same hostname as client_id.
    /// </summary>
    [JsonPropertyName("client_uri")]
    public string? ClientUri { get; set; }

    /// <summary>
    /// Gets or sets the logo URI. Only https: URIs are allowed.
    /// </summary>
    [JsonPropertyName("logo_uri")]
    public string? LogoUri { get; set; }

    /// <summary>
    /// Gets or sets the terms of service URI. Only https: URIs are allowed.
    /// </summary>
    [JsonPropertyName("tos_uri")]
    public string? TosUri { get; set; }

    /// <summary>
    /// Gets or sets the privacy policy URI. Only https: URIs are allowed.
    /// </summary>
    [JsonPropertyName("policy_uri")]
    public string? PolicyUri { get; set; }

    /// <summary>
    /// Generates a <see cref="OAuthClientMetadata"/> instance from a JSON string.
    /// </summary>
    /// <param name="json">JSON string.</param>
    /// <returns><see cref="OAuthClientMetadata"/>.</returns>
    public static OAuthClientMetadata FromJson(string json)
    {
        return JsonSerializer.Deserialize<OAuthClientMetadata>(json, SourceGenerationContext.Default.OAuthClientMetadata);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, SourceGenerationContext.Default.OAuthClientMetadata);
    }
}