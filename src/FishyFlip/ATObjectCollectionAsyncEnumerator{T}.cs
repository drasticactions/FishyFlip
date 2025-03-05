// <copyright file="ATObjectCollectionAsyncEnumerator{T}.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Abstract class for async enumerable ATObject collections.
/// </summary>
/// <typeparam name="T">Type of ATObject.</typeparam>
internal class ATObjectCollectionAsyncEnumerator<T> : IAsyncEnumerator<T>
    where T : ATObject
{
    private readonly CancellationToken cancellationToken;

    private int index = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATObjectCollectionAsyncEnumerator{T}"/> class.
    /// </summary>
    /// <param name="collection">The ATObject collection.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    public ATObjectCollectionAsyncEnumerator(IATObjectCollection<T> collection, CancellationToken cancellationToken)
    {
        this.Collection = collection;
        this.cancellationToken = cancellationToken;
    }

    /// <summary>
    /// Gets the current item.
    /// </summary>
    public T Current => this.Collection[this.index];

    /// <summary>
    /// Gets the ATObject collection.
    /// </summary>
    protected IATObjectCollection<T> Collection { get; }

    /// <summary>
    /// Disposes the enumerator.
    /// </summary>
    /// <returns>ValueTask.</returns>
    public ValueTask DisposeAsync()
    {
        return new ValueTask(Task.CompletedTask);
    }

    /// <summary>
    /// Moves to the next item.
    /// </summary>
    /// <returns>ValueTask.</returns>
    public async ValueTask<bool> MoveNextAsync()
    {
        if (this.index + 1 < this.Collection.Count)
        {
            this.index++;
            return true;
        }

        if (this.Collection.HasMoreItems)
        {
            await this.Collection.GetMoreItemsAsync(cancellationToken: this.cancellationToken);

            // Need to verify that new items were added with GetMoreItemsAsync to increase the index,
            // otherwise it will throw on an out of range index.
            if (this.index + 1 < this.Collection.Count)
            {
                this.index++;
                return true;
            }

            return this.Collection.HasMoreItems;
        }

        return false;
    }
}