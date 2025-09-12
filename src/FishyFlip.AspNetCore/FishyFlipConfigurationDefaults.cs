// <copyright file="FishyFlipConfigurationDefaults.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.AspNetCore;

/// <summary>
/// Default value constants for FishyFlip configuration.
/// </summary>
public static class FishyFlipConfigurationDefaults
{
    /// <summary>
    /// Default instance URL.
    /// </summary>
    public const string DefaultInstanceUrl = "https://bsky.social";

    /// <summary>
    /// Default cookie authentication scheme.
    /// </summary>
    public const string DefaultCookieAuthenticationScheme = "FishyFlipCookie";

    /// <summary>
    /// Default JWT Bearer authentication scheme.
    /// </summary>
    public const string DefaultJwtBearerAuthenticationScheme = "FishyFlipJwtBearer";

    /// <summary>
    /// Gets default OAuth scopes.
    /// </summary>
    public static IEnumerable<string> DefaultScopes { get; } = new[] { "atproto", "transition:generic" };
}