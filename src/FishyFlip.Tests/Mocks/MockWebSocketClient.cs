// <copyright file="MockWebSocketClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Buffers;

namespace FishyFlip.Tests.Mocks;

/// <summary>
/// Mock WebSocket client for testing ATWebSocketProtocol.
/// </summary>
public class MockWebSocketClient : IWebSocketClient
{
    private readonly Queue<byte[]> messageQueue = new();
    private readonly TaskCompletionSource connectionTcs = new();
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MockWebSocketClient"/> class.
    /// </summary>
    public MockWebSocketClient()
    {
        this.State = WebSocketState.None;
    }

    /// <inheritdoc/>
    public event Func<ReadOnlySequence<byte>, Task>? OnMessageReceived;

    /// <inheritdoc/>
    public WebSocketState State { get; private set; }

    /// <inheritdoc/>
    public bool IsConnected => this.State == WebSocketState.Open;

    /// <summary>
    /// Gets the number of messages in the queue.
    /// </summary>
    public int MessageCount => this.messageQueue.Count;

    /// <summary>
    /// Gets the last URI that was connected to.
    /// </summary>
    public Uri? LastConnectedUri { get; private set; }

    /// <inheritdoc/>
    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        if (this.State == WebSocketState.Open)
        {
            return;
        }

        this.LastConnectedUri = uri;
        this.State = WebSocketState.Connecting;

        // Simulate connection delay
        await Task.Delay(10, cancellationToken);

        this.State = WebSocketState.Open;
        this.connectionTcs.SetResult();
    }

    /// <inheritdoc/>
    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        if (this.State == WebSocketState.Closed || this.State == WebSocketState.Aborted)
        {
            return;
        }

        this.State = WebSocketState.CloseSent;

        // Simulate close delay
        await Task.Delay(5, cancellationToken);

        this.State = WebSocketState.Closed;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (this.isDisposed)
        {
            return;
        }

        this.State = WebSocketState.Aborted;
        this.messageQueue.Clear();
        this.isDisposed = true;
    }

    /// <summary>
    /// Simulates receiving a message from the WebSocket.
    /// </summary>
    /// <param name="message">The message bytes to simulate receiving.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SimulateMessageReceived(byte[] message)
    {
        if (this.State != WebSocketState.Open)
        {
            throw new InvalidOperationException("WebSocket is not open");
        }

        if (this.OnMessageReceived != null)
        {
            var sequence = new ReadOnlySequence<byte>(message);
            await this.OnMessageReceived(sequence);
        }
    }

    /// <summary>
    /// Simulates multiple messages being received.
    /// </summary>
    /// <param name="messages">The messages to simulate receiving.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SimulateMultipleMessages(params byte[][] messages)
    {
        foreach (var message in messages)
        {
            await this.SimulateMessageReceived(message);
            await Task.Delay(1); // Small delay between messages
        }
    }

    /// <summary>
    /// Waits for the connection to be established.
    /// </summary>
    /// <param name="timeout">Maximum time to wait.</param>
    /// <returns>A task that completes when connected or times out.</returns>
    public async Task WaitForConnectionAsync(TimeSpan timeout = default)
    {
        if (timeout == default)
        {
            timeout = TimeSpan.FromSeconds(5);
        }

        using var cts = new CancellationTokenSource(timeout);
        try
        {
            await this.connectionTcs.Task.WaitAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("Connection timeout");
        }
    }

    /// <summary>
    /// Simulates a connection error.
    /// </summary>
    /// <param name="exception">The exception to simulate.</param>
    public void SimulateConnectionError(Exception exception)
    {
        this.State = WebSocketState.Aborted;
        this.connectionTcs.SetException(exception);
    }

    /// <summary>
    /// Resets the mock to its initial state.
    /// </summary>
    public void Reset()
    {
        this.State = WebSocketState.None;
        this.messageQueue.Clear();
        this.LastConnectedUri = null;
    }
}