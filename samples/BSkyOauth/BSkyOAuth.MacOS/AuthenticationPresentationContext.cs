// <copyright file="AuthenticationPresentationContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using AuthenticationServices;

namespace BSkyOAuth;

/// <summary>
/// Authentication Presentation Context.
/// </summary>
public class AuthenticationPresentationContext : NSObject, IASWebAuthenticationPresentationContextProviding
{
    private NSWindow window;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationPresentationContext"/> class.
    /// </summary>
    /// <param name="window">The Window.</param>
    public AuthenticationPresentationContext(NSWindow window)
    {
        this.window = window;
    }

    /// <inheritdoc/>
    public NSWindow GetPresentationAnchor(ASWebAuthenticationSession session)
    {
        return this.window;
    }
}