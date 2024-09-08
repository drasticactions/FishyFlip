// <copyright file="OAuth2SessionManager.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;

namespace FishyFlip;

/// <summary>
/// OAuth2 Session Manager.
/// </summary>
internal class OAuth2SessionManager : ISessionManager
{
    private bool disposedValue;
    private HttpClient client;
    private ILogger? logger;
    private ATProtocol protocol;
    private Session? session;

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2SessionManager"/> class.
    /// </summary>
    /// <param name="protocol">Protocol.</param>
    public OAuth2SessionManager(ATProtocol protocol)
    {
        this.protocol = protocol;
        this.client = this.protocol.Options.GenerateHttpClient();
        this.logger = this.protocol.Options.Logger;
    }

    /// <inheritdoc/>
    public HttpClient Client => this.client;

    /// <inheritdoc/>
    public bool IsAuthenticated => this.session != null;

    /// <inheritdoc/>
    public Session? Session => this.session;

    /// <summary>
    /// Starts an OAuth2 Session.
    /// Once called, the session state is kept in memory.
    /// If you call this method again and try using the outputted URL, it will fail.
    /// </summary>
    /// <param name="clientId">ClientID, must be a URL.</param>
    /// <param name="redirectUrl">RedirectUrl.</param>
    /// <param name="scopes">ATProtocol Scopes.</param>
    /// <param name="instanceUrl">InstanceUrl, must be a URL. If null, uses https://bsky.social.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Authorization URL to call.</returns>
    public Task<string> StartAuthorizationAsync(string clientId, string redirectUrl, IEnumerable<string> scopes, string? instanceUrl = default, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the callback from the OAuth2 Server.
    /// </summary>
    /// <param name="data">Data URI from the callback.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    /// <exception cref="OAuth2Exception">Thrown if Login fails.</exception>
    public Task<Session> CompleteAuthorizationAsync(string data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task RefreshSessionAsync()
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public void SetSession(Session session)
    {
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
                var result2 = Uri.TryCreate(serviceUrl, UriKind.Absolute, out Uri? uriResult);
                if (!result2 || uriResult is null)
                {
                    logger?.LogWarning($"UseServiceEndpointUponLogin enabled, but session missing Service Endpoint.");
                }
                else
                {
                    this.protocol.Options.Url = uriResult;
                    this.client.Dispose();
                    logger?.LogInformation($"UseServiceEndpointUponLogin enabled, switching to {uriResult}.");
                }
            }
        }

        this.session = session;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">Disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
            }

            this.disposedValue = true;
        }
    }
}
