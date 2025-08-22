// <copyright file="AuthenticationPresentationContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using AuthenticationServices;

namespace BSkyOAuth;

/// <summary>
/// Authentication Presentation Context.
/// </summary>
public class AuthenticationPresentationContext(UIViewController viewController) : NSObject, IASWebAuthenticationPresentationContextProviding
{
    private UIViewController viewController = viewController;

    /// <inheritdoc/>
    public UIWindow GetPresentationAnchor(ASWebAuthenticationSession session)
    {
        return this.viewController.View!.Window!;
    }
}