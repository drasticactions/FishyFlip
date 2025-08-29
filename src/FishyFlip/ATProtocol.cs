// <copyright file="ATProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.App.Bsky.Actor;
using FishyFlip.Lexicon.Com.Atproto.Identity;
using Microsoft.Extensions.Logging.Abstractions;

namespace FishyFlip;

/// <summary>
/// AT Protocol.
/// https://atproto.com/specs/atp.
/// </summary>
public sealed partial class ATProtocol : IDisposable
{
    private ATProtocolOptions options;
    private bool disposedValue;
    private ISessionManager sessionManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocol"/> class.
    /// </summary>
    /// <param name="options">Configuration options for ATProto. <see cref="ATProtocolOptions"/>.</param>
    public ATProtocol(ATProtocolOptions options)
    {
        this.options = options;
        this.sessionManager = new UnauthenticatedSessionManager(this, options);
    }

    /// <summary>
    /// Fires when a session is updated.
    /// </summary>
    public event EventHandler<SessionUpdatedEventArgs>? SessionUpdated;

    /// <summary>
    /// Gets a value indicating whether the user is authenticated.
    /// </summary>
    public bool IsAuthenticated => this.sessionManager.IsAuthenticated;

    /// <summary>
    /// Gets the current ATProto Session. Null if no session is active.
    /// </summary>
    public Session? Session => this.sessionManager?.Session;

    /// <summary>
    /// Gets the ATProtocol Options.
    /// </summary>
    public ATProtocolOptions Options => this.options;

    /// <summary>
    /// Gets the base address for the underlying HttpClient.
    /// </summary>
    public Uri? BaseAddress => this.sessionManager.Client.BaseAddress;

    /// <summary>
    /// Gets the HttpClient.
    /// </summary>
    public HttpClient Client => this.sessionManager.Client;

    /// <summary>
    /// Gets the current OAuth session, if any is active.
    /// </summary>
    public AuthSession? OAuthSession => this.sessionManager is OAuth2SessionManager oAuth2SessionManager
        ? oAuth2SessionManager.OAuthSession
        : null;

    /// <summary>
    /// Gets the current PasswordSession session, if any is active.
    /// </summary>
    public AuthSession? PasswordSession => this.sessionManager is PasswordSessionManager passwordSessionManager
        ? passwordSessionManager.PasswordSession
        : null;

    /// <summary>
    /// Gets the PclDirectory Methods.
    /// </summary>
    public PlcDirectory PlcDirectory => new(this);

    /// <summary>
    /// Gets the OpenGraphParser.
    /// Can be used to generate embeds from URLs.
    /// </summary>
    public OpenGraphParser OpenGraphParser => new(this);

    /// <summary>
    /// Gets the current AuthSession.
    /// </summary>
    public AuthSession? AuthSession => this.sessionManager switch
    {
        PasswordSessionManager passwordSessionManager => passwordSessionManager.PasswordSession,
        OAuth2SessionManager oAuth2SessionManager => oAuth2SessionManager.OAuthSession,
        _ => null,
    };

    /// <summary>
    /// Gets the session manager.
    /// </summary>
    public ISessionManager SessionManager
    {
        get => this.sessionManager;

        internal set
        {
            this.sessionManager.SessionUpdated -= this.OnSessionUpdated;
            this.sessionManager.Dispose();

            this.sessionManager = value;

            this.sessionManager.SessionUpdated += this.OnSessionUpdated;
        }
    }

    /// <summary>
    /// Asynchronously creates a new session manager using a password.
    /// </summary>
    /// <param name="identifier">The identifier of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="authFactorToken">2-Factor Auth Token, optional.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    public async Task<Result<Session?>> AuthenticateWithPasswordResultAsync(string identifier, string password, string? authFactorToken = default, CancellationToken cancellationToken = default)
    {
        var passwordSessionManager = new PasswordSessionManager(this);
        this.SessionManager = passwordSessionManager;

        return await passwordSessionManager.CreateSessionAsync(identifier, password, authFactorToken, cancellationToken);
    }

