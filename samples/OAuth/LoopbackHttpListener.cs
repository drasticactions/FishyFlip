// <copyright file="LoopbackHttpListener.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using IdentityModel.OidcClient.Browser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OAuth;

/// <summary>
/// Loopback Http Listener.
/// </summary>
public class LoopbackHttpListener : IDisposable
{
    private const int DefaultTimeout = 60 * 5; // 5 mins (in seconds)

    private IWebHost host;

    private TaskCompletionSource<string> source = new TaskCompletionSource<string>();
    private string url;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoopbackHttpListener"/> class.
    /// </summary>
    /// <param name="port">Port number.</param>
    /// <param name="path">Path.</param>
    public LoopbackHttpListener(int port, string? path = null)
    {
        path = path ?? string.Empty;
        if (path.StartsWith("/"))
        {
            path = path.Substring(1);
        }

        this.url = $"http://127.0.0.1:{port}/{path}";

        this.host = new WebHostBuilder()
            .UseKestrel()
            .UseUrls(this.url)
            .Configure(this.Configure)
            .Build();
        this.host.Start();
    }

    /// <summary>
    /// Gets the URL.
    /// </summary>
    public string Url => this.url;

    /// <inheritdoc/>
    public void Dispose()
    {
        Task.Run(async () =>
        {
            await Task.Delay(500);
            this.host.Dispose();
        });
    }

    /// <summary>
    /// Wait for the callback.
    /// </summary>
    /// <param name="timeoutInSeconds">Timeout in seconds.</param>
    /// <returns>String result.</returns>
    public Task<string> WaitForCallbackAsync(int timeoutInSeconds = DefaultTimeout)
    {
        Task.Run(async () =>
        {
            await Task.Delay(timeoutInSeconds * 1000);
            this.source.TrySetCanceled();
        });

        return this.source.Task;
    }

    private void Configure(IApplicationBuilder app)
    {
        app.Run(async ctx =>
        {
            if (ctx.Request.Method == "GET")
            {
                await this.SetResultAsync(ctx.Request.QueryString.Value ?? string.Empty, ctx);
            }
            else if (ctx.Request.Method == "POST")
            {
                if (ctx.Request.ContentType is null)
                {
                    ctx.Response.StatusCode = 415;
                }
                else if (!ctx.Request.ContentType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
                {
                    ctx.Response.StatusCode = 415;
                }
                else
                {
                    using (var sr = new StreamReader(ctx.Request.Body, Encoding.UTF8))
                    {
                        var body = await sr.ReadToEndAsync();
                        await this.SetResultAsync(body, ctx);
                    }
                }
            }
            else
            {
                ctx.Response.StatusCode = 405;
            }
        });
    }

    private async Task SetResultAsync(string value, HttpContext ctx)
    {
        try
        {
            ctx.Response.StatusCode = 200;
            ctx.Response.ContentType = "text/html";
            await ctx.Response.WriteAsync("<h1>You can now return to the application.</h1>");
            await ctx.Response.Body.FlushAsync();

            this.source.TrySetResult(value);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

            ctx.Response.StatusCode = 400;
            ctx.Response.ContentType = "text/html";
            await ctx.Response.WriteAsync("<h1>Invalid request.</h1>");
            await ctx.Response.Body.FlushAsync();
        }
    }
}