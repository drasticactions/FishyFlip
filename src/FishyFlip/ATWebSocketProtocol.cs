// <copyright file="ATWebSocketProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT WebSocket Protocol.
/// </summary>
public sealed class ATWebSocketProtocol : IDisposable
{
    private const int ReceiveBufferSize = 32768;
    private ClientWebSocket client;
    private bool disposedValue;
    private ILogger? logger;
    private Uri instanceUri;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketProtocol"/> class.
    /// </summary>
    /// <param name="options"><see cref="ATWebSocketProtocolOptions"/>.</param>
    public ATWebSocketProtocol(ATWebSocketProtocolOptions options)
    {
        this.logger = options.Logger;
        this.instanceUri = options.Url;
        this.client = new ClientWebSocket();
    }

    /// <summary>
    /// Event triggered when a record is received.
    /// </summary>
    /// <remarks>
    /// This event is used to notify subscribers when a new record is received.
    /// The subscribers can be any component that needs to perform an action when a new record arrives.
    /// </remarks>
    public event EventHandler<RecordMessageReceivedEventArgs>? OnRecordReceived;

    /// <summary>
    /// On Connection Updated.
    /// </summary>
    public event EventHandler<SubscriptionConnectionStatusEventArgs>? OnConnectionUpdated;

    /// <summary>
    /// Event for when a subscribed repo message is received.
    /// </summary>
    public event EventHandler<SubscribedRepoEventArgs>? OnSubscribedRepoMessage;

    /// <summary>
    /// Gets a value indicating whether the object is disposed.
    /// </summary>
    public bool IsDisposed => this.disposedValue;

    /// <summary>
    /// Gets a value indicating whether ATProtocol is connected.
    /// </summary>
    public bool IsConnected => this.client.State == WebSocketState.Open;

    /// <summary>
    /// Connect to the BlueSky instance via a WebSocket connection.
    /// </summary>
    /// <param name="connection">Connection string.</param>
    /// <param name="token">CancellationToken.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task ConnectAsync(string connection, CancellationToken? token = default)
    {
        if (this.client.State == WebSocketState.Open)
        {
            return;
        }

        if (this.client.State == WebSocketState.Aborted || this.client.State == WebSocketState.Closed)
        {
            this.client = new ClientWebSocket();
        }

        var endToken = token ?? CancellationToken.None;
        await this.client.ConnectAsync(new Uri($"wss://{this.instanceUri.Host}{connection}"), endToken);
        this.logger?.LogInformation($"WSS: Connected to {this.instanceUri}");
        this.ReceiveMessages(this.client, endToken).FireAndForgetSafeAsync(this.logger);
        this.OnConnectionUpdated?.Invoke(this, new SubscriptionConnectionStatusEventArgs(this.client.State));
    }

    /// <summary>
    /// Close the existing WebSocket connection.
    /// </summary>
    /// <param name="status">Status for the shutdown. Defaults to <see cref="WebSocketCloseStatus.NormalClosure"/>.</param>
    /// <param name="disconnectReason">Reason for the shutdown.</param>
    /// <param name="token">CancellationToken.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task CloseAsync(WebSocketCloseStatus status = WebSocketCloseStatus.NormalClosure, string disconnectReason = "Client disconnecting", CancellationToken? token = default)
    {
        var endToken = token ?? CancellationToken.None;
        this.logger?.LogInformation($"WSS: Disconnecting");
        try
        {
            await this.client.CloseAsync(status, disconnectReason, endToken);
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Failed to Close WebSocket connection.");
        }

        this.OnConnectionUpdated?.Invoke(this, new SubscriptionConnectionStatusEventArgs(this.client.State));
    }

    /// <summary>
    /// Start the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartSubscribeReposAsync(CancellationToken? token = default)
        => this.ConnectAsync("/xrpc/com.atproto.sync.subscribeRepos", token);

    /// <summary>
    /// Start the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartSubscribeLabelsAsync(CancellationToken? token = default)
        => this.ConnectAsync("/xrpc/com.atproto.label.subscribeLabels", token);

