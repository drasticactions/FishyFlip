// <copyright file="WalkerStatusProgress.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// WalkerStatusProgress class.
/// </summary>
public class WalkerStatusProgress : IWalkerStatus
{
    /// <summary>
    /// Current node.
    /// </summary>
    public INodeEntry Current;

    /// <summary>
    /// Initializes a new instance of the <see cref="WalkerStatusProgress"/> class.
    /// </summary>
    /// <param name="current">The current node.</param>
    public WalkerStatusProgress(INodeEntry current)
    {
        this.Current = current;
    }

    /// <summary>
    /// Index of the current node.
    /// </summary>
    public int Index;

    /// <summary>
    /// Walking MST.
    /// </summary>
    public MST? Walking; // set to null if `Current` is the root of the tree

    /// <inheritdoc/>
    public bool Done => false;
}