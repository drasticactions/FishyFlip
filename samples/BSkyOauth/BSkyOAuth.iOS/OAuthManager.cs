// <copyright file="OAuthManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using AuthenticationServices;

namespace BSkyOAuth;

/// <summary>
/// OAuth Manager.
/// </summary>
public class OAuthManager(UIViewController viewController, string schema, Action<NSUrl?> success, Action<NSError?> error)
{
    private UIViewController viewController = viewController;

    private ASWebAuthenticationSessionCallback callback = ASWebAuthenticationSessionCallback.Create(schema);

    private Action<NSUrl?> success = success;

    private Action<NSError?> error = error;

    /// <summary>
    /// Start Authentication.
    /// </summary>
    /// <param name="authEndpoint">The Auth Endpoint.</param>
    /// <returns>True if successful.</returns>
    public bool StartAuthentication(string authEndpoint)
    {
        using var authSession = new ASWebAuthenticationSession(
            new NSUrl(authEndpoint),
            this.callback,
            (NSUrl? callbackUrl, NSError? error) =>
            {
                if (error != null)
                {
                    this.error(error);
                }
                else
                {
                    this.success(callbackUrl);
                }
            });
        authSession.PresentationContextProvider = new AuthenticationPresentationContext(this.viewController);
        return authSession.Start();
    }

}