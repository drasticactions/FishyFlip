// <copyright file="CarDecoderTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;

namespace FishyFlip.Tests;

/// <summary>
/// Tests for CarDecoder functionality.
/// </summary>
[TestClass]
public class CarDecoderTests
{
    private static byte[]? testRepoData;

    /// <summary>
    /// Load test.repo data once for all tests.
    /// </summary>
    /// <param name="context">Test context.</param>
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var testRepoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "test.repo");
        testRepoData = File.ReadAllBytes(testRepoPath);
    }

    /// <summary>
    /// Test that test.repo file exists and has data.
    /// </summary>
    [TestMethod]
    public void CarDecoder_TestRepoFile_ShouldExistAndHaveData()
    {
        // Assert
        Assert.IsNotNull(testRepoData);
        Assert.IsTrue(testRepoData.Length > 0);
    }

    /// <summary>
    /// Test DecodeCar with test.repo data returns FrameEvents.
    /// </summary>
    [TestMethod]
    public void CarDecoder_DecodeCar_ShouldReturnFrameEvents()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);

        // Act
        var frameEvents = CarDecoder.DecodeCar(testRepoData).ToList();

        // Assert
        Assert.IsNotNull(frameEvents);
        Assert.IsTrue(frameEvents.Count > 0);

        foreach (var frameEvent in frameEvents)
        {
            Assert.IsNotNull(frameEvent);
            Assert.IsNotNull(frameEvent.Cid);
            Assert.IsNotNull(frameEvent.Bytes);
            Assert.IsTrue(frameEvent.Bytes.Length > 0);
        }
    }

    /// <summary>
    /// Test DecodeRepo with test.repo data returns ATObjects.
    /// </summary>
    [TestMethod]
    public void CarDecoder_DecodeRepo_ShouldReturnATObjects()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);

        // Act
        var atObjects = CarDecoder.DecodeRepo(testRepoData).ToList();

        // Assert
        Assert.IsNotNull(atObjects);

        foreach (var atObject in atObjects)
        {
            Assert.IsNotNull(atObject);
            Assert.IsNotNull(atObject.Type);
        }
    }

    /// <summary>
    /// Test DecodeCarAsync with test.repo data returns FrameEvents.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    [TestMethod]
    public async Task CarDecoder_DecodeCarAsync_ShouldReturnFrameEvents()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);
        using var stream = new MemoryStream(testRepoData);

        // Act
        var frameEvents = new List<FrameEvent>();
        await foreach (var frameEvent in CarDecoder.DecodeCarAsync(stream))
        {
            frameEvents.Add(frameEvent);
        }

        // Assert
        Assert.IsNotNull(frameEvents);
        Assert.IsTrue(frameEvents.Count > 0);

        foreach (var frameEvent in frameEvents)
        {
            Assert.IsNotNull(frameEvent);
            Assert.IsNotNull(frameEvent.Cid);
            Assert.IsNotNull(frameEvent.Bytes);
            Assert.IsTrue(frameEvent.Bytes.Length > 0);
        }
    }

    /// <summary>
    /// Test DecodeRepoAsync with test.repo data returns ATObjects.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    [TestMethod]
    public async Task CarDecoder_DecodeRepoAsync_ShouldReturnATObjects()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);
        using var stream = new MemoryStream(testRepoData);

        // Act
        var atObjects = new List<ATObject>();
        await foreach (var atObject in CarDecoder.DecodeRepoAsync(stream))
        {
            atObjects.Add(atObject);
        }

        // Assert
        Assert.IsNotNull(atObjects);

        foreach (var atObject in atObjects)
        {
            Assert.IsNotNull(atObject);
            Assert.IsNotNull(atObject.Type);
        }
    }

    /// <summary>
    /// Test obsolete DecodeCarAsync with progress callback.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    [TestMethod]
    public async Task CarDecoder_DecodeCarAsync_WithProgress_ShouldInvokeCallback()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);
        using var stream = new MemoryStream(testRepoData);
        var progressEvents = new List<CarProgressStatusEvent>();

        void OnProgress(CarProgressStatusEvent e)
        {
            progressEvents.Add(e);
        }

        // Act
#pragma warning disable CS0618 // Type or member is obsolete
        await CarDecoder.DecodeCarAsync(stream, OnProgress);
