// <copyright file="TokenRefreshedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.OAuth;

/// <summary>
/// Event args for token refresh events.
/// </summary>
public class TokenRefreshedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TokenRefreshedEventArgs"/> class.
    /// </summary>
    /// <param name="accessToken">The new access token.</param>
    /// <param name="refreshToken">The new refresh token.</param>
    /// <param name="expiresIn">Token expiration in seconds.</param>
    public TokenRefreshedEventArgs(string accessToken, string? refreshToken, int expiresIn)
    {
        this.AccessToken = accessToken;
        this.RefreshToken = refreshToken;
        this.ExpiresIn = expiresIn;
    }

    /// <summary>
    /// Gets the new access token.
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// Gets the new refresh token.
    /// </summary>
    public string? RefreshToken { get; }

    /// <summary>
    /// Gets the token expiration in seconds.
    /// </summary>
    public int ExpiresIn { get; }
}
