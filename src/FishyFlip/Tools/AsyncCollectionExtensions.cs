// <copyright file="AsyncCollectionExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

namespace FishyFlip.Tools;

/// <summary>
/// Async Collection Extensions.
/// </summary>
internal static class AsyncCollectionExtensions
{
    /// <summary>
    /// Returns a specified number of contiguous elements from the start of an asynchronous sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The source sequence to return elements from.</param>
    /// <param name="count">The number of elements to return.</param>
    /// <returns>An <see cref="IAsyncEnumerable{T}"/> that contains the specified number of elements from the start of the input sequence.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="source"/> is null.</exception>
    public static async IAsyncEnumerable<T> TakeAsync<T>(this IAsyncEnumerable<T> source, int count)
    {
        var currentCount = 0;

        await foreach (var i in source)
        {
            if (currentCount >= count)
            {
                yield break;
            }

            yield return i;
            currentCount++;
        }
    }

    /// <summary>
    /// Projects each element of an async sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one async sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the source sequence.</typeparam>
    /// <typeparam name="T">The type of the elements of the resulting sequence.</typeparam>
    /// <param name="source">An async sequence of values to project.</param>
    /// <param name="transform">A transform function to apply to each element of the input sequence.</param>
    /// <returns>An async sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
    public static async IAsyncEnumerable<T> SelectManyAsync<TSource, T>(
        this IAsyncEnumerable<TSource> source,
        Func<TSource, IEnumerable<T>> transform)
    {
        await foreach (var i in source)
        {
            foreach (var j in transform(i))
            {
                yield return j;
            }
        }
    }

    /// <summary>
    /// Filters the elements of an <see cref="IAsyncEnumerable{T}"/> based on a specified type.
    /// </summary>
    /// <typeparam name="T">The type to filter the elements of the sequence on.</typeparam>
    /// <param name="source">The source sequence to filter.</param>
    /// <returns>An <see cref="IAsyncEnumerable{T}"/> that contains elements from the input sequence of type <typeparamref name="T"/>.</returns>
    public static async IAsyncEnumerable<T> OfTypeAsync<T>(this IAsyncEnumerable<object> source)
    {
        await foreach (var i in source)
        {
            if (i is T match)
            {
                yield return match;
            }
        }
    }

    /// <summary>
    /// Converts an <see cref="IAsyncEnumerable{T}"/> to a <see cref="List{T}"/> asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The source sequence to convert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list with the elements from the source sequence.</returns>
    public static async ValueTask<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        var list = new List<T>();

        await foreach (var i in source)
        {
            list.Add(i);
        }

        return list;
    }

    /// <summary>
    /// Converts an <see cref="IAsyncEnumerable{T}"/> to a <see cref="List{T}"/> and returns a <see cref="ValueTaskAwaiter{TResult}"/> for awaiting the result.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The source sequence to convert to a list.</param>
    /// <returns>A <see cref="ValueTaskAwaiter{TResult}"/> for awaiting the result of converting the source sequence to a list.</returns>
    public static ValueTaskAwaiter<List<T>> GetAwaiter<T>(this IAsyncEnumerable<T> source) =>
        source.ToListAsync().GetAwaiter();
}