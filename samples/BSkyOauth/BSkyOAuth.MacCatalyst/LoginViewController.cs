// <copyright file="LoginViewController.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using BSkyOAuth;
using FishyFlip;
using FishyFlip.Models;

/// <summary>
/// Login View Controller.
/// </summary>
public sealed class LoginViewController : UIViewController
{
    private const string ClientMetadataUrl = "https://drasticactions.vip/client-metadata.json";

    private const string RedirectUri = "vip.drasticactions:/callback";

    private readonly OAuthManager oauthManager;

    private ATProtocol atProtocol;

    private UIButton authButton;

    private UITextField handleField;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginViewController"/> class.
    /// </summary>
    public LoginViewController()
    {
        this.oauthManager = new OAuthManager(this, "vip.drasticactions", this.OnSuccess, this.OnError);
        var atProtocolBuilder = new ATProtocolBuilder();
        this.atProtocol = atProtocolBuilder.Build();

        this.View!.BackgroundColor = UIColor.SystemBackground;
        this.authButton = new UIButton(UIButtonType.System);
        this.authButton.SetTitle("Authenticate", UIControlState.Normal);
        this.authButton.TouchUpInside += this.AuthButton_TouchUpInside;
        this.View!.AddSubview(this.authButton);

        this.handleField = new UITextField();
        this.handleField.Placeholder = "Handle";

        this.View!.AddSubview(this.handleField);
        this.handleField.TranslatesAutoresizingMaskIntoConstraints = false;

        this.View!.AddConstraints(new[]
        {
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Top, 1, 100),
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
        });

        this.authButton.TranslatesAutoresizingMaskIntoConstraints = false;
        this.View!.AddConstraints(new[]
        {
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.handleField, NSLayoutAttribute.Bottom, 1, 20),
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
        });
    }

    private async void AuthButton_TouchUpInside(object? sender, EventArgs e)
    {
        if (!ATIdentifier.TryCreate(this.handleField.Text, out ATIdentifier? atIdentifier))
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", "Invalid handle", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        var (uri, error) = await this.atProtocol.GenerateOAuth2AuthenticationUrlResultAsync(
            ClientMetadataUrl,
            RedirectUri,
            new[] { "atproto" },
            atIdentifier!);

        if (error != null)
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", error.ToString(), UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        this.oauthManager.StartAuthentication(uri!);
    }

    private async void OnSuccess(NSUrl? callbackUrl)
    {
        // OnSuccess means we got a successful response from the session, but
        // there may be an error in the response. We need to check for that.
        if (callbackUrl != null)
        {
            var parameters = callbackUrl.Query?.TrimStart('?')
                .Split('&')
                .Select(param => param.Split('='))
                .ToDictionary(split => split[0], split => Uri.UnescapeDataString(split[1])) ?? new Dictionary<string, string>();

            if (parameters.TryGetValue("code", out string? code))
            {
                // If we got a code, we can complete the authentication process.
                var (session, error) = await this.atProtocol.AuthenticateWithOAuth2CallbackResultAsync(callbackUrl.ToString());
                if (session != null)
                {
                    // We have a session!
                    this.InvokeOnMainThread(() =>
                    {
                        var alert = UIAlertController.Create("Success", $"Authenticated as {session.Handle}", UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                        this.PresentViewController(alert, true, null);
                    });
                }
                else
                {
                    this.InvokeOnMainThread(() =>
                    {
                        var alert = UIAlertController.Create("Error", "Failed to authenticate", UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                        this.PresentViewController(alert, true, null);
                    });
                }
            }
            else if (parameters.TryGetValue("error", out string? error))
            {
                this.InvokeOnMainThread(() =>
                {
                    var alert = UIAlertController.Create("Error", error, UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    this.PresentViewController(alert, true, null);
                });
            }
        }
    }

    private void OnError(NSError? error)
    {
        this.InvokeOnMainThread(() =>
        {
            var alert = UIAlertController.Create("Error", error!.LocalizedDescription, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        });
    }
}