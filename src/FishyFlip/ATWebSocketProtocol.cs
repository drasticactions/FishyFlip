// <copyright file="ATWebSocketProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT WebSocket Protocol.
/// </summary>
internal class ATWebSocketProtocol : IDisposable
{
    private const int ReceiveBufferSize = 32768;
    private ClientWebSocket client;
    private ATProtocol protocol;
    private CancellationToken? token;
    private CBORTypeMapper mapper;
    private CarDecoder decoder;
    private bool disposedValue;
    private Task? recieveTask;
    private ILogger? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketProtocol"/> class.
    /// </summary>
    /// <param name="options"><see cref="ATProtocolOptions"/>.</param>
    public ATWebSocketProtocol(ATProtocol protocol)
    {
        this.protocol = protocol;
        this.logger = protocol.Options.Logger;
        this.client = new ClientWebSocket();
        var opsConverter = new OpsConverter();
        this.mapper = new CBORTypeMapper()
            .AddConverter(typeof(FrameBody), new FrameBodyConverter(opsConverter))
            .AddConverter(typeof(FrameHeader), new FrameHeaderConverter());
        this.decoder = new CarDecoder();
    }

    /// <summary>
    /// Connect to the BlueSky instance via a WebSocket connection.
    /// </summary>
    /// <param name="token">CancellationToken.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task ConnectAsync(CancellationToken? token = default)
    {
        if (this.client.State == WebSocketState.Open)
        {
            return;
        }

        var baselineUrl = new Uri($"{this.protocol.Options.Url}");
        var endToken = token ?? CancellationToken.None;
        await this.client.ConnectAsync(new Uri($"wss://{baselineUrl.Host}{Constants.Urls.AtProtoSync.SubscribeRepos}"), endToken);
        this.logger?.LogInformation($"WSS: Connected to {baselineUrl}");
        await this.ReceiveMessages(this.client, endToken);
    }

    /// <summary>
    /// Close the existing WebSocket connection.
    /// </summary>
    /// <param name="status">Status for the shutdown. Defaults to <see cref="WebSocketCloseStatus.NormalClosure"/>.</param>
    /// <param name="disconnectReason">Reason for the shutdown.</param>
    /// <param name="token">CancellationToken.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task CloseAsync(WebSocketCloseStatus status = WebSocketCloseStatus.NormalClosure, string disconnectReason = "Client disconnecting", CancellationToken? token = default)
    {
        var endToken = token ?? CancellationToken.None;
        this.logger?.LogInformation($"WSS: Disconnecting");
        return this.client.CloseAsync(status, disconnectReason, endToken);
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">Is Disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.client.Dispose();
            }

            this.disposedValue = true;
        }
    }

    private async Task ReceiveMessages(ClientWebSocket webSocket, CancellationToken token)
    {
        byte[] receiveBuffer = new byte[ReceiveBufferSize];
        while (webSocket.State == WebSocketState.Open)
        {
            try
            {
                WebSocketReceiveResult result =
                    await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), token);
                if (result is not { MessageType: WebSocketMessageType.Binary, EndOfMessage: true })
                {
                    continue;
                }

                using var memoryStream = new MemoryStream(receiveBuffer, 0, result.Count);
                var objects = CBORObject.ReadSequence(memoryStream);
                var frameHeader = objects[0].ToObject<FrameHeader>(this.mapper);
                var frameBody = objects[1].ToObject<FrameBody>(this.mapper);
            }
            catch (OperationCanceledException canceledException)
            {
                this.logger?.LogDebug("WSS: Operation Canceled.");
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, "WSS: Error receiving message.");
            }
        }
    }

    /// <inheritdoc/>
    void IDisposable.Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}