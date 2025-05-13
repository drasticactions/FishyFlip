// <copyright file="DidDocInMemoryAsyncCacheTest.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FishyFlip.Tools.Tests;

/// <summary>
/// Unit tests for <see cref="DidDocInMemoryAsyncCache"/>.
/// </summary>
[TestClass]
public class DidDocInMemoryAsyncCacheTest
{
    /// <summary>
    /// SetAsync should add an item to the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task SetAsync_ShouldAddItemToCache()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("test.key")!;
        var value = new DidDoc();

        // Act
        var result = await cache.SetAsync(key, value);

        // Assert
        Assert.IsTrue(result);
        Assert.IsTrue(cache.DidDocCache.ContainsKey(key));
        Assert.AreEqual(value, cache.DidDocCache[key]);
    }

    /// <summary>
    /// GetAsync should return the item if it exists in the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task GetAsync_ShouldReturnItemIfExists()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("test.key")!;
        var value = new DidDoc();
        await cache.SetAsync(key, value);

        // Act
        var result = await cache.GetAsync(key);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(value, result);
    }

    /// <summary>
    /// GetAsync should return null if the item does not exist in the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task GetAsync_ShouldReturnNullIfItemDoesNotExist()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("non.existent.key")!;

        // Act
        var result = await cache.GetAsync(key);

        // Assert
        Assert.IsNull(result);
    }

    /// <summary>
    /// RemoveAsync should remove the item if it exists in the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task RemoveAsync_ShouldRemoveItemIfExists()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("test.key")!;
        var value = new DidDoc();
        await cache.SetAsync(key, value);

        // Act
        var result = await cache.RemoveAsync(key);

        // Assert
        Assert.IsTrue(result);
        Assert.IsFalse(cache.DidDocCache.ContainsKey(key));
    }

    /// <summary>
    /// RemoveAsync should return false if the item does not exist in the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task RemoveAsync_ShouldReturnFalseIfItemDoesNotExist()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("non.existent.key")!;

        // Act
        var result = await cache.RemoveAsync(key);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// ClearAsync should clear all items in the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task ClearAsync_ShouldClearAllItems()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key1 = ATIdentifier.Create("key.test")!;
        var key2 = ATIdentifier.Create("key.test.two")!;
        await cache.SetAsync(key1, new DidDoc());
        await cache.SetAsync(key2, new DidDoc());

        // Act
        await cache.ClearAsync();

        // Assert
        Assert.AreEqual(0, cache.DidDocCache.Count);
    }

    /// <summary>
    /// TryGetAsync should return true and the value if it exists in the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task TryGetAsync_ShouldReturnTrueAndValueIfExists()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("test.key")!;
        var value = new DidDoc();
        await cache.SetAsync(key, value);

        // Act
        var result = await cache.TryGetAsync(key, out var retrievedValue);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(value, retrievedValue);
    }

    /// <summary>
    /// TryGetAsync should return false if the item does not exist in the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task TryGetAsync_ShouldReturnFalseIfItemDoesNotExist()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("non.existent.key")!;

        // Act
        var result = await cache.TryGetAsync(key, out var retrievedValue);

        // Assert
        Assert.IsFalse(result);
        Assert.IsNull(retrievedValue);
    }

    /// <summary>
    /// DisposeAsync should clear the cache.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task DisposeAsync_ShouldClearCache()
    {
        // Arrange
        var cache = new DidDocInMemoryAsyncCache();
        var key = ATIdentifier.Create("test.key")!;
        await cache.SetAsync(key, new DidDoc());

        // Act
        await cache.DisposeAsync();

        // Assert
        Assert.AreEqual(0, cache.DidDocCache.Count);
    }
}