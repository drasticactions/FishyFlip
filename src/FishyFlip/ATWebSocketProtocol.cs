// <copyright file="ATWebSocketProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;
using System.IO;

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
        this.ReceiveMessages(this.client, endToken).FireAndForgetSafeAsync(this.logger);
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

    /// <inheritdoc/>
    void IDisposable.Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
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

    private async Task HandleMessage(WebSocketReceiveResult result)
    {

    }

    private async Task HandleMessage(byte[] byteArray)
    {
        using var stream = new MemoryStream(byteArray);
        var objects = CBORObject.ReadSequence(stream);

        if (objects.Length != 2)
        {
            return;
        }

        //this.logger?.LogDebug($"WSS: {objects[0].ToJSONString()}");
        //this.logger?.LogDebug($"WSS: {objects[1].ToJSONString()}");

        var frameHeader = new FrameHeader(objects[0]);

        switch (frameHeader.Operation)
        {
            case FrameHeaderOperation.Unknown:
                break;
            case FrameHeaderOperation.Frame:
                var frameBody = new FrameBody(objects[1]);
                if (frameBody.Blocks is null)
                {
                    break;
                }

                var blocks = CarDecoder.DecodeCar(frameBody.Blocks);
                foreach (var block in blocks)
                {
                    using var blockStream = new MemoryStream(block.Value);
                    var blockObj = CBORObject.Read(blockStream);
                    if (blockObj["$type"] is not null)
                    {
                        //this.logger?.LogDebug($"WSS: Type Obj: {blockObj.ToJSONString()}");
                        switch (blockObj["$type"].AsString())
                        {
                            case Constants.FeedType.Post:
                                var post = new Post(blockObj);
                                break;
                            case Constants.FeedType.Like:
                                var like = new Like(blockObj);
                                break;
                            case Constants.FeedType.Repost:
                                var repost = new Repost(blockObj);
                                break;
                            case Constants.GraphTypes.Follow:
                                var follow = new Follow(blockObj);
                                break;
                            case Constants.GraphTypes.List:
                                var list = new BSList(blockObj);
                                break;
                            case Constants.GraphTypes.Block:
                                var blockG = new Block(blockObj);
                                break;
                            case Constants.ActorTypes.Profile:
                                var profile = new Profile(blockObj);
                                break;
                            default:
                                this.logger?.LogDebug($"WSS: Unknown Obj: {blockObj.ToJSONString()}");
                                break;
                        }
                    }
                    else if (blockObj["sig"] is not null)
                    {
                        //this.logger?.LogDebug($"WSS: Footer: {blockObj.ToJSONString()}");
                        var footer = new FrameFooter(blockObj);
                    }
                    else
                    {
                       // this.logger?.LogDebug($"WSS: Node: {blockObj.ToJSONString()}");
                        var node = new FrameNode(blockObj);
                    }
                }

                break;
            case FrameHeaderOperation.Error:
                var frameError = new FrameError(objects[1]);
                this.logger?.LogError($"WSS: Error: {frameError.Message}");
                this.CloseAsync(WebSocketCloseStatus.InternalServerError, frameError.Message ?? string.Empty).FireAndForgetSafeAsync(this.logger);
                break;
            default:
                break;
        }
    }

    private async Task ReceiveMessages(ClientWebSocket webSocket, CancellationToken token)
    {
        byte[] receiveBuffer = new byte[ReceiveBufferSize];
        while (webSocket.State == WebSocketState.Open)
        {
            try
            {
                var result =
                    await webSocket.ReceiveAsync(new Memory<byte>(receiveBuffer), token);
                if (result is not { MessageType: WebSocketMessageType.Binary, EndOfMessage: true })
                {
                    continue;
                }

                byte[] newArray = new byte[result.Count];
                Array.Copy(receiveBuffer, 0, newArray, 0, result.Count);

                Task.Run(() => this.HandleMessage(newArray)).FireAndForgetSafeAsync(this.logger);
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
}