#pragma warning restore CS0618 // Type or member is obsolete

        // Assert
        Assert.IsTrue(progressEvents.Count > 0);

        foreach (var progressEvent in progressEvents)
        {
            Assert.IsNotNull(progressEvent);
            Assert.IsNotNull(progressEvent.Cid);
            Assert.IsNotNull(progressEvent.Bytes);
            Assert.IsTrue(progressEvent.Bytes.Length > 0);
        }
    }

    /// <summary>
    /// Test DecodeCar with empty byte array throws appropriate exception.
    /// </summary>
    [TestMethod]
    public void CarDecoder_DecodeCar_WithEmptyBytes_ShouldThrow()
    {
        // Arrange
        var emptyBytes = Array.Empty<byte>();

        // Act & Assert
        Assert.ThrowsException<InvalidDataException>(() => CarDecoder.DecodeCar(emptyBytes).ToList());
    }

    /// <summary>
    /// Test DecodeRepo with empty byte array throws appropriate exception.
    /// </summary>
    [TestMethod]
    public void CarDecoder_DecodeRepo_WithEmptyBytes_ShouldThrow()
    {
        // Arrange
        var emptyBytes = Array.Empty<byte>();

        // Act & Assert
        Assert.ThrowsException<InvalidDataException>(() => CarDecoder.DecodeRepo(emptyBytes).ToList());
    }

    /// <summary>
    /// Test DecodeCarAsync with empty stream completes without error.
    /// </summary>
    /// <returns>Task representing the asynchronous operation.</returns>
    [TestMethod]
    public async Task CarDecoder_DecodeCarAsync_WithEmptyStream_ShouldCompleteWithoutError()
    {
        // Arrange
        using var emptyStream = new MemoryStream();

        // Act
        var frameEvents = new List<FrameEvent>();
        await foreach (var frameEvent in CarDecoder.DecodeCarAsync(emptyStream))
        {
            frameEvents.Add(frameEvent);
        }

        // Assert
        Assert.AreEqual(0, frameEvents.Count);
    }

    /// <summary>
    /// Test DecodeRepoAsync with empty stream completes without error.
    /// </summary>
    [TestMethod]
    public async Task CarDecoder_DecodeRepoAsync_WithEmptyStream_ShouldCompleteWithoutError()
    {
        // Arrange
        using var emptyStream = new MemoryStream();

        // Act
        var atObjects = new List<ATObject>();
        await foreach (var atObject in CarDecoder.DecodeRepoAsync(emptyStream))
        {
            atObjects.Add(atObject);
        }

        // Assert
        Assert.AreEqual(0, atObjects.Count);
    }

    /// <summary>
    /// Test that DecodeCar and DecodeCarAsync return consistent results.
    /// </summary>
    [TestMethod]
    public async Task CarDecoder_SyncAndAsyncMethods_ShouldReturnConsistentResults()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);
        using var stream = new MemoryStream(testRepoData);

        // Act
        var syncFrameEvents = CarDecoder.DecodeCar(testRepoData).ToList();

        var asyncFrameEvents = new List<FrameEvent>();
        await foreach (var frameEvent in CarDecoder.DecodeCarAsync(stream))
        {
            asyncFrameEvents.Add(frameEvent);
        }

        // Assert
        Assert.AreEqual(syncFrameEvents.Count, asyncFrameEvents.Count);

        for (int i = 0; i < syncFrameEvents.Count; i++)
        {
            Assert.AreEqual(syncFrameEvents[i].Cid, asyncFrameEvents[i].Cid);
            CollectionAssert.AreEqual(syncFrameEvents[i].Bytes, asyncFrameEvents[i].Bytes);
        }
    }

    /// <summary>
    /// Test that DecodeRepo and DecodeRepoAsync return consistent results.
    /// </summary>
    [TestMethod]
    public async Task CarDecoder_SyncAndAsyncRepoMethods_ShouldReturnConsistentResults()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);
        using var stream = new MemoryStream(testRepoData);

        // Act
        var syncATObjects = CarDecoder.DecodeRepo(testRepoData).ToList();

        var asyncATObjects = new List<ATObject>();
        await foreach (var atObject in CarDecoder.DecodeRepoAsync(stream))
        {
            asyncATObjects.Add(atObject);
        }

        // Assert
        Assert.AreEqual(syncATObjects.Count, asyncATObjects.Count);

        for (int i = 0; i < syncATObjects.Count; i++)
        {
            Assert.AreEqual(syncATObjects[i].Type, asyncATObjects[i].Type);
        }
    }

    /// <summary>
    /// Test FrameEvent toString method.
    /// </summary>
    [TestMethod]
    public void CarDecoder_FrameEvent_ToString_ShouldReturnExpectedFormat()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);
        var frameEvents = CarDecoder.DecodeCar(testRepoData).Take(1).ToList();
        Assert.IsTrue(frameEvents.Count > 0);

        var frameEvent = frameEvents[0];

        // Act
        var result = frameEvent.ToString();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Contains(frameEvent.Cid.ToString()));
        Assert.IsTrue(result.Contains(frameEvent.Bytes.Length.ToString()));
    }

    /// <summary>
    /// Test that CarDecoder handles malformed CAR data gracefully.
    /// </summary>
    [TestMethod]
    public void CarDecoder_DecodeCar_WithMalformedData_ShouldThrow()
    {
        // Arrange
        var malformedData = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        // Act & Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => CarDecoder.DecodeCar(malformedData).ToList());
    }

    /// <summary>
    /// Test that stream position is correctly managed in async methods.
    /// </summary>
    [TestMethod]
    public async Task CarDecoder_DecodeCarAsync_ShouldConsumeEntireStream()
    {
        // Arrange
        Assert.IsNotNull(testRepoData);
        using var stream = new MemoryStream(testRepoData);
        var initialPosition = stream.Position;

        // Act
        var frameEvents = new List<FrameEvent>();
        await foreach (var frameEvent in CarDecoder.DecodeCarAsync(stream))
        {
            frameEvents.Add(frameEvent);
        }

        // Assert
        Assert.IsTrue(frameEvents.Count > 0);
        Assert.IsTrue(stream.Position > initialPosition);
    }
}