// <copyright file="IWalkerStatus.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Interface for WalkerStatus, used for walking the MST.
/// </summary>
public interface IWalkerStatus
{
    /// <summary>
    /// Gets a value indicating whether the walker is done.
    /// </summary>
    public bool Done { get; }
}