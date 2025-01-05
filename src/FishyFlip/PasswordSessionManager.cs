// <copyright file="PasswordSessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.Com.Atproto.Server;
using Microsoft.IdentityModel.Tokens;

namespace FishyFlip;

/// <summary>
/// ATProtocol Password Session Manager.
/// </summary>
internal class PasswordSessionManager : ISessionManager
{
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

    private readonly TokenValidationParameters defaultTokenValidationParameters = new()
    {
        ValidateActor = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuer = false,
        ValidateSignatureLast = false,
        ValidateTokenReplay = false,
        ValidateIssuerSigningKey = false,
        ValidateWithLKG = false,
        LogValidationExceptions = false,
        SignatureValidator = (token, parameters) =>
        {
            var jwt = new JwtSecurityToken(token);
            return jwt;
        },
    };

    private HttpClient client;
    private ATProtocol protocol;
    private Session? session;
    private bool disposed;
    private ILogger? logger;
    private AuthSession? authSession;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordSessionManager"/> class.
    /// </summary>
    /// <param name="protocol"><see cref="ATProtocol"/>.</param>
    public PasswordSessionManager(ATProtocol protocol)
    {
        this.protocol = protocol;
        this.client = protocol.Options.GenerateHttpClient(this.protocol);
        this.logger = this.protocol.Options.Logger;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordSessionManager"/> class.
    /// </summary>
    /// <param name="protocol"><see cref="ATProtocol"/>.</param>
    /// <param name="session">Existing Session.</param>
    public PasswordSessionManager(ATProtocol protocol, Session session)
    {
        this.protocol = protocol;
        this.client = protocol.Options.GenerateHttpClient(this.protocol);
        this.logger = this.protocol.Options.Logger;
        this.SetSession(session);
    }

    /// <inheritdoc/>
    public event EventHandler<SessionUpdatedEventArgs>? SessionUpdated;

    /// <summary>
    /// Gets a value indicating whether the user is authenticated.
    /// </summary>
    public bool IsAuthenticated => this.session is not null;

    /// <summary>
    /// Gets the current session. Can be null if no session.
    /// </summary>
    public Session? Session => this.session;

    /// <inheritdoc/>
    public HttpClient Client => this.client;

    /// <summary>
    /// Gets the password Auth Session.
    /// </summary>
    public AuthSession? PasswordSession => this.authSession;

    /// <inheritdoc/>
    public async Task<Result<RefreshSessionOutput?>> RefreshSessionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (this.session is null)
            {
                throw new ArgumentNullException(nameof(this.session));
            }

            this.UpdateBearerTokenForRefresh(this.session);
            var (d, error) = await this.protocol.ThrowIfNull().RefreshSessionAsync(cancellationToken);

            if (error != null)
            {
                return error;
            }

            if (d is not null)
            {
                var newSession = new Session(
                    d!.Did!,
                    d.DidDoc,
                    d.Handle!,
                    string.Empty,
                    d.AccessJwt!,
                    d.RefreshJwt!,
                    this.GetTimeToNextRenewal(d.AccessJwt!));
                this.SetSession(newSession);
            }

            return d;
        }
        catch (Exception e)
        {
            this.logger?.LogError(e, "Error refreshing session.");
            throw;
        }
    }

    /// <inheritdoc/>
    public void Dispose() => this.Dispose(true);

    /// <summary>
    /// Asynchronously creates a new session.
    /// </summary>
    /// <param name="identifier">The identifier of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="authFactorToken">2-Factor Auth Token, optional.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    internal async Task<Result<Session?>> CreateSessionAsync(string identifier, string password, string? authFactorToken = default,  CancellationToken cancellationToken = default)
    {
        var host = this.protocol.Options.Url.ToString();
        var usingPublicApi = host.Contains(Constants.Urls.ATProtoServer.PublicApi);
        if (usingPublicApi)
        {
            this.logger?.LogInformation($"Using public server {host} to login, resolving identifier to find host.");
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (emailRegex.IsMatch(identifier))
            {
                this.logger?.LogWarning($"Using email address {identifier} to login to {host}. This is not recommended. Please use your handle instead.");
            }
            else if (ATIdentifier.TryCreate(identifier, out var ident))
            {
                string? resolveHost = null;
                if (ident is ATHandle handle)
                {
                    (resolveHost, var resolveHandleError) = await this.protocol.ResolveATHandleHostAsync(handle, cancellationToken);
                    if (resolveHandleError is not null)
                    {
                        this.logger?.LogError($"Error resolving handle {identifier}: {resolveHandleError}");
                    }
                }
                else if (ident is ATDid did)
                {
                    (resolveHost, var resolveDidError) = await this.protocol.ResolveATDidHostAsync(did, cancellationToken);
                    if (resolveDidError is not null)
                    {
                        this.logger?.LogError($"Error resolving did {identifier}: {resolveDidError}");
                    }
                }

                if (Uri.TryCreate(resolveHost, UriKind.Absolute, out var uri))
                {
                    this.protocol.Options.Url = uri;
                    this.logger?.LogInformation($"Resolved handle {identifier} to {uri}, setting for authentication");
                }
                else
                {
                    this.logger?.LogWarning($"Could not resolve identifier {identifier} to a valid host.");
                }
            }
            else
            {
                this.logger?.LogWarning($"Could not validate identifier: {identifier}");
            }
        }

#pragma warning disable CS0618
        var (session, error) = await this.protocol.CreateSessionAsync(identifier, password, authFactorToken, cancellationToken: cancellationToken);
#pragma warning restore CS0618
        if (session is not null)
        {
            if (this.protocol.Options.UseServiceEndpointUponLogin)
            {
                var logger = this.protocol.Options.Logger;
                var serviceUrl = session!.DidDoc?.Service?.FirstOrDefault()?.ServiceEndpoint;
                if (string.IsNullOrEmpty(serviceUrl))
                {
                    logger?.LogWarning($"UseServiceEndpointUponLogin enabled, but session missing Service Endpoint.");
                }
                else
                {
                    var result = Uri.TryCreate(serviceUrl, UriKind.Absolute, out Uri? uriResult);
                    if (!result || uriResult is null)
                    {
                        logger?.LogWarning($"UseServiceEndpointUponLogin enabled, but session missing Service Endpoint.");
                    }
                    else
                    {
                        this.protocol.Options.Url = uriResult;
                        this.client.Dispose();
                        this.client = this.protocol.Options.GenerateHttpClient(this.protocol);
                        logger?.LogInformation($"UseServiceEndpointUponLogin enabled, switching to {uriResult}.");
                    }
                }
            }

            var resultSession = new Session(
                session!.Did!,
                session.DidDoc,
                session.Handle!,
                session.Email,
                session.AccessJwt!,
                session.RefreshJwt!,
                this.GetTimeToNextRenewal(session.AccessJwt!));
            this.SetSession(resultSession);
            return resultSession;
        }

        if (error is not null)
        {
            this.logger?.LogError(error.ToString());
            if (usingPublicApi)
            {
                this.logger?.LogInformation($"Login failed, Resetting to public server {Constants.Urls.ATProtoServer.PublicApi}.");
                this.protocol.Options.Url = new Uri(Constants.Urls.ATProtoServer.PublicApi);
            }

            return error;
        }

        throw new ATProtocolUnknownError($"Unknown error in Password CreateSessionAsync.");
    }

    /// <summary>
    /// Sets the given session.
    /// </summary>
    /// <param name="session"><see cref="Session"/>.</param>
    internal void SetSession(Session session)
    {
        this.session = session;
        this.UpdateBearerToken(session);

        this.logger?.LogDebug($"Session set, {session.Did}");

        lock (this)
        {
            this.authSession = new AuthSession(session);
        }

        this.SessionUpdated?.Invoke(this, new SessionUpdatedEventArgs(this.authSession, this.protocol.Options.Url));
    }

    /// <summary>
    /// Updates the bearer token for the session.
    /// </summary>
    /// <param name="session">The updated session.</param>
    internal void UpdateBearerToken(Session session)
    {
        this.client
                .DefaultRequestHeaders
                .Authorization =
            new AuthenticationHeaderValue("Bearer", session.AccessJwt);
    }

    /// <summary>
    /// Updates the bearer token for the session.
    /// </summary>
    /// <param name="session">The updated session.</param>
    internal void UpdateBearerTokenForRefresh(Session session)
    {
        this.client
                .DefaultRequestHeaders
                .Authorization =
            new AuthenticationHeaderValue("Bearer", session.RefreshJwt);
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">Is disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.disposed)
        {
            return;
        }

        if (disposing)
        {
            this.client?.Dispose();
        }

        this.disposed = true;
    }

    private DateTime GetTimeToNextRenewal(string accessJwt)
    {
        this.jwtSecurityTokenHandler
            .ValidateToken(
                accessJwt,
                this.defaultTokenValidationParameters,
                out SecurityToken token);
        return token.ValidTo;
    }
}
