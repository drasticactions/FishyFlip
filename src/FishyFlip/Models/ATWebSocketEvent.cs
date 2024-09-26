// <copyright file="ATWebSocketEvent.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// AT WebSocket Event.
/// </summary>
public enum ATWebSocketEvent
{
    /// <summary>
    /// Unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// Commit.
    /// </summary>
    Commit,

    /// <summary>
    /// Account.
    /// </summary>
    Account,

    /// <summary>
    /// Identity.
    /// </summary>
    Identity,
}