    /// <summary>
    /// Stops the ATProtocol Subscription session.
    /// </summary>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StopSubscriptionAsync(CancellationToken? token = default)
    {
        if (this.IsConnected)
        {
            return this.CloseAsync(token: token);
        }

        return Task.CompletedTask;
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
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">Is Disposing.</param>
    private void Dispose(bool disposing)
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
        if (byteArray.Length == 0)
        {
            this.logger?.LogDebug("WSS: ATError reading message. Empty byte array.");
            return;
        }

        CBORObject[]? objects = null;
        try
        {
            objects = CBORObject.DecodeSequenceFromBytes(byteArray, new CBOREncodeOptions("useIndefLengthStrings=true;float64=true;allowduplicatekeys=true;allowEmpty=true"));
        }
        catch (Exception e)
        {
            this.logger?.LogError(e, "WSS: ATError reading message.");
        }

        if (objects is null)
        {
            return;
        }

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
                        var frameCommit = new FrameCommit(objects[1], this.logger);

                        // this.logger?.LogDebug($"FrameBody: {objects[1].ToJSONString()}");
                        message.Commit = frameCommit;
                        if (frameCommit.Blocks is null)
                        {
                            break;
                        }

                        void HandleProgressStatus(CarProgressStatusEvent e)
                        {
                            using var blockStream = new MemoryStream(e.Bytes);
                            var blockObj = CBORObject.Read(blockStream);
                            if (blockObj["$type"] is not null)
                            {
                                message.Record = blockObj.ToATObject();
                                this.OnRecordReceived?.Invoke(this, new RecordMessageReceivedEventArgs(frameCommit, message.Record));
                            }
                            else if (blockObj["sig"] is not null)
                            {
                                message.Footer = FrameFooter.FromCBORObject(blockObj, this.logger);
                            }
                            else
                            {
                                message.Nodes.Add(new FrameNode(blockObj));
                            }
                        }

                        CarDecoder.DecodeCar(frameCommit.Blocks, HandleProgressStatus);

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
                    case "#account":
                        message.Account = new FrameAccount(objects[1]);
                        break;
                    case "#identity":
                        message.Identity = new FrameIdentity(objects[1]);
                        break;
                    default:
                        this.logger?.LogDebug($"Unknown Frame: {objects[1].ToJSONString()}");
                        break;
                }

                break;
            case FrameHeaderOperation.Error:
                var frameError = new FrameError(objects[1]);
                message.Error = frameError;
                this.logger?.LogError($"WSS: ATError: {frameError.Message}");
                this.CloseAsync(WebSocketCloseStatus.InternalServerError, frameError.Message ?? string.Empty).FireAndForgetSafeAsync(this.logger);
                break;
            default:
                break;
        }

        this.OnSubscribedRepoMessage?.Invoke(this, new SubscribedRepoEventArgs(message));
    }

    private async Task ReceiveMessages(ClientWebSocket webSocket, CancellationToken token)
    {
        const int InitialBufferSize = 4096;
        using var memoryStream = new MemoryStream();

        while (webSocket.State == WebSocketState.Open)
        {
            try
            {
                var messageBuffer = new byte[InitialBufferSize];
                WebSocketReceiveResult result;

                // Keep receiving until we get a complete message
                do
                {
#if NETSTANDARD
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(messageBuffer), token);
#else
                    result = await webSocket.ReceiveAsync(messageBuffer, token);
#endif

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", token);
                        break;
                    }

                    // Write received data to memory stream
                    await memoryStream.WriteAsync(messageBuffer, 0, result.Count, token);
                }
                while (!result.EndOfMessage);

                // Process the complete message
                if (memoryStream.Length > 0)
                {
                    byte[] completeMessage = memoryStream.ToArray();

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Binary:
                            this.HandleMessage(completeMessage);
                            break;

                        case WebSocketMessageType.Text:
                            string textMessage = Encoding.UTF8.GetString(completeMessage);
                            this.HandleTextMessage(textMessage);
                            break;
                    }

                    // Reset stream for next message
                    memoryStream.SetLength(0);
                }
            }
            catch (OperationCanceledException)
            {
                this.logger?.LogDebug("WSS: Operation Canceled.");
                break;
            }
            catch (WebSocketException wsEx)
            {
                this.logger?.LogError(wsEx, "WSS: WebSocket error occurred.");
                break;
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, "WSS: Error receiving message.");

                await Task.Delay(1000, token);
            }
        }

        // Connection status update
        this.OnConnectionUpdated?.Invoke(this, new SubscriptionConnectionStatusEventArgs(webSocket.State));
    }

    private void HandleTextMessage(string message)
    {
        try
        {
            this.logger?.LogDebug($"WSS: Received text message: {message}");
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "WSS: Error handling text message");
        }
    }
}