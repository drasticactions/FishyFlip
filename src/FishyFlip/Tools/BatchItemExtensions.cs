// <copyright file="BatchItemExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

namespace FishyFlip;

/// <summary>
/// Extensions for <see cref="IBatchItem" />.
/// Taken from https://github.com/Tyrrrz/YoutubeExplode/blob/master/YoutubeExplode/Common/IBatchItem.cs.
/// </summary>
public static class BatchItemExtensions
{
    /// <summary>
    /// Collects all elements of the specified asynchronous sequence into a read-only list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence, which must implement <see cref="IBatchItem"/>.</typeparam>
    /// <param name="source">The asynchronous sequence to collect elements from.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of the collected elements.</returns>
    public static async ValueTask<IReadOnlyList<T>> CollectAsync<T>(this IAsyncEnumerable<T> source)
        where T : IBatchItem => await source.ToListAsync();

    /// <summary>
    /// Collects a specified number of items from an asynchronous enumerable sequence and returns them as a read-only list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence, which must implement <see cref="IBatchItem"/>.</typeparam>
    /// <param name="source">The asynchronous enumerable sequence to collect items from.</param>
    /// <param name="count">The number of items to collect from the source sequence.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of collected items.</returns>
    public static async ValueTask<IReadOnlyList<T>> CollectAsync<T>(
        this IAsyncEnumerable<T> source,
        int count)
        where T : IBatchItem => await source.TakeAsync(count).ToListAsync();

    /// <inheritdoc cref="CollectAsync{T}(System.Collections.Generic.IAsyncEnumerable{T})" />
    public static ValueTaskAwaiter<IReadOnlyList<T>> GetAwaiter<T>(this IAsyncEnumerable<T> source)
        where T : IBatchItem => source.CollectAsync().GetAwaiter();
}