    /// <summary>
    /// Asynchronously creates a new session manager using a password.
    /// </summary>
    /// <param name="identifier">The identifier of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    [Obsolete("Use AuthenticateWithPasswordResultAsync instead.")]
    public async Task<Session?> AuthenticateWithPasswordAsync(string identifier, string password, CancellationToken cancellationToken = default)
    {
        // The error is logged in the AuthenticateWithPasswordResultAsync method.
        var (result, _) = await this.AuthenticateWithPasswordResultAsync(identifier, password, cancellationToken: cancellationToken);
        return result;
    }

    /// <summary>
    /// Starts the OAuth2 authentication process asynchronously.
    /// </summary>
    /// <param name="clientId">ClientID, must be a URL.</param>
    /// <param name="redirectUrl">RedirectUrl.</param>
    /// <param name="scopes">ATProtocol Scopes.</param>
    /// <param name="identifier">ATIdentifier, used for LoginHint and InstanceUrl.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Authorization URL to call.</returns>
    public async Task<Result<string?>> GenerateOAuth2AuthenticationUrlResultAsync(string clientId, string redirectUrl, IEnumerable<string> scopes, ATIdentifier identifier, CancellationToken cancellationToken = default)
    {
        var (hostUrl, error) = await this.ResolveATIdentifierToHostAddressAsync(identifier, cancellationToken);

        if (error is not null)
        {
            return error;
        }

        var uri = new Uri(hostUrl!);

        // If the uri contains bsky.network, we need to use the bsky.social instance.
        var instanceUrl = uri.Host.Contains("bsky.network") ? Constants.Urls.ATProtoServer.SocialApi : uri.ToString();

        var oAuth2SessionManager = new OAuth2SessionManager(this);
        this.SessionManager = oAuth2SessionManager;
        return await oAuth2SessionManager.StartAuthorizationAsync(clientId, redirectUrl, scopes, identifier.ToString(), instanceUrl, null, cancellationToken);
    }

    /// <summary>
    /// Starts the OAuth2 authentication process asynchronously.
    /// </summary>
    /// <param name="clientId">ClientID, must be a URL.</param>
    /// <param name="redirectUrl">RedirectUrl.</param>
    /// <param name="scopes">ATProtocol Scopes.</param>
    /// <param name="instanceUrl">InstanceUrl, must be a URL. If null, uses https://bsky.social.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Authorization URL to call.</returns>
    public async Task<Result<string?>> GenerateOAuth2AuthenticationUrlResultAsync(string clientId, string redirectUrl, IEnumerable<string> scopes, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        var oAuth2SessionManager = new OAuth2SessionManager(this);
        this.SessionManager = oAuth2SessionManager;
        return await oAuth2SessionManager.StartAuthorizationAsync(clientId, redirectUrl, scopes, null, instanceUrl, null, cancellationToken);
    }

    /// <summary>
    /// Starts the OAuth2 authentication process asynchronously.
    /// </summary>
    /// <param name="clientId">ClientID, must be a URL.</param>
    /// <param name="redirectUrl">RedirectUrl.</param>
    /// <param name="scopes">ATProtocol Scopes.</param>
    /// <param name="instanceUrl">InstanceUrl, must be a URL. If null, uses https://bsky.social.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Authorization URL to call.</returns>
    [Obsolete("Use GenerateOAuth2AuthenticationUrlResultAsync instead.")]
    public async Task<string> GenerateOAuth2AuthenticationUrlAsync(string clientId, string redirectUrl, IEnumerable<string> scopes, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        return (await this.GenerateOAuth2AuthenticationUrlResultAsync(clientId, redirectUrl, scopes, instanceUrl, cancellationToken)).HandleResult()!;
    }

    /// <summary>
    /// Authenticates with OAuth2 callback asynchronously.
    /// </summary>
    /// <param name="callbackData">The callback data received from the OAuth2 provider.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    public async Task<Result<Session?>> AuthenticateWithOAuth2CallbackResultAsync(string callbackData, CancellationToken cancellationToken = default)
    {
        if (this.SessionManager is not OAuth2SessionManager oAuth2SessionManager)
        {
            return new ATError(new OAuth2Exception("Session manager is not an OAuth2 session manager."));
        }

        return await oAuth2SessionManager.CompleteAuthorizationAsync(callbackData, cancellationToken);
    }

