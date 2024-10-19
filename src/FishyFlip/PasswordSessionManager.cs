// <copyright file="PasswordSessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    private System.Timers.Timer? timer;
    private int refreshing;
    private ILogger? logger;
    private AuthSession? authSession;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordSessionManager"/> class.
    /// </summary>
    /// <param name="protocol"><see cref="ATProtocol"/>.</param>
    public PasswordSessionManager(ATProtocol protocol)
    {
        this.protocol = protocol;
        this.client = protocol.Options.GenerateHttpClient();
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
        this.client = protocol.Options.GenerateHttpClient();
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
    public Task RefreshSessionAsync(CancellationToken cancellationToken = default)
        => this.RefreshTokenAsync(cancellationToken);

    /// <inheritdoc/>
    public void Dispose() => this.Dispose(true);

    /// <summary>
    /// Asynchronously creates a new session.
    /// </summary>
    /// <param name="identifier">The identifier of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    internal async Task<Session?> CreateSessionAsync(string identifier, string password, CancellationToken cancellationToken = default)
    {
#pragma warning disable CS0618
        var sessionResult = await this.protocol.Server.CreateSessionAsync(identifier, password, cancellationToken);
#pragma warning restore CS0618
        Session? resultSession = null;
        sessionResult.Switch(
            session =>
        {
            resultSession = session;
            if (this.protocol.Options.UseServiceEndpointUponLogin)
            {
                var logger = this.protocol.Options.Logger;
                var serviceUrl = session.DidDoc?.Service?.FirstOrDefault()?.ServiceEndpoint;
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
                        this.client = this.protocol.Options.GenerateHttpClient();
                        logger?.LogInformation($"UseServiceEndpointUponLogin enabled, switching to {uriResult}.");
                    }
                }
            }

            this.SetSession(session);
        },
            e => this.logger?.LogError(e.ToString(), e));

        return resultSession;
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

        if (!this.protocol.Options.AutoRenewSession)
        {
            this.logger?.LogDebug("AutoRenewSession is disabled.");
            return;
        }

        this.logger?.LogDebug("AutoRenewSession is enabled.");
        this.timer ??= new System.Timers.Timer();

        this.ConfigureRefreshTokenTimer();
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
    /// Refresh session token.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    internal async Task RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        if (Interlocked.Increment(ref this.refreshing) > 1)
        {
            this.logger?.LogDebug("Already refreshing.");
            Interlocked.Decrement(ref this.refreshing);
            return;
        }

        this.logger?.LogDebug("Refreshing session.");
        if (this.timer is not null)
        {
            this.timer.Enabled = false;
        }

        try
        {
            if (this.session is not null)
            {
                Multiple<Session, ATError> result =
                await this.protocol.ThrowIfNull().Server.RefreshSessionAsync(this.session, cancellationToken);

                result
                    .Switch(
                    this.SetSession,
                    e =>
                    {
                        this.logger?.LogError(e.ToString(), e);
                    });
            }
            else
            {
                this.logger?.LogInformation("Session is null, skipping refresh.");
            }
        }
        finally
        {
            Interlocked.Decrement(ref this.refreshing);
            this.logger?.LogDebug("Session refreshed.");
        }
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">Is disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.disposed || this.timer is null)
        {
            return;
        }

        if (disposing)
        {
            this.timer.Enabled = false;
            this.timer.Dispose();
            this.timer = null;
            this.client?.Dispose();
        }

        this.disposed = true;
    }

    private void ConfigureRefreshTokenTimer()
    {
        this.timer.ThrowIfNull();
        TimeSpan timeToNextRenewal = this.protocol.Options.SessionRefreshInterval ?? this.GetTimeToNextRenewal(this.session.ThrowIfNull());

        // If less than 10,000 Milliseconds or negative, force refresh. Once it does, it should then set the timer.
        if (timeToNextRenewal.TotalMilliseconds < 10000)
        {
            this.RefreshTokenAsync().FireAndForgetSafeAsync(this.logger);
            return;
        }

        var seconds = timeToNextRenewal.TotalMilliseconds >= int.MaxValue ? int.MaxValue : timeToNextRenewal.TotalMilliseconds;
        this.logger?.LogDebug($"Next renewal in {seconds}.");
        if (this.timer is not null)
        {
            this.timer.Elapsed += this.RefreshToken;
            this.timer.Interval = seconds;
            this.timer.Enabled = true;
            this.timer.Start();
        }
    }

    private TimeSpan GetTimeToNextRenewal(Session session)
    {
        this.jwtSecurityTokenHandler
            .ValidateToken(
                session.AccessJwt,
                this.defaultTokenValidationParameters,
                out SecurityToken token);
        return token.ValidTo.ToUniversalTime() - DateTime.UtcNow;
    }

    private void RefreshToken(object? sender, ElapsedEventArgs e)
        => this.RefreshTokenAsync().FireAndForgetSafeAsync(this.logger);
}
