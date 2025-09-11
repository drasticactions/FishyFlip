// <copyright file="FishyFlipOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.AspNetCore;

/// <summary>
/// Configuration options for FishyFlip ASP.NET Core integration.
/// </summary>
public class FishyFlipOptions
{
    /// <summary>
    /// Gets or sets the default ATProtocol instance URL.
    /// </summary>
    public string InstanceUrl { get; set; } = FishyFlipConfigurationDefaults.DefaultInstanceUrl;

    /// <summary>
    /// Gets or sets the OAuth client ID for authentication flows.
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// Gets or sets the OAuth redirect URI for authentication flows.
    /// </summary>
    public string? RedirectUri { get; set; }

    /// <summary>
    /// Gets or sets the OAuth scopes to request during authentication.
    /// </summary>
    public IEnumerable<string> Scopes { get; set; } = FishyFlipConfigurationDefaults.DefaultScopes;

    /// <summary>
    /// Gets or sets the cookie authentication scheme name.
    /// </summary>
    [Obsolete("Moved to extension method parameter, this property is ignored and will be removed in the future", error: true)]
    public string CookieAuthenticationScheme { get; set; } = FishyFlipConfigurationDefaults.DefaultCookieAuthenticationScheme;

    /// <summary>
    /// Gets or sets the JWT bearer authentication scheme name.
    /// </summary>
    [Obsolete("Moved to extension method parameter, this property is ignored and will be removed in the future", error: true)]
    public string JwtBearerAuthenticationScheme { get; set; } = FishyFlipConfigurationDefaults.DefaultJwtBearerAuthenticationScheme;

    /// <summary>
    /// Gets or sets the default session expiration time.
    /// </summary>
    public TimeSpan SessionExpiration { get; set; } = TimeSpan.FromHours(24);

    /// <summary>
    /// Gets or sets a value indicating whether to automatically refresh tokens before expiration.
    /// </summary>
    public bool AutoRefreshTokens { get; set; } = true;

    /// <summary>
    /// Gets or sets the time before token expiration to attempt refresh.
    /// </summary>
    public TimeSpan RefreshThreshold { get; set; } = TimeSpan.FromMinutes(10);
}