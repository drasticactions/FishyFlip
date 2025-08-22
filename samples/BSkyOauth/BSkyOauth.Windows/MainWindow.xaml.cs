// <copyright file="MainWindow.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics;
using FishyFlip;
using FishyFlip.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace BSkyOAuth
{
    /// <summary>
    /// Main Window of the app.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private const string ClientMetadataUrl = "https://drasticactions.vip/client-metadata.json";

        private const string RedirectUri = "vip.drasticactions:/callback";

        private ATProtocol protocol;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.SystemBackdrop = new MicaBackdrop();
            var atprotocolBuilder = new ATProtocolBuilder();
            this.protocol = atprotocolBuilder.Build();
        }

        /// <summary>
        /// Handles OAuth Call.
        /// </summary>
        /// <param name="uri">Callback URI.</param>
        /// <returns>Result.</returns>
        public async Task HandleOAuthCallAsync(Uri uri)
        {
            var (result, error) = await this.protocol.AuthenticateWithOAuth2CallbackResultAsync(uri.ToString());
            if (result is not null)
            {
                this.myButton.Content = $"Authenticated as {result.Handle}";
            }
            else
            {
                this.myButton.Content = "Failed to authenticate!";
            }
        }

        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ATIdentifier.TryCreate(this.myTextBox.Text, out ATIdentifier? atIdentifier))
            {
                var dialog = new ContentDialog
                {
                    Title = "Invalid Handle",
                    Content = "Invalid Handle",
                    PrimaryButtonText = "OK",
                };

                dialog.XamlRoot = this.Content.XamlRoot;

                await dialog.ShowAsync();
                return;
            }

            var (uri, error) = await this.protocol.GenerateOAuth2AuthenticationUrlResultAsync(
            ClientMetadataUrl,
            RedirectUri,
            new[] { "atproto" },
            atIdentifier!);

            if (uri is not null)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = uri,
                    UseShellExecute = true,
                });
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "Failed to generate URL",
                    Content = "Failed to generate URL",
                    PrimaryButtonText = "OK",
                };

                dialog.XamlRoot = this.Content.XamlRoot;

                await dialog.ShowAsync();
            }
        }
    }
}
