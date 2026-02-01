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

    private UIButton refreshTokenButton;

    private UIButton getTimelineButton;

    private UIButton saveSessionButton;

    private UIButton loadSessionButton;

    private UITextField handleField;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginViewController"/> class.
    /// </summary>
    public LoginViewController()
    {
        this.oauthManager = new OAuthManager(this, "vip.drasticactions", this.OnSuccess, this.OnError);
        var atProtocolBuilder = new ATProtocolBuilder();
        this.atProtocol = atProtocolBuilder.Build();
        this.atProtocol.SessionUpdated += (sender, args) =>
        {
            Console.WriteLine($"Session updated: {args.Session.ToString()}");
        };

        this.View!.BackgroundColor = UIColor.SystemBackground;

        this.handleField = new UITextField();
        this.handleField.Placeholder = "Handle";
        this.handleField.TranslatesAutoresizingMaskIntoConstraints = false;
        this.View!.AddSubview(this.handleField);

        this.authButton = new UIButton(UIButtonType.System);
        this.authButton.SetTitle("Authenticate", UIControlState.Normal);
        this.authButton.TranslatesAutoresizingMaskIntoConstraints = false;
        this.authButton.TouchUpInside += this.AuthButton_TouchUpInside;
        this.View!.AddSubview(this.authButton);

        this.refreshTokenButton = new UIButton(UIButtonType.System);
        this.refreshTokenButton.SetTitle("Refresh Token", UIControlState.Normal);
        this.refreshTokenButton.TranslatesAutoresizingMaskIntoConstraints = false;
        this.refreshTokenButton.Enabled = false;
        this.refreshTokenButton.TouchUpInside += this.RefreshTokenButton_TouchUpInside;
        this.View!.AddSubview(this.refreshTokenButton);

        this.getTimelineButton = new UIButton(UIButtonType.System);
        this.getTimelineButton.SetTitle("Get Timeline", UIControlState.Normal);
        this.getTimelineButton.TranslatesAutoresizingMaskIntoConstraints = false;
        this.getTimelineButton.Enabled = false;
        this.getTimelineButton.TouchUpInside += this.GetTimelineButton_TouchUpInside;
        this.View!.AddSubview(this.getTimelineButton);

        this.saveSessionButton = new UIButton(UIButtonType.System);
        this.saveSessionButton.SetTitle("Save Session", UIControlState.Normal);
        this.saveSessionButton.TranslatesAutoresizingMaskIntoConstraints = false;
        this.saveSessionButton.Enabled = false;
        this.saveSessionButton.TouchUpInside += this.SaveSessionButton_TouchUpInside;
        this.View!.AddSubview(this.saveSessionButton);

        this.loadSessionButton = new UIButton(UIButtonType.System);
        this.loadSessionButton.SetTitle("Load Session", UIControlState.Normal);
        this.loadSessionButton.TranslatesAutoresizingMaskIntoConstraints = false;
        this.loadSessionButton.TouchUpInside += this.LoadSessionButton_TouchUpInside;
        this.View!.AddSubview(this.loadSessionButton);

        this.View!.AddConstraints(new[]
        {
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Top, 1, 100),
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.handleField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.handleField, NSLayoutAttribute.Bottom, 1, 20),
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.authButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
            NSLayoutConstraint.Create(this.refreshTokenButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.authButton, NSLayoutAttribute.Bottom, 1, 20),
            NSLayoutConstraint.Create(this.refreshTokenButton, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.refreshTokenButton, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.refreshTokenButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
            NSLayoutConstraint.Create(this.getTimelineButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.refreshTokenButton, NSLayoutAttribute.Bottom, 1, 20),
            NSLayoutConstraint.Create(this.getTimelineButton, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.getTimelineButton, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.getTimelineButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
            NSLayoutConstraint.Create(this.saveSessionButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.getTimelineButton, NSLayoutAttribute.Bottom, 1, 20),
            NSLayoutConstraint.Create(this.saveSessionButton, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.saveSessionButton, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.saveSessionButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
            NSLayoutConstraint.Create(this.loadSessionButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.saveSessionButton, NSLayoutAttribute.Bottom, 1, 20),
            NSLayoutConstraint.Create(this.loadSessionButton, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Leading, 1, 20),
            NSLayoutConstraint.Create(this.loadSessionButton, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, this.View!, NSLayoutAttribute.Trailing, 1, -20),
            NSLayoutConstraint.Create(this.loadSessionButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 40),
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
            new[] { "atproto", "transition:generic" },
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
                    this.InvokeOnMainThread(() =>
                    {
                        this.refreshTokenButton.Enabled = true;
                        this.getTimelineButton.Enabled = true;
                        this.saveSessionButton.Enabled = true;
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

    private async void RefreshTokenButton_TouchUpInside(object? sender, EventArgs e)
    {
        var (refreshedSession, refreshError) = await this.atProtocol.RefreshAuthSessionResultAsync();
        if (refreshError != null)
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", $"Token refresh failed: {refreshError}", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        this.InvokeOnMainThread(() =>
        {
            var alert = UIAlertController.Create("Success", "OAuth token refreshed.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        });
    }

    private async void GetTimelineButton_TouchUpInside(object? sender, EventArgs e)
    {
        var (timeline, timelineError) = await this.atProtocol.Feed.GetTimelineAsync(limit: 1);
        if (timelineError != null)
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", $"GetTimeline failed: {timelineError}", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        var postCount = timeline?.Feed?.Count ?? 0;
        this.InvokeOnMainThread(() =>
        {
            var alert = UIAlertController.Create("Success", $"Timeline returned {postCount} post(s).", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        });
    }

    private void SaveSessionButton_TouchUpInside(object? sender, EventArgs e)
    {
        var oauthSession = this.atProtocol.OAuthSession;
        if (oauthSession == null)
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", "No active OAuth session to save.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        var json = oauthSession.ToString();
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
        File.WriteAllText(path, json);

        this.InvokeOnMainThread(() =>
        {
            var alert = UIAlertController.Create("Success", $"Session saved to {path}", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        });
    }

    private async void LoadSessionButton_TouchUpInside(object? sender, EventArgs e)
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
        if (!File.Exists(path))
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", "No saved session file found.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        var json = File.ReadAllText(path);
        var authSession = AuthSession.FromString(json);
        if (authSession == null)
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", "Failed to deserialize session.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        var (session, error) = await this.atProtocol.AuthenticateWithOAuth2SessionResultAsync(authSession, ClientMetadataUrl);
        if (error != null)
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = UIAlertController.Create("Error", $"Failed to restore session: {error}", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
            });

            return;
        }

        this.InvokeOnMainThread(() =>
        {
            this.refreshTokenButton.Enabled = true;
            this.getTimelineButton.Enabled = true;
            this.saveSessionButton.Enabled = true;
            var alert = UIAlertController.Create("Success", $"Session loaded. Authenticated as {session?.Handle}", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(alert, true, null);
        });
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