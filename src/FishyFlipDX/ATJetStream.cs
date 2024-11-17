// <copyright file="ATJetStream.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Events;
using FishyFlip.Lexicon;
using FishyFlip.Tools.Json;

namespace FishyFlip;

/// <summary>
/// AT JetStream.
/// </summary>
public sealed class ATJetStream : IDisposable
{
    private const int ReceiveBufferSize = 32768;
    private readonly JsonSerializerOptions jsonSerializerOptions;
    private readonly SourceGenerationContext sourceGenerationContext;
    private ClientWebSocket client;
    private bool disposedValue;
    private ILogger? logger;
    private Uri instanceUri;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATJetStream"/> class.
    /// </summary>
    /// <param name="options"><see cref="ATJetStreamOptions"/>.</param>
    public ATJetStream(ATJetStreamOptions options)
    {
        this.logger = options.Logger;
        this.instanceUri = options.Url;
        this.client = new ClientWebSocket();
        this.jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
            Converters = { new ATUriJsonConverter() },
        };
        this.sourceGenerationContext = new SourceGenerationContext(this.jsonSerializerOptions);
    }

    /// <summary>
    /// On Connection Updated.
    /// </summary>
    public event EventHandler<SubscriptionConnectionStatusEventArgs>? OnConnectionUpdated;

    /// <summary>
    /// On Raw Message Received.
    /// </summary>
    public event EventHandler<JetStreamRawMessageEventArgs>? OnRawMessageReceived;

    /// <summary>
    /// On AT WebSocket Record Received.
    /// </summary>
    public event EventHandler<JetStreamATWebSocketRecordEventArgs>? OnRecordReceived;

    /// <inheritdoc/>
    void IDisposable.Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Connect to the JetStream instance via a WebSocket connection.
    /// </summary>
    /// <param name="wantedCollections">List of collection namespaces (ex. app.bsky.feed.post) you want to receive. Defaults to all.</param>
    /// <param name="wantedDids">List of User ATDids to filter for. Defaults to All Repos.</param>
    /// <param name="cursor">A unix microseconds timestamp cursor to begin playback from. Set the value from a previous <see cref="ATWebSocketRecord.TimeUs"/> value to start stream from this point. Defaults to live tail.</param>
    /// <param name="token">CancellationToken.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task ConnectAsync(string[]? wantedCollections = default, string[]? wantedDids = default, long cursor = 0, CancellationToken? token = default)
    {
        var subscribe = "/subscribe?";
        if (wantedCollections is not null && wantedCollections.Length > 0)
        {
            foreach (var collection in wantedCollections)
            {
                subscribe += $"wantedCollections={collection}&";
            }
        }

        if (wantedDids is not null && wantedDids.Length > 0)
        {
            foreach (var did in wantedDids)
            {
                subscribe += $"wantedDids={did}&";
            }
        }

        if (cursor > 0)
        {
            subscribe += $"cursor={cursor}&";
        }

        if (subscribe.EndsWith("&"))
        {
            subscribe = subscribe.Substring(0, subscribe.Length - 1);
        }

        this.logger?.LogInformation($"WSS: Connecting to {this.instanceUri}{subscribe}");

        return this.ConnectAsync(subscribe, token);
    }

    /// <summary>
    /// Connect to the JetStream instance via a WebSocket connection.
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
    /// Dispose.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private async Task ReceiveMessages(ClientWebSocket webSocket, CancellationToken token)
    {
        byte[] receiveBuffer = new byte[ReceiveBufferSize];
        while (webSocket.State == WebSocketState.Open)
        {
            try
            {
#if NETSTANDARD
                var result =
                    await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), token);
                if (result is not { MessageType: WebSocketMessageType.Text, EndOfMessage: true })
                {
                    continue;
                }

                byte[] newArray = new byte[result.Count];
                Array.Copy(receiveBuffer, 0, newArray, 0, result.Count);
#else
                var result =
                    await webSocket.ReceiveAsync(new Memory<byte>(receiveBuffer), token);
                if (result is not { MessageType: WebSocketMessageType.Text, EndOfMessage: true })
                {
                    continue;
                }

                // Convert result to string
                byte[] newArray = new byte[result.Count];
                Array.Copy(receiveBuffer, 0, newArray, 0, result.Count);
#endif
                var message = Encoding.UTF8.GetString(newArray);
                this.OnRawMessageReceived?.Invoke(this, new JetStreamRawMessageEventArgs(message));
                Task.Run(() => this.HandleMessage(message)).FireAndForgetSafeAsync(this.logger);
            }
            catch (OperationCanceledException)
            {
                this.logger?.LogDebug("WSS: Operation Canceled.");
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, "WSS: ATError receiving message.");
            }
        }

        this.OnConnectionUpdated?.Invoke(this, new SubscriptionConnectionStatusEventArgs(webSocket.State));
    }

    private void HandleMessage(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            this.logger?.LogDebug("WSS: Empty message received.");
            return;
        }

        try
        {
            var atWebSocketRecord = JsonSerializer.Deserialize<ATWebSocketRecord>(json, this.sourceGenerationContext.ATWebSocketRecord);
            if (atWebSocketRecord is null)
            {
                this.logger?.LogError("WSS: Failed to deserialize ATWebSocketRecord.");
                this.logger?.LogError(json);
                return;
            }

            this.OnRecordReceived?.Invoke(this, new JetStreamATWebSocketRecordEventArgs(atWebSocketRecord, json));
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "WSS: Failed to deserialize ATWebSocketRecord.");
            this.logger?.LogError(json);
        }
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
}