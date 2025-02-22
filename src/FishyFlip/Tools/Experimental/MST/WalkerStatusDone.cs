// <copyright file="WalkerStatusDone.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// WalkerStatusDone class.
/// </summary>
public record WalkerStatusDone : IWalkerStatus
{
    /// <inheritdoc/>
    public bool Done => true;
}