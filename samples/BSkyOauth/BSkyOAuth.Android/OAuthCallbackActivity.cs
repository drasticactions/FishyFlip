// <copyright file="OAuthCallbackActivity.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Android.Content.PM;

namespace BSkyOAuth;

/// <summary>
/// OAuth Callback Activity.
/// </summary>
[Activity(NoHistory = true, Exported = true, LaunchMode = LaunchMode.SingleTop)]
[IntentFilter(
    [Android.Content.Intent.ActionView],
    Categories = [Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable],
    DataSchemes = ["vip.drasticactions"])]
public class OAuthCallbackActivity : Activity
{
    /// <inheritdoc/>
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var uri = this.Intent?.Data;
        if (uri != null)
        {
            this.HandleCallback(uri);
        }

        this.Finish();
    }

    private void HandleCallback(Android.Net.Uri uri)
    {
        var parameters = uri.Query?.TrimStart('?')
            .Split('&')
            .Select(param => param.Split('='))
            .ToDictionary(split => split[0], split => Uri.UnescapeDataString(split[1])) ?? new Dictionary<string, string>();

        if (parameters.TryGetValue("code", out string code))
        {
            this.HandleAuthorizationCode(code);
        }
        else if (parameters.TryGetValue("error", out string error))
        {
            this.HandleAuthorizationError(error);
        }
    }

    private void HandleAuthorizationCode(string code)
    {
        Console.WriteLine($"Authorization code received: {code}");
    }

    private void HandleAuthorizationError(string error)
    {
        Console.WriteLine($"Authorization error: {error}");
    }
}