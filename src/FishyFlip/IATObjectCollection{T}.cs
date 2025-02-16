// <copyright file="IATObjectCollection{T}.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Lexicon;
using FishyFlip.Models;
using FishyFlip.Tools;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FishyFlip;

/// <summary>
/// Interface for enumerable ATObject collections.
/// </summary>
/// <typeparam name="T">Type of ATObject.</typeparam>
public interface IATObjectCollection<T> : IReadOnlyList<T>, IAsyncEnumerable<T>, INotifyCollectionChanged
    where T : ATObject
{
    /// <summary>
    /// Gets the cursor.
    /// </summary>
    string Cursor { get; }

    /// <summary>
    /// Gets the cancellation token.
    /// </summary>
    CancellationToken? CancellationToken { get; }

    /// <summary>
    /// Gets a value indicating whether there are more items.
    /// </summary>
    bool HasMoreItems { get; }

    /// <summary>
    /// Gets the limit of items to return.
    /// </summary>
    int Limit { get; }

    /// <summary>
    /// Refreshes the items.
    /// </summary>
    /// <param name="limit">Limit of items to fetch.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>Task.</returns>
    Task RefreshAsync(int? limit = null, CancellationToken? token = default);

    /// <summary>
    /// Gets more items.
    /// </summary>
    /// <param name="limit">Limit of items to fetch.</param>
    /// <param name="token">Cancellation Token.</param>
    /// <returns>Task.</returns>
    Task GetMoreItemsAsync(int? limit = null, CancellationToken? token = default);

    /// <summary>
    /// Clears the items.
    /// </summary>
    public void Clear();

    /// <summary>
    /// Handle ATError.
    /// </summary>
    /// <param name="error">ATError.</param>
    /// <exception cref="ATNetworkErrorException">Thrown if ATError is not null.</exception>
    public void HandleATError(ATError? error = null);
}