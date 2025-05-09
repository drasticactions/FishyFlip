// <copyright file="ATWebSocketProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Buffers;
using System.IO.Pipelines;

namespace FishyFlip;

/// <summary>
/// AT WebSocket Protocol.
/// </summary>
public sealed class ATWebSocketProtocol : IDisposable
{
    private readonly TaskFactory taskFactory;
    private bool subscribedLabels;
    private WebSocketWrapper webSocketWrapper;
    private IReadOnlyList<ICustomATObjectCBORConverter> customConverters;
    private bool disposedValue;
    private ILogger? logger;
    private Uri instanceUri;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketProtocol"/> class.
    /// </summary>
    /// <param name="options"><see cref="ATWebSocketProtocolOptions"/>.</param>
    public ATWebSocketProtocol(ATWebSocketProtocolOptions options)
    {
        this.customConverters = options.CustomConverters;
        this.taskFactory = options.TaskFactory;
        this.logger = options.Logger;
        this.instanceUri = options.Url;
        this.webSocketWrapper = new WebSocketWrapper(this.logger);
        this.webSocketWrapper.OnMessageReceived += this.OnInternalMessageReceived;
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
    /// Event for when a subscribed label message is received.
    /// </summary>
    public event EventHandler<SubscribedLabelEventArgs>? OnSubscribedLabelMessage;

    /// <summary>
    /// Event for when a message is received.
    /// </summary>
    public event EventHandler<ReadOnlySequence<byte>>? OnMessageReceived;

    /// <summary>
    /// Gets a value indicating whether the object is disposed.
    /// </summary>
    public bool IsDisposed => this.disposedValue;

    /// <summary>
    /// Gets a value indicating whether ATProtocol is connected.
    /// </summary>
    public bool IsConnected => this.webSocketWrapper.IsConnected;

    /// <summary>
    /// Connect to the BlueSky instance via a WebSocket connection.
    /// </summary>
    /// <param name="connection">Connection string.</param>
    /// <param name="token">CancellationToken.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task ConnectAsync(string connection, CancellationToken? token = default)
    {
        if (this.IsConnected)
        {
            this.logger?.LogInformation("WSS: Already connected.");
            return;
        }

        var endToken = token ?? CancellationToken.None;
        await this.webSocketWrapper.ConnectAsync(new Uri($"wss://{this.instanceUri.Host}{connection}"), endToken);
        this.logger?.LogInformation($"WSS: Connected to {this.instanceUri}");
        this.OnConnectionUpdated?.Invoke(this, new SubscriptionConnectionStatusEventArgs(this.webSocketWrapper.State));
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
        if (!this.IsConnected)
        {
            return;
        }

        var endToken = token ?? CancellationToken.None;
        this.logger?.LogInformation($"WSS: Disconnecting");
        try
        {
            await this.webSocketWrapper.CloseAsync(endToken);
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Failed to Close WebSocket connection.");
        }

        this.OnConnectionUpdated?.Invoke(this, new SubscriptionConnectionStatusEventArgs(this.webSocketWrapper.State));
        this.subscribedLabels = false;
    }

    /// <summary>
    /// Start the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="cursor">The last known event seq number to backfill from.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartSubscribeReposAsync(long? cursor = default, CancellationToken? token = default)
    {
        var endPoint = "/xrpc/com.atproto.sync.subscribeRepos";
        if (cursor is not null)
        {
            endPoint += $"?cursor={cursor}";
        }

        return this.ConnectAsync(endPoint, token);
    }

    /// <summary>
    /// Start the ATProtocol SubscribeRepos sync session.
    /// </summary>
    /// <param name="cursor">The last known event seq number to backfill from.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task StartSubscribeLabelsAsync(long? cursor = default, CancellationToken? token = default)
    {
        this.subscribedLabels = true;
        var endPoint = "/xrpc/com.atproto.label.subscribeLabels";
        if (cursor is not null)
        {
            endPoint += $"?cursor={cursor}";
        }

        return this.ConnectAsync(endPoint, token);
    }

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
                this.webSocketWrapper.Dispose();
            }

