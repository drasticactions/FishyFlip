// <copyright file="ATWebSocketProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Google.Protobuf;

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

        if (this.client.State == WebSocketState.Aborted || this.client.State == WebSocketState.Closed)
        {
            this.client = new ClientWebSocket();
        }

        var baselineUrl = new Uri($"{this.protocol.Options.Url}");
        var endToken = token ?? CancellationToken.None;
        await this.client.ConnectAsync(new Uri($"wss://{baselineUrl.Host}{Constants.Urls.ATProtoSync.SubscribeRepos}"), endToken);
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

    private void HandleMessage(byte[] byteArray)
    {
        using var stream = new MemoryStream(byteArray);
        var objects = CBORObject.ReadSequence(stream);

        if (objects.Length != 2)
        {
            return;
        }

        var message = new SubscribeRepoMessage();

        var frameHeader = new FrameHeader(objects[0]);

        // this.logger?.LogDebug($"FrameHeader: {objects[0].ToJSONString()}");
        message.Header = frameHeader;

        switch (frameHeader.Operation)
        {
            case FrameHeaderOperation.Unknown:
                break;
            case FrameHeaderOperation.Frame:
                var frameType = frameHeader.Type;
                switch (frameType)
                {
                    case "#commit":
                        var frameCommit = new FrameCommit(objects[1]);

                        // this.logger?.LogDebug($"FrameBody: {objects[1].ToJSONString()}");
                        message.Commit = frameCommit;
                        if (frameCommit.Blocks is null)
                        {
                            break;
                        }

                        var blocks = CarDecoder.DecodeCar(frameCommit.Blocks);
                        foreach (var block in blocks)
                        {
                            using var blockStream = new MemoryStream(block.Value);
                            var blockObj = CBORObject.Read(blockStream);
                            if (blockObj["$type"] is not null)
                            {
                                switch (blockObj["$type"].AsString())
                                {
                                    case Constants.FeedType.Post:
                                        message.Record = new Post(blockObj);
                                        break;
                                    case Constants.FeedType.Like:
                                        message.Record = new Like(blockObj);
                                        break;
                                    case Constants.FeedType.Generator:
                                        message.Record = new FeedGenerator(blockObj);
                                        break;
                                    case Constants.FeedType.Repost:
                                        message.Record = new Repost(blockObj);
                                        break;
                                    case Constants.GraphTypes.Follow:
                                        message.Record = new Follow(blockObj);
                                        break;
                                    case Constants.GraphTypes.List:
                                        message.Record = new BSList(blockObj);
                                        break;
                                    case Constants.GraphTypes.ListItem:
                                        message.Record = new BSListItem(blockObj);
                                        break;
                                    case Constants.GraphTypes.Block:
                                        message.Record = new Block(blockObj);
                                        break;
                                    case Constants.ActorTypes.Profile:
                                        message.Record = new Profile(blockObj);
                                        break;
                                    default:
                                        this.logger?.LogDebug($"WSS: Unknown Obj: {blockObj.ToJSONString()}");
                                        break;
                                }

                                // this.logger?.LogDebug($"FrameRecord: {blockObj.ToJSONString()}");
                            }
                            else if (blockObj["sig"] is not null)
                            {
                                message.Footer = new FrameFooter(blockObj);

                                // this.logger?.LogDebug($"FrameFooter: {blockObj.ToJSONString()}");
                            }
                            else
                            {
                                message.Nodes.Add(new FrameNode(blockObj));

                                // this.logger?.LogDebug($"FrameNode: {blockObj.ToJSONString()}");
                            }
                        }

                        break;
                    case "#handle":
                        var frameHandle = new FrameHandle(objects[1]);
                        message.Handle = frameHandle;
                        break;
                    case "#repoOp":
                        message.RepoOp = new FrameRepoOp(objects[1]);
                        break;
                    case "#info":
                        message.Info = new FrameInfo(objects[1]);
                        break;
                    case "#tombstone":
                        message.Tombstone = new FrameTombstone(objects[1]);
                        break;
                    case "#migrate":
                        message.Migrate = new FrameMigrate(objects[1]);
                        break;
                    default:
                        this.logger?.LogDebug($"Unknown Frame: {objects[1].ToJSONString()}");
                        break;
                }

                break;
            case FrameHeaderOperation.Error:
                var frameError = new FrameError(objects[1]);
                message.Error = frameError;
                this.logger?.LogError($"WSS: Error: {frameError.Message}");
                this.CloseAsync(WebSocketCloseStatus.InternalServerError, frameError.Message ?? string.Empty).FireAndForgetSafeAsync(this.logger);
                break;
            default:
                break;
        }

        this.protocol.OnSubscribedRepoMessageInternal(new SubscribedRepoEventArgs(message));
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

                Task.Run(() => this.HandleMessage(newArray));
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