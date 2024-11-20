// <copyright file="ATProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
        this.sessionManager = new UnauthenticatedSessionManager(options);
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
    public AuthSession? OAuthSession => this.sessionManager is OAuth2SessionManager oAuth2SessionManager ? oAuth2SessionManager.OAuthSession : null;

    /// <summary>
    /// Gets the current PasswordSession session, if any is active.
    /// </summary>
    public AuthSession? PasswordSession => this.sessionManager is PasswordSessionManager passwordSessionManager ? passwordSessionManager.PasswordSession : null;

    /// <summary>
    /// Gets the PclDirectory Methods.
    /// </summary>
    public PlcDirectory PlcDirectory => new(this);

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
    /// Gets or sets the internal session manager.
    /// </summary>
    internal ISessionManager SessionManager
    {
        get => this.sessionManager;

        set
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
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    public async Task<Session?> AuthenticateWithPasswordAsync(string identifier, string password, CancellationToken cancellationToken = default)
    {
        var passwordSessionManager = new PasswordSessionManager(this);
        this.SessionManager = passwordSessionManager;

        return await passwordSessionManager.CreateSessionAsync(identifier, password, cancellationToken);
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
    public async Task<string> GenerateOAuth2AuthenticationUrlAsync(string clientId, string redirectUrl, IEnumerable<string> scopes, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        var oAuth2SessionManager = new OAuth2SessionManager(this);
        this.SessionManager = oAuth2SessionManager;
        return await oAuth2SessionManager.StartAuthorizationAsync(clientId, redirectUrl, scopes, instanceUrl, cancellationToken);
    }

    /// <summary>
    /// Authenticates with OAuth2 callback asynchronously.
    /// </summary>
    /// <param name="callbackData">The callback data received from the OAuth2 provider.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    public async Task<Session?> AuthenticateWithOAuth2CallbackAsync(string callbackData, CancellationToken cancellationToken = default)
    {
        if (this.SessionManager is not OAuth2SessionManager oAuth2SessionManager)
        {
            throw new OAuth2Exception("Session manager is not an OAuth2 session manager.");
        }

        return await oAuth2SessionManager.CompleteAuthorizationAsync(callbackData, cancellationToken);
    }

    /// <summary>
    /// Authenticates with password session asynchronously.
    /// </summary>
    /// <param name="session">The password session.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    /// <exception cref="OAuth2Exception">Thrown if ProofKey was included in AuthSession.</exception>
    public async Task<Session?> AuthenticateWithPasswordSessionAsync(AuthSession session)
    {
        if (!string.IsNullOrEmpty(session.ProofKey))
        {
            throw new OAuth2Exception("Proof key is not required for password sessions. Is this an OAuth2 session?");
        }

        var passwordSessionManager = new PasswordSessionManager(this, session.Session);
        this.SessionManager = passwordSessionManager;
        return await Task.FromResult<Session?>(passwordSessionManager.Session);
    }

    /// <summary>
    /// Authenticates with OAuth2 session asynchronously.
    /// </summary>
    /// <param name="session">The OAuth session.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="instanceUrl">Optional. The instance URL. If null, uses https://bsky.social.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    public async Task<Session?> AuthenticateWithOAuth2SessionAsync(AuthSession session, string clientId, string? instanceUrl = default)
    {
        var oAuth2SessionManager = new OAuth2SessionManager(this);
        this.SessionManager = oAuth2SessionManager;
        if (string.IsNullOrEmpty(session.ProofKey))
        {
            throw new OAuth2Exception("Proof key is required for OAuth2 sessions.");
        }

        return (await oAuth2SessionManager.StartSessionAsync(session, clientId, instanceUrl)).Session;
    }

    /// <summary>
    /// Refreshes the current session asynchronously.
    /// </summary>
    /// <returns><see cref="AuthSession"/>.</returns>
    public async Task<AuthSession?> RefreshAuthSessionAsync()
    {
        switch (this.sessionManager)
        {
            case OAuth2SessionManager oAuth2SessionManager:
                // Refresh the token to make sure it's the most up to date.
                var result = await oAuth2SessionManager.RefreshTokenAsync();
                return oAuth2SessionManager.OAuthSession;
            case PasswordSessionManager { Session: not null } passwordManager:
                await passwordManager.RefreshSessionAsync();
                return new AuthSession(passwordManager.Session);
            default:
                return null;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
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
}