    /// <summary>
    /// Authenticates with OAuth2 callback asynchronously.
    /// </summary>
    /// <param name="callbackData">The callback data received from the OAuth2 provider.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    [Obsolete("Use AuthenticateWithOAuth2CallbackResultAsync instead.")]
    public async Task<Session?> AuthenticateWithOAuth2CallbackAsync(string callbackData, CancellationToken cancellationToken = default)
    {
        return (await this.AuthenticateWithOAuth2CallbackResultAsync(callbackData, cancellationToken)).HandleResult();
    }

    /// <summary>
    /// Authenticates with password session asynchronously.
    /// </summary>
    /// <param name="session">The password session.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    /// <exception cref="OAuth2Exception">Thrown if ProofKey was included in AuthSession.</exception>
    public async Task<Result<Session?>> AuthenticateWithPasswordSessionResultAsync(AuthSession session)
    {
        if (!string.IsNullOrEmpty(session.ProofKey))
        {
            return new ATError(new OAuth2Exception("Proof key is not required for password sessions. Is this an OAuth2 session?"));
        }

        var passwordSessionManager = new PasswordSessionManager(this, session.Session);
        this.SessionManager = passwordSessionManager;
        return await Task.FromResult<Session?>(passwordSessionManager.Session);
    }

    /// <summary>
    /// Authenticates with password session asynchronously.
    /// </summary>
    /// <param name="session">The password session.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    /// <exception cref="OAuth2Exception">Thrown if ProofKey was included in AuthSession.</exception>
    [Obsolete("Use AuthenticateWithPasswordSessionResultAsync instead.")]
    public async Task<Session?> AuthenticateWithPasswordSessionAsync(AuthSession session)
    {
        return (await this.AuthenticateWithPasswordSessionResultAsync(session)).HandleResult();
    }

    /// <summary>
    /// Authenticates with OAuth2 session asynchronously.
    /// </summary>
    /// <param name="session">The OAuth session.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="instanceUrl">Optional. The instance URL. If null, uses https://bsky.social.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    public async Task<Result<Session?>> AuthenticateWithOAuth2SessionResultAsync(AuthSession session, string clientId, string? instanceUrl = default)
    {
        var oAuth2SessionManager = new OAuth2SessionManager(this);
        this.SessionManager = oAuth2SessionManager;
        if (string.IsNullOrEmpty(session.ProofKey))
        {
            return new ATError(new OAuth2Exception("Proof key is required for OAuth2 sessions."));
        }

        var (session2, error2) = await oAuth2SessionManager.StartSessionAsync(session, clientId, instanceUrl);
        if (error2 is not null)
        {
            return error2;
        }

        return session2!.Session;
    }

    /// <summary>
    /// Authenticates with OAuth2 session asynchronously.
    /// </summary>
    /// <param name="session">The OAuth session.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="instanceUrl">Optional. The instance URL. If null, uses https://bsky.social.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    [Obsolete("Use AuthenticateWithOAuth2SessionResultAsync instead.")]
    public async Task<Session?> AuthenticateWithOAuth2SessionAsync(AuthSession session, string clientId, string? instanceUrl = default)
    {
        return (await this.AuthenticateWithOAuth2SessionResultAsync(session, clientId, instanceUrl)).HandleResult();
    }

    /// <summary>
    /// Refreshes the current session asynchronously.
    /// </summary>
    /// <returns><see cref="AuthSession"/>.</returns>
    [Obsolete("Use RefreshAuthSessionResultAsync instead.")]
    public async Task<AuthSession?> RefreshAuthSessionAsync()
        => (await this.RefreshAuthSessionResultAsync()).HandleResult();

    /// <summary>
    /// Refreshes the current session asynchronously.
    /// </summary>
    /// <returns><see cref="AuthSession"/>.</returns>
    public async Task<Result<AuthSession?>> RefreshAuthSessionResultAsync()
    {
        switch (this.sessionManager)
        {
            case OAuth2SessionManager oAuth2SessionManager:
                // Refresh the token to make sure it's the most up to date.
                var (resultOauth, errorOauth) = await oAuth2SessionManager.RefreshSessionAsync();
                if (errorOauth is not null)
                {
                    return errorOauth;
                }

                return oAuth2SessionManager.OAuthSession;
            case PasswordSessionManager { Session: not null } passwordManager:
                // The information from RefreshSessionOutput is set in passwordManager.Session,
                // so we can return the session from password manager, and only worry about
                // checking for the error.
                var (_, error) = await passwordManager.RefreshSessionAsync();
                if (error is not null)
                {
                    return error;
                }

                return new AuthSession(passwordManager.Session);
            default:
                // If you don't have a session manager, you can't refresh the session.
                // So return null.
                return (AuthSession?)null;
        }
    }

