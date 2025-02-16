// <copyright file="ATObjectCollectionBase{T}.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FishyFlip;
using FishyFlip.Lexicon;
using FishyFlip.Models;
using FishyFlip.Tools;

namespace FishyFlip;

/// <summary>
/// Base implementation of IATObjectCollection that provides observable collection functionality.
/// </summary>
/// <typeparam name="T">Type of ATObject.</typeparam>
public abstract class ATObjectCollectionBase<T> : IATObjectCollection<T>
    where T : ATObject
{
    private readonly ATProtocol atp;
    private readonly ObservableCollection<T> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATObjectCollectionBase{T}"/> class.
    /// </summary>
    /// <param name="atp">ATProtocol.</param>
    protected ATObjectCollectionBase(ATProtocol atp)
    {
        this.atp = atp;
        this.items = new ObservableCollection<T>();
    }

    /// <inheritdoc/>
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => this.items.CollectionChanged += value;
        remove => this.items.CollectionChanged -= value;
    }

    /// <inheritdoc/>
    public string? Cursor { get; protected set; } = string.Empty;

    /// <inheritdoc/>
    public int? Limit { get; protected set; }

    /// <inheritdoc/>
    public CancellationToken? CancellationToken { get; protected set; }

    /// <inheritdoc/>
    public bool HasMoreItems { get; protected set; }

    /// <inheritdoc/>
    public int Count => this.items.Count;

    /// <summary>
    /// Gets the ATProtocol.
    /// </summary>
    internal ATProtocol ATProtocol => this.atp;

    /// <inheritdoc/>
    public T this[int index] => this.items[index];

    /// <inheritdoc/>
    public virtual Task RefreshAsync(int? limit = null, CancellationToken? token = default)
    {
        this.Clear();
        return this.GetMoreItemsAsync(limit, token);
    }

    /// <summary>
    /// Gets records.
    /// </summary>
    /// <param name="limit">Limit.</param>
    /// <param name="token">Token.</param>
    /// <returns>Result of T.</returns>
    public abstract Task<(IList<T> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default);

    /// <inheritdoc/>
    public virtual async Task GetMoreItemsAsync(int? limit = null, CancellationToken? token = default)
    {
        var (postViews, cursor) = await this.GetRecordsAsync(limit, token);
        foreach (var postView in postViews)
        {
            this.AddItem(postView);
        }

        this.HasMoreItems = !string.IsNullOrEmpty(cursor);
        this.Cursor = cursor;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        this.items.Clear();
        this.Cursor = string.Empty;
        this.HasMoreItems = false;
    }

    /// <inheritdoc/>
    public void HandleATError(ATError? error = null)
    {
        if (error is not null)
        {
            throw new ATNetworkErrorException(error);
        }
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => this.items.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    /// <inheritdoc/>
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        foreach (var item in this.items)
        {
            yield return item;
            await Task.CompletedTask;
        }
    }

    /// <summary>
    /// Adds an item to the collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    protected void AddItem(T item)
    {
        this.items.Add(item);
    }

    /// <summary>
    /// Adds a range of items to the collection.
    /// </summary>
    /// <param name="items">The items to add.</param>
    protected void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            this.items.Add(item);
        }
    }

    /// <summary>
    /// Inserts an item at the specified index.
    /// </summary>
    /// <param name="index">The index to insert at.</param>
    /// <param name="item">The item to insert.</param>
    protected void InsertItem(int index, T item)
    {
        this.items.Insert(index, item);
    }

    /// <summary>
    /// Removes an item from the collection.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// <returns>True if the item was removed, false otherwise.</returns>
    protected bool RemoveItem(T item)
    {
        return this.items.Remove(item);
    }

    /// <summary>
    /// Removes an item at the specified index.
    /// </summary>
    /// <param name="index">The index to remove at.</param>
    protected void RemoveAt(int index)
    {
        this.items.RemoveAt(index);
    }
}