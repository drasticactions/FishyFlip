// <copyright file="MainActivity.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using AndroidX.Browser.CustomTabs;
using FishyFlip;
using FishyFlip.Models;

namespace BSkyOAuth;

/// <summary>
/// Main Activity.
/// </summary>
[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    private const string ClientMetadataUrl = "https://drasticactions.vip/client-metadata.json";

    private const string RedirectUri = "vip.drasticactions:/callback";

    private ATProtocol? protocol;

    private CustomTabsIntent? customTabsIntent;

    /// <inheritdoc/>
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var builder = new CustomTabsIntent.Builder()
            .SetShowTitle(true)
            .SetUrlBarHidingEnabled(true);
        this.customTabsIntent = builder.Build();

        var atProtocolBuilder = new ATProtocolBuilder();
        this.protocol = atProtocolBuilder.Build();

        this.SetContentView(Resource.Layout.activity_main);

        var authButton = FindViewById<Button>(Resource.Id.authButton)!;
        var editText = FindViewById<EditText>(Resource.Id.handleEditor)!;

        authButton.Click += async (sender, e) =>
        {
            Task.Run(async () =>
            {
                if (!ATIdentifier.TryCreate(editText.Text ?? string.Empty, out var identifier))
                {
                    this.RunOnUiThread(() =>
                    {
                        var builder = new AlertDialog.Builder(this);
                        builder.SetMessage("Invalid identifier.");
                        builder.SetPositiveButton("OK", (sender, e) => { });
                        builder.Create()!.Show();
                    });
                    return;
                }

                var (uri, error) = await this.protocol!.GenerateOAuth2AuthenticationUrlResultAsync(ClientMetadataUrl, RedirectUri, new [] { "atproto" }, identifier!);
                if (error != null)
                {
                    this.RunOnUiThread(() =>
                    {
                        var builder = new AlertDialog.Builder(this);
                        builder.SetMessage(error.ToString());
                        builder.SetPositiveButton("OK", (sender, e) => { });
                        builder.Create()!.Show();
                    });
                    return;
                }

                this.customTabsIntent!.LaunchUrl(this, Android.Net.Uri.Parse(uri)!);
            });
        };
    }
}