    /// <summary>
    /// Calls GetPreferencesAsync to fetch the user's preferences.
    /// Then gets the labels from the user, and creates a list of LabelParameters.
    /// That are then added to the ATProtocol Options.
    /// </summary>
    /// <returns>List of LabelParameter.</returns>
    public async Task<Result<List<LabelParameter>?>> SetDefaultLabelsAsync()
    {
        var (preferences, error) = await this.Actor.GetPreferencesAsync();
        if (error is not null)
        {
            return error;
        }

        var labels = preferences?.Preferences?.FirstOrDefault(x => x.Type == "app.bsky.actor.defs#labelersPref") as LabelersPref;
        if (labels?.Labelers is null)
        {
            return new List<LabelParameter>();
        }

        foreach (var label in labels.Labelers)
        {
            this.options.LabelParameters.Add(new LabelParameter(label.Did!));
        }

        return this.options.LabelParameters.ToList();
    }

    /// <summary>
    /// Resolves an ATIdentifier to their ATDid and ATHandle..
    /// </summary>
    /// <param name="identifier"><see cref="ATIdentifier"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>String of Host URI if it could be resolved, null if it could not.</returns>
    public async Task<Result<(ATDid Did, ATHandle Handle)>> ResolveATIdentifierAsync(ATIdentifier identifier, CancellationToken? token = default)
    {
        if (identifier is ATHandle handle)
        {
            var dnsHandle = await this.ResolveATHandleViaDNSAsync(handle, token ?? CancellationToken.None);
            if (dnsHandle is not null)
            {
                this.options.Logger?.LogDebug($"Resolved handle from DNS: {handle} to {dnsHandle}");
                return (dnsHandle, handle);
            }

            var httpsHandle = await this.ResolveATHandleViaHTTPSAsync(handle, token ?? CancellationToken.None);
            if (httpsHandle is not null)
            {
                this.options.Logger?.LogDebug($"Resolved handle from HTTPS: {handle} to {httpsHandle}");
                return (httpsHandle, handle);
            }

            var (resolveHandle, resolveError) = await this.Identity.ResolveHandleAsync(handle, token ?? CancellationToken.None);
            if (resolveError is not null)
            {
                return resolveError;
            }

            if (resolveHandle?.Did is null)
            {
                return new ATError(new Exception($"Failed to resolve Handle: {handle}. Missing DID."));
            }

            return (resolveHandle.Did, handle);
        }
        else if (identifier is ATDid did)
        {
            var (didDoc, error) = await this.GetDidDocAsync(did, token ?? CancellationToken.None);
            if (error is not null)
            {
                return error;
            }

            return (did, didDoc?.GetHandle()!);
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// Resolves an ATIdentifier to a host address.
    /// </summary>
    /// <param name="identifier"><see cref="ATIdentifier"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>String of Host URI if it could be resolved, null if it could not.</returns>
    public async Task<Result<string?>> ResolveATIdentifierToHostAddressAsync(ATIdentifier identifier, CancellationToken? token = default)
    {
        if (identifier is ATHandle handle)
        {
            return await this.ResolveATHandleHostAsync(handle, token);
        }
        else if (identifier is ATDid did)
        {
            return await this.ResolveATDidHostAsync(did, token);
        }

        return string.Empty;
    }

    /// <summary>
    /// Resolves an ATHandle to a host address.
    /// </summary>
    /// <param name="handle"><see cref="ATHandle"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>String of Host URI if it could be resolved, null if it could not.</returns>
    public async Task<Result<string?>> ResolveATHandleHostAsync(ATHandle handle, CancellationToken? token = default)
    {
        if (this.options.DidCache.TryGetValue(handle.ToString(), out var host) && !string.IsNullOrEmpty(host))
        {
            this.options.Logger?.LogDebug($"Resolved handle from cache: {handle} to {host}");
            return host;
        }

        if (this.IsAuthenticated && this.Session?.Handle.ToString() == handle.ToString())
        {
            host = this.Session?.DidDoc?.GetPDSEndpointUrl(this.options.Logger)?.ToString();
            if (!string.IsNullOrEmpty(host))
            {
                this.options.DidCache[handle.ToString()] = host!;
                this.options.Logger?.LogDebug($"Resolved handle: {handle} to {host}, adding to cache.");
                return host;
            }

            this.options.Logger?.LogError($"Failed to resolve Self User handle: {handle}, missing Service Handle.");
        }

        try
        {
            var endpointUrl = $"{Constants.Urls.ATProtoServer.PublicApi}{IdentityEndpoints.ResolveHandle}?handle={handle}";
            var result = await this.Client.GetAsync(endpointUrl, token ?? CancellationToken.None);
            if (result.IsSuccessStatusCode)
            {
                var resolveHandle = JsonSerializer.Deserialize<ResolveHandleOutput>(
                    await result.Content.ReadAsStringAsync(),
                    this.options.SourceGenerationContext.ComAtprotoIdentityResolveHandleOutput);
                if (resolveHandle?.Did is not null)
                {
                    (host, var error) = await this.ResolveATDidHostAsync(resolveHandle.Did, token);
                    if (!string.IsNullOrEmpty(host))
                    {
                        this.options.DidCache[handle.ToString()] = host!;
                        this.options.Logger?.LogDebug($"Resolved handle: {handle} to {host}, adding to cache.");
                    }
                    else
                    {
                        this.options.Logger?.LogError($"Failed to resolve Handle: {handle}, missing Service Handle.");
                        return error;
                    }
                }
                else
                {
                    this.options.Logger?.LogError($"Failed to resolve Handle: {handle}. Missing DID.");
                }
            }
            else
            {
                var resolveError = JsonSerializer.Deserialize<ATError>(
                    await result.Content.ReadAsStringAsync(),
                    this.options.SourceGenerationContext.ATError);
                if (resolveError is not null)
                {
                    this.options.Logger?.LogError($"Failed to resolve Handle: {handle}. {resolveError}");
                    return resolveError;
                }

                this.options.Logger?.LogError($"Failed to resolve Handle: {handle}. {result.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            this.options.Logger?.LogError($"Failed to resolve Handle: {handle}. {ex.Message}");
        }

        return host;
    }

    /// <summary>
    /// Resolves an ATDid to a host address.
    /// </summary>
    /// <param name="did"><see cref="ATDid"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>String of Host URI if it could be resolved, null if it could not.</returns>
    public async Task<Result<string?>> ResolveATDidHostAsync(ATDid did, CancellationToken? token = default)
    {
        if (this.options.DidCache.TryGetValue(did.ToString(), out var host) && !string.IsNullOrEmpty(host))
        {
            this.options.Logger?.LogDebug($"Resolved DID from cache: {did} to {host}");
            return host;
        }

        try
        {
            switch (did.Type)
            {
                case "plc":
                    (host, var error) = await this.ResolvePlcDidAsync(did, token);
                    if (error is not null)
                    {
                        return error;
                    }

                    break;
                case "web":
                    host = await this.ResolveWebDidAsync(did, token);
                    break;
                default:
                    this.options.Logger?.LogError($"DID type could not be resolved: {did}");
                    break;
            }

            if (!string.IsNullOrEmpty(host))
            {
                this.options.DidCache[did.ToString()] = host!;
                this.options.Logger?.LogDebug($"Resolved DID: {did} to {host}, adding to cache.");
            }
        }
        catch (Exception ex)
        {
            this.options.Logger?.LogError($"Failed to resolve DID: {did}. {ex.Message}");
        }

        return host;
    }

    /// <summary>
    /// Sets the Ozone Proxy Header.
    /// </summary>
    /// <param name="did">The ATDid of the labeler service. This DID should include an #at_labeler that points to the ozone instance to resolve to.</param>
    /// <remarks>Sets the Ozone Proxy Header to the given DID.</remarks>
    public void SetOzoneProxy(ATDid did)
    {
        this.options.OzoneProxyHeader = $"{did}{Constants.AtLabeler}";
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets the DID Document for the given ATIdentifier.
    /// </summary>
    /// <param name="identifier"><see cref="ATIdentifier"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>Result of <see cref="DidDoc"/>.</returns>
    public async Task<Result<DidDoc?>> GetDidDocAsync(ATIdentifier identifier, CancellationToken? token)
    {
        var (did, error) = await this.ResolveATDidAsync(identifier);
        if (error is not null)
        {
            return error;
        }

        if (did is null)
        {
            return new ATError(new Exception($"Failed to resolve ATIdentifier: {identifier}."));
        }

        return await this.GetDidDocAsync(did, token);
    }

    /// <summary>
    /// Gets the DID Document for the given DID.
    /// </summary>
    /// <param name="did"><see cref="ATDid"/>.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>Result of <see cref="DidDoc"/>.</returns>
    public async Task<Result<DidDoc?>> GetDidDocAsync(ATDid did, CancellationToken? token)
    {
        switch (did.Type)
        {
            case "plc":
                return await this.PlcDirectory.GetDidDocAsync(did!, token ?? CancellationToken.None);
            case "web":
                var baseUri = did.ToString().Split(':').Last();
                if (!baseUri.Contains("http"))
                {
                    baseUri = $"https://{baseUri}";
                }

                if (Uri.TryCreate(baseUri, UriKind.Absolute, out var uri))
                {
                    var result = await this.Client.GetAsync($"{uri}{Constants.DidJson}", token ?? CancellationToken.None);
                    if (result.IsSuccessStatusCode)
                    {
                        return JsonSerializer.Deserialize<DidDoc>(await result.Content.ReadAsStringAsync(), this.options.SourceGenerationContext.DidDoc);
                    }

                    this.options.Logger?.LogError($"Failed to resolve web DID: {did}.");
                }
                else
                {
                    this.options.Logger?.LogError($"Failed to resolve web DID: {did}.");
                }

                break;
            default:
                this.options.Logger?.LogError($"DID type could not be resolved: {did}");
                break;
        }

        return new ATError(new Exception($"DID type could not be resolved: {did}"));
    }

    /// <summary>
    /// Resolves an ATHandle to an ATDid via DNS.
    /// </summary>
    /// <param name="handle">The ATHandle to resolve.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns><see cref="ATDid"/>.</returns>
    public async Task<ATDid?> ResolveATHandleViaDNSAsync(ATHandle handle, CancellationToken token)
    {
        // Originally from idunno.Bluesky https://github.com/blowdart/idunno.Bluesky/blob/b5267989dff4edfdc455a845c27c46233dbdfd73/src/idunno.AtProto/Identity/AtProtoServer.cs
        string didTxtRecordHost = $"_atproto.{handle}";
        const string didTextRecordPrefix = "did=";
        IDnsQueryResponse dnsLookupResult = await this.Options.DnsClient.QueryAsync(didTxtRecordHost, QueryType.TXT, QueryClass.IN, CancellationToken.None).ConfigureAwait(false);

        foreach (TxtRecord? textRecord in dnsLookupResult.Answers.TxtRecords())
        {
            foreach (string? text in textRecord.Text.Where(t => t.StartsWith(didTextRecordPrefix, StringComparison.InvariantCulture)))
            {
                if (ATDid.TryCreate(text.Substring(didTextRecordPrefix.Length), out var did))
                {
                    return did;
                }
                else
                {
                    this.options.Logger?.LogError($"Failed to resolve Handle: {handle}. Invalid DID.");
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Resolves an ATHandle to an ATDid via HTTPs.
    /// </summary>
    /// <param name="handle">The ATHandle to resolve.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns><see cref="ATDid"/>.</returns>
    public async Task<ATDid?> ResolveATHandleViaHTTPSAsync(ATHandle handle, CancellationToken token)
    {
        // Originally from idunno.Bluesky https://github.com/blowdart/idunno.Bluesky/blob/b5267989dff4edfdc455a845c27c46233dbdfd73/src/idunno.AtProto/Identity/AtProtoServer.cs
        Uri didUri = new($"https://{handle}/.well-known/atproto-did");
        using HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, didUri) { Headers = { Accept = { new("text/plain") } } };
        HttpResponseMessage httpResponseMessage = await this.SessionManager.Client.SendAsync(httpRequestMessage, token).ConfigureAwait(false);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
#if NETSTANDARD
            string? did = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
            string? did = await httpResponseMessage.Content.ReadAsStringAsync(token).ConfigureAwait(false);
#endif
            if (ATDid.TryCreate(did, out var atDid))
            {
                return atDid;
            }
            else
            {
                this.options.Logger?.LogError($"Failed to resolve Handle: {handle}. Invalid DID.");
            }
        }
        else
        {
            this.options.Logger?.LogError($"Failed to resolve Handle: {handle}. {httpResponseMessage.StatusCode}");
        }

        return null;
    }

    /// <summary>
    /// Resolves an ATIdentifier to an ATDid.
    /// </summary>
    /// <param name="atIdentifier">ATIdentifier.</param>
    /// <returns>Result of ATDid.</returns>
    internal async Task<Result<ATDid?>> ResolveATDidAsync(ATIdentifier atIdentifier)
    {
        if (atIdentifier is ATDid)
        {
            return (ATDid)atIdentifier;
        }

        if (atIdentifier is ATHandle handle)
        {
            var (didOutput, error) = await this.Identity.ResolveHandleAsync(handle);
            if (error is not null)
            {
                return error;
            }

            return didOutput?.Did;
        }

        return new ATError(new Exception("Could not resolve ATIdentifier to ATDid."));
    }

    private void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.sessionManager.Dispose();
            }

            this.disposedValue = true;
        }
    }

    private void OnSessionUpdated(object? sender, SessionUpdatedEventArgs e)
    {
        this.SessionUpdated?.Invoke(sender, e);
    }

    private async Task<Result<string?>> ResolvePlcDidAsync(ATDid did, CancellationToken? token)
    {
        string? host = null;
        if (this.IsAuthenticated && this.Session?.Did.ToString() == did.ToString())
        {
            host = this.Session?.DidDoc?.GetPDSEndpointUrl(this.options.Logger)?.ToString();
            if (!string.IsNullOrEmpty(host))
            {
                this.options.DidCache[did.ToString()] = host!;
                this.options.Logger?.LogDebug($"Resolved DID: {did} to {host}, adding to cache.");
            }
            else
            {
                this.options.Logger?.LogError($"Failed to resolve Self User DID: {did}, missing Service Handle.");
            }
        }

        var (resolveHandle, error) = await this.PlcDirectory.GetDidDocAsync(did!, token ?? CancellationToken.None);
        if (resolveHandle is not null)
        {
            host = resolveHandle.GetPDSEndpointUrl(this.options.Logger)?.ToString();
            if (string.IsNullOrEmpty(host))
            {
                this.options.Logger?.LogError($"Failed to resolve DID: {did}, missing Service Handle.");
            }
        }
        else
        {
            this.options.Logger?.LogError($"Failed to resolve plc DID: {did}. {error?.ToString()}");
            return error;
        }

        return host;
    }

    private async Task<string?> ResolveWebDidAsync(ATDid did, CancellationToken? token)
    {
        string? host = null;
        var baseUri = did.ToString().Split(':').Last();
        if (!baseUri.Contains("http"))
        {
            baseUri = $"https://{baseUri}";
        }

        if (Uri.TryCreate(baseUri, UriKind.Absolute, out var uri))
        {
            var result = await this.Client.GetAsync($"{uri}{Constants.DidJson}", token ?? CancellationToken.None);
            if (result.IsSuccessStatusCode)
            {
                var didDoc = JsonSerializer.Deserialize<DidDoc>(await result.Content.ReadAsStringAsync(), this.options.SourceGenerationContext.DidDoc);
                if (didDoc is not null)
                {
                    host = didDoc.GetPDSEndpointUrl(this.options.Logger)?.ToString();
                    if (string.IsNullOrEmpty(host))
                    {
                        this.options.Logger?.LogError($"Failed to resolve DID: {did}, missing Service Handle.");
                    }
                }
                else
                {
                    this.options.Logger?.LogError($"Failed to resolve DID: {did}, missing DID Doc.");
                }
            }
            else
            {
                this.options.Logger?.LogError($"Failed to resolve web DID: {did}.");
            }
        }
        else
        {
            this.options.Logger?.LogError($"Failed to resolve web DID: {did}.");
        }

        return host;
    }
}