// <copyright file="MainWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;

namespace BSkyOAuth;

/// <summary>
/// Main Window.
/// </summary>
public sealed class MainWindow : NSWindow
{
    private const string ClientMetadataUrl = "https://drasticactions.vip/client-metadata.json";

    private const string RedirectUri = "vip.drasticactions:/callback";

    private readonly OAuthManager oauthManager;

    private ATProtocol atProtocol;

    private NSTextField handleField;

    private NSButton authButton;


    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
        : base(new CGRect(0, 0, 400, 300), NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled, NSBackingStore.Buffered, false)
    {
        this.Title = "BSkyOAuth";
        this.oauthManager = new OAuthManager(this, "vip.drasticactions", this.OnSuccess, this.OnError);
        var atProtocolBuilder = new ATProtocolBuilder();
        this.atProtocol = atProtocolBuilder.Build();

        this.handleField = new NSTextField(new CGRect(10, 10, 380, 30))
        {
            PlaceholderString = "Handle",
        };

        this.authButton = new NSButton(new CGRect(10, 50, 380, 30))
        {
            Title = "Authenticate",
        };

        this.authButton.Activated += this.AuthButton_TouchUpInside;
        var view = new NSView();
        this.ContentView = view;
        view.AddSubview(this.handleField);
        view.AddSubview(this.authButton);

        this.handleField.TranslatesAutoresizingMaskIntoConstraints = false;
        this.authButton.TranslatesAutoresizingMaskIntoConstraints = false;
        this.handleField.TopAnchor.ConstraintEqualTo(view.TopAnchor, 10).Active = true;
        this.handleField.LeadingAnchor.ConstraintEqualTo(view.LeadingAnchor, 10).Active = true;
        this.handleField.TrailingAnchor.ConstraintEqualTo(view.TrailingAnchor, -10).Active = true;
        this.handleField.HeightAnchor.ConstraintEqualTo(30).Active = true;
        this.authButton.TopAnchor.ConstraintEqualTo(this.handleField.BottomAnchor, 10).Active = true;
        this.authButton.LeadingAnchor.ConstraintEqualTo(view.LeadingAnchor, 10).Active = true;
        this.authButton.TrailingAnchor.ConstraintEqualTo(view.TrailingAnchor, -10).Active = true;
        this.authButton.HeightAnchor.ConstraintEqualTo(30).Active = true;
    }

    private async void AuthButton_TouchUpInside(object? sender, EventArgs e)
    {
        if (!ATIdentifier.TryCreate(this.handleField.StringValue, out ATIdentifier? atIdentifier))
        {
            this.InvokeOnMainThread(() =>
            {
                var alert = new NSAlert
                {
                    AlertStyle = NSAlertStyle.Critical,
                    InformativeText = "Invalid Handle",
                    MessageText = "Invalid Handle",
                };

                alert.RunModal();
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
                var alert = new NSAlert
                {
                    AlertStyle = NSAlertStyle.Critical,
                    InformativeText = error.ToString(),
                    MessageText = "Error",
                };
                alert.RunModal();
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
                        var alert = new NSAlert
                        {
                            AlertStyle = NSAlertStyle.Informational,
                            InformativeText = $"Authenticated as {session.Handle}",
                            MessageText = "Authenticated",
                        };

                        alert.RunModal();
                    });
                }
                else
                {
                    this.InvokeOnMainThread(() =>
                    {
                        var alert = new NSAlert
                        {
                            AlertStyle = NSAlertStyle.Critical,
                            InformativeText = error?.ToString() ?? string.Empty,
                            MessageText = "Error",
                        };
                        alert.RunModal();
                    });
                }
            }
            else if (parameters.TryGetValue("error", out string? error))
            {
                this.InvokeOnMainThread(() =>
                {
                    var alert = new NSAlert
                    {
                        AlertStyle = NSAlertStyle.Critical,
                        InformativeText = error,
                        MessageText = "Error",
                    };
                    alert.RunModal();
                });
            }
        }
    }

    private void OnError(NSError? error)
    {
        this.InvokeOnMainThread(() =>
        {
            var alert = new NSAlert
            {
                AlertStyle = NSAlertStyle.Critical,
                InformativeText = error?.ToString() ?? string.Empty,
                MessageText = "Error",
            };
            alert.RunModal();
        });
    }
}
