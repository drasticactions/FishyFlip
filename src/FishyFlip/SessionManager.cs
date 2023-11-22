// <copyright file="SessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Bluesky Session Manager.
/// </summary>
internal class SessionManager : IDisposable
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

    private ATProtocol protocol;
    private Session? session;
    private bool disposed;
    private System.Timers.Timer? timer;
    private int refreshing;
    private ILogger? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionManager"/> class.
    /// </summary>
    /// <param name="protocol"><see cref="ATProtocol"/>.</param>
    public SessionManager(ATProtocol protocol)
    {
        this.protocol = protocol;
        this.logger = this.protocol.Options.Logger;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionManager"/> class.
    /// </summary>
    /// <param name="protocol"><see cref="ATProtocol"/>.</param>
    /// <param name="session">Existing Session.</param>
    public SessionManager(ATProtocol protocol, Session session)
    {
        this.protocol = protocol;
        this.logger = this.protocol.Options.Logger;
        this.SetSession(session);
    }

    /// <summary>
    /// Gets a value indicating whether the user is authenticated.
    /// </summary>
    public bool IsAuthenticated => this.session is not null;

    /// <summary>
    /// Gets the current session. Can be null if no session.
    /// </summary>
    public Session? Session => this.session;

    /// <inheritdoc/>
    public void Dispose() => this.Dispose(true);

    /// <summary>
    /// Updates the bearer token for the session.
    /// </summary>
    /// <param name="session">The updated session.</param>
    internal void UpdateBearerToken(Session session)
    {
        this.protocol.Client
                .DefaultRequestHeaders
                .Authorization =
            new AuthenticationHeaderValue("Bearer", session.AccessJwt);
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
        }

        this.disposed = true;
    }

    /// <summary>
    /// Refresh session token.
    /// </summary>
    /// <returns>Task.</returns>
    internal async Task RefreshTokenAsync()
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
                Multiple<Session, Error> result =
                await this.protocol.ThrowIfNull().Server.RefreshSessionAsync(this.session, CancellationToken.None);

                result
                    .Switch(
                    s =>
                    {
                        this.SetSession(s);
                    },
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
