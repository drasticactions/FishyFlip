// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using ConsoleAppFramework;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;
using OAuth;

var app = ConsoleApp.Create();
app.Add<OAuthCommands>();
app.Run(args);

/// <summary>
/// OAuth Commands.
/// </summary>
#pragma warning disable SA1649 // File name should match first type name
public class OAuthCommands
#pragma warning restore SA1649 // File name should match first type name
{
    /// <summary>
    /// Refresh session from saved json file.
    /// </summary>
    /// <param name="outputName">-o, Output Name.</param>
    /// <param name="instanceUrl">-i, Instance URL.</param>
    /// <param name="verbose">-v, Verbose logging.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    [Command("refresh")]
    public async Task RefreshSessionAsync(string outputName = "session.json", string instanceUrl = "https://bsky.social", bool verbose = false, CancellationToken cancellationToken = default)
    {
        var consoleLog = new ConsoleLog(verbose);
        if (!File.Exists(outputName))
        {
            consoleLog.LogError("Session file does not exist");
            return;
        }

        var protocol = this.GenerateProtocol(new Uri(instanceUrl));
        var sessionJson = File.ReadAllText(outputName);
        var oauthSession = AuthSession.FromString(sessionJson);
        if (oauthSession is null)
        {
            consoleLog.LogError("Failed to read session");
            return;
        }

        consoleLog.Log($"Starting OAuth2 Refresh");
        var (session, _) = await protocol.AuthenticateWithOAuth2SessionResultAsync(oauthSession, "http://localhost");
        if (session is null)
        {
            consoleLog.LogError("Failed to refresh session, session is null");
            return;
        }

        consoleLog.Log($"Refreshed session");
        consoleLog.LogDebug($"Did: {session.Did}");
        consoleLog.LogDebug($"Access Token: {session.AccessJwt}");
        consoleLog.LogDebug($"Refresh Token: {session.RefreshJwt}");

        var savedSession = await protocol.RefreshAuthSessionResultAsync();
        if (savedSession is null)
        {
            consoleLog.LogError("OAuth Session is null, failed to save session");
            return;
        }

        consoleLog.LogDebug($"OAuth Session: {savedSession}");
        File.WriteAllText(outputName, savedSession.ToString());
        consoleLog.Log($"Session saved to {outputName}");
    }

    /// <summary>
    /// Start new headless session.
    /// This is done by generating a OAuth URL that you can copy and paste into a browser.
    /// Then, once you complete the login, you copy the URL you are redirected to and paste it back.
    /// This will generate a session.json file that you can use to authenticate with.
    /// </summary>
    /// <param name="clientId">-c, Client ID.</param>
    /// <param name="instanceUrl">-i, Instance URL.</param>
    /// <param name="scopes">-s, Scopes.</param>
    /// <param name="outputName">-o, Output Name.</param>
    /// <param name="verbose">-v, Verbose logging.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    [Command("start headless")]
    public async Task StartHeadlessAsync(string clientId = "http://localhost", string instanceUrl = "https://bsky.social", string scopes = "atproto", string outputName = "session.json", bool verbose = false, CancellationToken cancellationToken = default)
    {
        var consoleLog = new ConsoleLog(verbose);
        Uri.TryCreate(instanceUrl, UriKind.Absolute, out var iUrl);

        // If outputName does not end in ".json", add it.
        if (!outputName.EndsWith(".json"))
        {
            outputName += ".json";
        }

        if (iUrl is null)
        {
            consoleLog.LogError("Invalid instance URL");
            return;
        }

        if (string.IsNullOrEmpty(clientId))
        {
            consoleLog.LogError("Invalid Client ID");
            return;
        }

        var scopeList = scopes.Split(',').Select(n => n.Trim()).ToArray();
        if (scopeList.Length == 0)
        {
            consoleLog.LogError("Invalid Scopes");
            return;
        }

        var protocol = this.GenerateProtocol(iUrl);
        consoleLog.Log($"Starting OAuth2 Authentication for {instanceUrl}");
        var (url, _) = await protocol.GenerateOAuth2AuthenticationUrlResultAsync(clientId, "http://127.0.0.1", scopeList, instanceUrl.ToString(), cancellationToken);
        consoleLog.Log($"Login URL: {url}");
        consoleLog.Log("Please login and copy the URL of the page you are redirected to.");
        var redirectUrl = Console.ReadLine();
        if (string.IsNullOrEmpty(redirectUrl))
        {
            consoleLog.LogError("Invalid redirect URL");
            return;
        }

        consoleLog.Log($"Got redirect url, finishing OAuth2 Authentication on {instanceUrl}");
        var (session, _) = await protocol.AuthenticateWithOAuth2CallbackResultAsync(redirectUrl, cancellationToken);

        if (session is null)
        {
            consoleLog.LogError("Failed to authenticate, session is null");
            return;
        }

        consoleLog.Log($"Authenticated as {session.Did}");

        consoleLog.LogDebug($"Did: {session.Did}");
        consoleLog.LogDebug($"Access Token: {session.AccessJwt}");
        consoleLog.LogDebug($"Refresh Token: {session.RefreshJwt}");

        var savedSession = await protocol.RefreshAuthSessionResultAsync();

        if (savedSession is null)
        {
            consoleLog.LogError("OAuth Session is null");
            return;
        }

        consoleLog.LogDebug($"OAuth Session: {savedSession}");
        await File.WriteAllTextAsync(outputName, savedSession.ToString(), cancellationToken);
        consoleLog.Log($"Session saved to {outputName}");
    }

    /// <summary>
    /// Start new session.
    /// </summary>
    /// <param name="clientId">-c, Client ID.</param>
    /// <param name="redirectUrl">-r, Redirect URL.</param>
    /// <param name="identity">-id, Identity.</param>
    /// <param name="instanceUrl">-i, Instance URL.</param>
    /// <param name="port">-p, Port.</param>
    /// <param name="scopes">-s, Scopes.</param>
    /// <param name="outputName">-o, Output Name.</param>
    /// <param name="verbose">-v, Verbose logging.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    [Command("start")]
    public async Task StartSessionAsync(string clientId = "http://localhost", string? redirectUrl = default, string? identity = default, string instanceUrl = "https://bsky.social", int port = 0, string scopes = "atproto", string outputName = "session.json", bool verbose = false, CancellationToken cancellationToken = default)
    {
        var consoleLog = new ConsoleLog(verbose);
        Uri.TryCreate(instanceUrl, UriKind.Absolute, out var iUrl);

        // If outputName does not end in ".json", add it.
        if (!outputName.EndsWith(".json"))
        {
            outputName += ".json";
        }

        ATIdentifier? identityId = null;

        if (!string.IsNullOrEmpty(identity))
        {
            identityId = ATIdentifier.Create(identity);
            if (identityId is null)
            {
                consoleLog.LogError("Invalid Identity");
                return;
            }
        }

        if (iUrl is null)
        {
            consoleLog.LogError("Invalid instance URL");
            return;
        }

        if (string.IsNullOrEmpty(clientId))
        {
            consoleLog.LogError("Invalid Client ID");
            return;
        }

        var browser = new SystemBrowser(port);
        if (string.IsNullOrEmpty(redirectUrl))
        {
            redirectUrl = $"http://127.0.0.1:{browser.Port}/";
        }

        var scopeList = scopes.Split(',');
        if (scopeList.Length == 0)
        {
            consoleLog.LogError("Invalid Scopes");
            return;
        }

        var protocol = this.GenerateProtocol(iUrl);
        consoleLog.Log($"Starting OAuth2 Authentication for {instanceUrl}");

        string? url;
        if (identityId is not null)
        {
            consoleLog.Log($"Using Identity: {identityId}");
            (url, var error) = await protocol.GenerateOAuth2AuthenticationUrlResultAsync(clientId, redirectUrl, scopeList, identityId, cancellationToken);
            if (error is not null)
            {
                consoleLog.LogError(error.ToString());
                return;
            }
        }
        else
        {
            (url, var error2) = await protocol.GenerateOAuth2AuthenticationUrlResultAsync(clientId, redirectUrl, scopeList, instanceUrl, cancellationToken);
            if (error2 is not null)
            {
                consoleLog.LogError(error2.ToString());
                return;
            }
        }

        if (url is null)
        {
            consoleLog.LogError("Failed to generate OAuth2 URL");
            return;
        }

        var result = await browser.InvokeAsync(url, cancellationToken);
        if (result.IsError)
        {
            consoleLog.LogError(result.Error);
            return;
        }

        consoleLog.Log($"Got session, finishing OAuth2 Authentication on {instanceUrl}");

        var (session, _) = await protocol.AuthenticateWithOAuth2CallbackResultAsync($"{redirectUrl}{result.Response}", cancellationToken);
        if (session is null)
        {
            consoleLog.LogError("Failed to authenticate, session is null");
            return;
        }

        consoleLog.Log($"Authenticated as {session.Did}");
        consoleLog.LogDebug($"Did: {session.Did}");
        consoleLog.LogDebug($"Access Token: {session.AccessJwt}");
        consoleLog.LogDebug($"Refresh Token: {session.RefreshJwt}");

        var savedSession = await protocol.RefreshAuthSessionResultAsync();
        if (savedSession is null)
        {
            consoleLog.LogError("OAuth Session is null");
            return;
        }

        consoleLog.LogDebug($"OAuth Session: {savedSession}");
        await File.WriteAllTextAsync(outputName, savedSession.ToString(), cancellationToken);
        consoleLog.Log($"Session saved to {outputName}");
    }

    private ATProtocol GenerateProtocol(Uri instanceUrl)
    {
        var debugLogger = new DebugLoggerProvider();
        var builder = new ATProtocolBuilder();
        builder.WithLogger(debugLogger.CreateLogger("ATProtocol"));
        builder.WithInstanceUrl(instanceUrl);
        return builder.Build();
    }
}