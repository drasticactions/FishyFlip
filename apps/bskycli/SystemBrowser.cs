// <copyright file="SystemBrowser.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using IdentityModel.OidcClient.Browser;

namespace Bskycli;

/// <summary>
/// System Browser.
/// </summary>
public class SystemBrowser : IBrowser
{
    private readonly string? path;

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemBrowser"/> class.
    /// </summary>
    /// <param name="port">Default port.</param>
    /// <param name="path">Default path.</param>
    public SystemBrowser(int port = 0, string? path = null)
    {
        this.path = path;

        if (port == 0)
        {
            this.Port = this.GetRandomUnusedPort();
        }
        else
        {
            this.Port = port;
        }
    }

    /// <summary>
    /// Gets the port.
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// Opens the users browser with the given URL.
    /// </summary>
    /// <param name="url">URL to open.</param>
    public static void OpenBrowser(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true,
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
    }

    /// <summary>
    /// Starts the browser session.
    /// </summary>
    /// <param name="options">URL.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>The result of the session.</returns>
    public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Starts the browser session.
    /// </summary>
    /// <param name="url">URL.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>The result of the session.</returns>
    public async Task<BrowserResult> InvokeAsync(string url, CancellationToken cancellationToken = default(CancellationToken))
    {
        using (var listener = new LoopbackHttpListener(this.Port, this.path))
        {
            OpenBrowser(url);

            try
            {
                var result = await listener.WaitForCallbackAsync();
                if (string.IsNullOrWhiteSpace(result))
                {
                    return new BrowserResult { ResultType = BrowserResultType.UnknownError, Error = "Empty response." };
                }

                return new BrowserResult { Response = result, ResultType = BrowserResultType.Success };
            }
            catch (TaskCanceledException ex)
            {
                return new BrowserResult { ResultType = BrowserResultType.Timeout, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new BrowserResult { ResultType = BrowserResultType.UnknownError, Error = ex.Message };
            }
        }
    }

    private int GetRandomUnusedPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
}