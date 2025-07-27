// <copyright file="IWebSocketClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Buffers;

namespace FishyFlip.Abstractions;

/// <summary>
/// Abstraction for WebSocket client functionality to enable testing.
/// </summary>
public interface IWebSocketClient : IDisposable
{
    /// <summary>
    /// Event triggered when a message is received from the WebSocket.
    /// </summary>
    event Func<ReadOnlySequence<byte>, Task>? OnMessageReceived;

    /// <summary>
    /// Gets the current state of the WebSocket connection.
    /// </summary>
    WebSocketState State { get; }

    /// <summary>
    /// Gets a value indicating whether the WebSocket is connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Connect to the specified URI asynchronously.
    /// </summary>
    /// <param name="uri">The URI to connect to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ConnectAsync(Uri uri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Close the WebSocket connection asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CloseAsync(CancellationToken cancellationToken = default);
}