            this.disposedValue = true;
        }
    }

    private void HandleMessage(ReadOnlySpan<byte> span)
    {
        byte[] byteArray = span.ToArray();
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

        var frameHeader = new FrameHeader(objects[0]);

        if (this.subscribedLabels)
        {
            this.HandleLabelMessage(frameHeader, objects[1]);
        }
        else
        {
            this.HandleRepoMessage(frameHeader, objects[1]);
        }
    }

    private void HandleLabelMessage(FrameHeader header, CBORObject obj)
    {
        var message = new SubscribeLabelMessage();
        message.Header = header;
        switch (header.Operation)
        {
            case FrameHeaderOperation.Unknown:
                break;
            case FrameHeaderOperation.Frame:
                var frameType = header.Type;
                switch (frameType)
                {
                    case "#labels":
                        message.Labels = new FishyFlip.Lexicon.Com.Atproto.Label.Labels(obj);
                        break;
                    case "#info":
                        message.Info = new FrameInfo(obj);
                        break;
                    default:
                        this.logger?.LogDebug($"Unknown Frame: {obj.ToJSONString()}");
                        break;
                }

                break;
            case FrameHeaderOperation.Error:
                var frameError = new FrameError(obj);
                message.Error = frameError;
                this.logger?.LogError($"WSS: ATError: {frameError.Message}");
                this.CloseAsync(WebSocketCloseStatus.InternalServerError, frameError.Message ?? string.Empty).FireAndForgetSafeAsync(this.logger);
                break;
            default:
                break;
        }

        this.OnSubscribedLabelMessage?.Invoke(this, new SubscribedLabelEventArgs(message));
    }

    private void HandleRepoMessage(FrameHeader header, CBORObject obj)
    {
        var message = new SubscribeRepoMessage();
        message.Header = header;
        switch (header.Operation)
        {
            case FrameHeaderOperation.Unknown:
                break;
            case FrameHeaderOperation.Frame:
                var frameType = header.Type;
                switch (frameType)
                {
                    case "#commit":
                        var frameCommit = new FrameCommit(obj, this.logger);
                        message.Commit = frameCommit;
                        if (frameCommit.Blocks is null)
                        {
                            break;
                        }

                        var records = new List<(Cid Cid, ATObject Value)>();
                        foreach (var e in CarDecoder.DecodeCar(frameCommit.Blocks))
                        {
                            using var blockStream = new MemoryStream(e.Bytes);
                            var blockObj = CBORObject.Read(blockStream);
                            if (blockObj["$type"] is not null)
                            {
                                var type = blockObj["$type"].AsString();
                                var record = blockObj.ToATObject(this.customConverters);
                                if (record is null)
                                {
                                    this.logger?.LogWarning($"WSS: ATError: Unable to decode record of type {type}");
                                    continue;
                                }

                                record.Type = type;
                                this.OnRecordReceived?.Invoke(this, new RecordMessageReceivedEventArgs(frameCommit, record));
                                records.Add((e.Cid, record));
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

                        if (records.Count > 0)
                        {
                            message.Records = records;
                        }

                        break;
                    case "#handle":
                        var frameHandle = new FrameHandle(obj);
                        message.Handle = frameHandle;
                        break;
                    case "#repoOp":
                        message.RepoOp = new FrameRepoOp(obj);
                        break;
                    case "#info":
                        message.Info = new FrameInfo(obj);
                        break;
                    case "#tombstone":
                        message.Tombstone = new FrameTombstone(obj);
                        break;
                    case "#migrate":
                        message.Migrate = new FrameMigrate(obj);
                        break;
                    case "#account":
                        message.Account = new FrameAccount(obj);
                        break;
                    case "#identity":
                        message.Identity = new FrameIdentity(obj);
                        break;
                    default:
                        this.logger?.LogDebug($"Unknown Frame: {obj.ToJSONString()}");
                        break;
                }

                break;
            case FrameHeaderOperation.Error:
                var frameError = new FrameError(obj);
                message.Error = frameError;
                this.logger?.LogError($"WSS: ATError: {frameError.Message}");
                this.CloseAsync(WebSocketCloseStatus.InternalServerError, frameError.Message ?? string.Empty).FireAndForgetSafeAsync(this.logger);
                break;
            default:
                break;
        }

        this.OnSubscribedRepoMessage?.Invoke(this, new SubscribedRepoEventArgs(message));
    }

    private Task OnInternalMessageReceived(ReadOnlySequence<byte> message)
    {
        this.OnMessageReceived?.Invoke(this, message);
        if (this.OnRecordReceived is not null || this.OnSubscribedRepoMessage is not null || this.OnSubscribedLabelMessage is not null)
        {
            var newMessage = message.ToArray();
            this.taskFactory.StartNew(() => this.HandleMessage(newMessage)).FireAndForgetSafeAsync(this.logger);
        }

        return Task.CompletedTask;
    }

    private class WebSocketWrapper : IDisposable
    {
        private readonly ClientWebSocket webSocket;
        private readonly Pipe pipe;
        private readonly CancellationTokenSource cts;
        private readonly ILogger? logger;

        public WebSocketWrapper(ILogger? logger)
        {
            this.webSocket = new ClientWebSocket();
            this.pipe = new Pipe();
            this.cts = new CancellationTokenSource();
            this.logger = logger;
        }

        public delegate Task MessageReceivedHandler(ReadOnlySequence<byte> message);

        public event MessageReceivedHandler? OnMessageReceived;

        public WebSocketState State => this.webSocket.State;

        public bool IsConnected => this.webSocket.State == WebSocketState.Open;

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

        public async Task CloseAsync(CancellationToken cancellationToken = default)
        {
            if (this.webSocket.State == WebSocketState.Closed || this.webSocket.State == WebSocketState.Aborted)
            {
                return;
            }

            this.logger?.LogInformation("WSS: Closing WebSocket connection.");
            await this.webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
        }

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
}