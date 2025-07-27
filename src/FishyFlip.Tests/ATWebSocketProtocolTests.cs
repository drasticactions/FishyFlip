// <copyright file="ATWebSocketProtocolTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Buffers;
using FishyFlip.Tests.Mocks;

namespace FishyFlip.Tests;

/// <summary>
/// Tests for ATWebSocketProtocol functionality.
/// </summary>
[TestClass]
public class ATWebSocketProtocolTests
{
    /// <summary>
    /// Test that connection state is properly managed.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_ConnectAsync_ShouldUpdateConnectionState()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);

        // Act
        await protocol.ConnectAsync("/test");

        // Assert
        Assert.IsTrue(protocol.IsConnected);
        Assert.AreEqual(WebSocketState.Open, mockWebSocket.State);
        Assert.IsNotNull(mockWebSocket.LastConnectedUri);
        Assert.IsTrue(mockWebSocket.LastConnectedUri!.ToString().Contains("/test"));
    }

    /// <summary>
    /// Test that disconnection works properly.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_CloseAsync_ShouldUpdateConnectionState()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);
        await protocol.ConnectAsync("/test");

        // Act
        await protocol.CloseAsync();

        // Assert
        Assert.IsFalse(protocol.IsConnected);
        Assert.AreEqual(WebSocketState.Closed, mockWebSocket.State);
    }

    /// <summary>
    /// Test that subscription endpoints are constructed correctly.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_StartSubscribeReposAsync_ShouldConnectToCorrectEndpoint()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);

        // Act
        await protocol.StartSubscribeReposAsync();

        // Assert
        Assert.IsTrue(protocol.IsConnected);
        Assert.IsNotNull(mockWebSocket.LastConnectedUri);
        Assert.IsTrue(mockWebSocket.LastConnectedUri!.ToString().Contains("subscribeRepos"));
    }

    /// <summary>
    /// Test that subscription with cursor parameter works.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_StartSubscribeReposAsync_WithCursor_ShouldIncludeCursorInUrl()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);
        const long testCursor = 12345;

        // Act
        await protocol.StartSubscribeReposAsync(testCursor);

        // Assert
        Assert.IsTrue(protocol.IsConnected);
        Assert.IsNotNull(mockWebSocket.LastConnectedUri);
        Assert.IsTrue(mockWebSocket.LastConnectedUri!.ToString().Contains($"cursor={testCursor}"));
    }

    /// <summary>
    /// Test that labels subscription endpoint is constructed correctly.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_StartSubscribeLabelsAsync_ShouldConnectToCorrectEndpoint()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);

        // Act
        await protocol.StartSubscribeLabelsAsync();

        // Assert
        Assert.IsTrue(protocol.IsConnected);
        Assert.IsNotNull(mockWebSocket.LastConnectedUri);
        Assert.IsTrue(mockWebSocket.LastConnectedUri!.ToString().Contains("subscribeLabels"));
    }

    /// <summary>
    /// Test that connection events are fired properly.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_ConnectionEvents_ShouldFireCorrectly()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);
        var connectionEvents = new List<SubscriptionConnectionStatusEventArgs>();

        protocol.OnConnectionUpdated += (sender, args) => connectionEvents.Add(args);

        // Act
        await protocol.ConnectAsync("/test");
        await protocol.CloseAsync();

        // Assert
        Assert.AreEqual(2, connectionEvents.Count);
        Assert.AreEqual(WebSocketState.Open, connectionEvents[0].State);
        Assert.AreEqual(WebSocketState.Closed, connectionEvents[1].State);
    }

    /// <summary>
    /// Test that already connected calls don't create duplicate connections.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_ConnectAsync_WhenAlreadyConnected_ShouldNotReconnect()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);
        await protocol.ConnectAsync("/test1");
        var firstUri = mockWebSocket.LastConnectedUri;

        // Act
        await protocol.ConnectAsync("/test2");

        // Assert
        Assert.IsTrue(protocol.IsConnected);
        Assert.AreEqual(firstUri, mockWebSocket.LastConnectedUri); // Should be unchanged
    }

    /// <summary>
    /// Test that stop subscription works when connected.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_StopSubscriptionAsync_WhenConnected_ShouldDisconnect()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);
        await protocol.ConnectAsync("/test");

        // Act
        await protocol.StopSubscriptionAsync();

        // Assert
        Assert.IsFalse(protocol.IsConnected);
        Assert.AreEqual(WebSocketState.Closed, mockWebSocket.State);
    }

    /// <summary>
    /// Test that stop subscription is safe when not connected.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_StopSubscriptionAsync_WhenNotConnected_ShouldCompleteGracefully()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);

        // Act & Assert - Should not throw
        await protocol.StopSubscriptionAsync();

        Assert.IsFalse(protocol.IsConnected);
    }

    /// <summary>
    /// Test that message received event is triggered.
    /// </summary>
    [TestMethod]
    public async Task ATWebSocketProtocol_OnMessageReceived_ShouldFireWhenMessageReceived()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);
        var receivedMessages = new List<ReadOnlySequence<byte>>();

        protocol.OnMessageReceived += (sender, message) =>
        {
            receivedMessages.Add(message);
        };

        await protocol.ConnectAsync("/test");

        // Act
        var testMessage = new byte[] { 1, 2, 3, 4, 5 };
        await mockWebSocket.SimulateMessageReceived(testMessage);

        // Give a moment for async processing
        await Task.Delay(50);

        // Assert
        Assert.AreEqual(1, receivedMessages.Count);
        var receivedBytes = receivedMessages[0].ToArray();
        CollectionAssert.AreEqual(testMessage, receivedBytes);
    }

    /// <summary>
    /// Test disposal works correctly.
    /// </summary>
    [TestMethod]
    public void ATWebSocketProtocol_Dispose_ShouldDisposeWebSocketClient()
    {
        // Arrange
        var mockWebSocket = new MockWebSocketClient();
        var options = new ATWebSocketProtocolOptions();
        var protocol = new ATWebSocketProtocol(options, mockWebSocket);

        // Act
        protocol.Dispose();

        // Assert
        Assert.IsTrue(protocol.IsDisposed);
        Assert.AreEqual(WebSocketState.Aborted, mockWebSocket.State);
    }
}