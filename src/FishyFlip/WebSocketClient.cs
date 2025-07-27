// <copyright file="WebSocketClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Buffers;
using System.IO.Pipelines;
using FishyFlip.Abstractions;

namespace FishyFlip;

/// <summary>
/// Default implementation of IWebSocketClient using System.Net.WebSockets.ClientWebSocket.
/// </summary>
internal sealed class WebSocketClient : IWebSocketClient
{
    private readonly ClientWebSocket webSocket;
    private readonly Pipe pipe;
    private readonly CancellationTokenSource cts;
    private readonly ILogger? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebSocketClient"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    public WebSocketClient(ILogger? logger = null)
    {
        this.webSocket = new ClientWebSocket();
        this.pipe = new Pipe();
        this.cts = new CancellationTokenSource();
        this.logger = logger;
    }

    /// <inheritdoc/>
    public event Func<ReadOnlySequence<byte>, Task>? OnMessageReceived;

    /// <inheritdoc/>
    public WebSocketState State => this.webSocket.State;

    /// <inheritdoc/>
    public bool IsConnected => this.webSocket.State == WebSocketState.Open;

    /// <inheritdoc/>
    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        if (this.webSocket.State == WebSocketState.Open)
        {
            return;
        }

        this.logger?.LogInformation("WSS: Connecting to WebSocket.");
        await this.webSocket.ConnectAsync(uri, cancellationToken);
        _ = this.StartReceiveLoop();
    }

    /// <inheritdoc/>
    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        if (this.webSocket.State == WebSocketState.Closed || this.webSocket.State == WebSocketState.Aborted)
        {
            return;
        }

        this.logger?.LogInformation("WSS: Closing WebSocket connection.");
        await this.webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.cts.Cancel();
        this.webSocket.Dispose();
        this.cts.Dispose();
    }

    private async Task StartReceiveLoop()
    {
        try
        {
            while (this.webSocket.State == WebSocketState.Open)
            {
                var memory = this.pipe.Writer.GetMemory(8192);
#if NETSTANDARD
                var arraySegment = new ArraySegment<byte>(memory.Span.ToArray());
                var result = await this.webSocket.ReceiveAsync(
                    arraySegment, this.cts.Token);
#else
                var result = await this.webSocket.ReceiveAsync(
                    memory, this.cts.Token);
#endif

                if (result.Count <= 0)
                {
                    continue;
                }

                this.pipe.Writer.Advance(result.Count);

                if (result.EndOfMessage)
                {
                    await this.pipe.Writer.FlushAsync(this.cts.Token);
                    await this.ProcessMessageAsync();
                }

                if (result.MessageType == WebSocketMessageType.Close || this.webSocket.State == WebSocketState.Aborted)
                {
                    break;
                }
            }
        }
        catch (OperationCanceledException ocx)
        {
            this.logger?.LogInformation(ocx, "WSS: Operation Cancelled.");
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "WSS: Error in Receive Loop.");
            throw;
        }
        finally
        {
            await this.pipe.Writer.CompleteAsync();
        }
    }

    private async Task ProcessMessageAsync()
    {
        ReadResult result = await this.pipe.Reader.ReadAsync(this.cts.Token);
        ReadOnlySequence<byte> buffer = result.Buffer;

        try
        {
            if (this.OnMessageReceived != null)
            {
                if (this.webSocket.State == WebSocketState.Open)
                {
                    await this.OnMessageReceived(buffer);
                }
            }
        }
        finally
        {
            this.pipe.Reader.AdvanceTo(buffer.End);
        }
    }
}