// <copyright file="ATWebSocketEvent.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools.Json;

namespace FishyFlip.Models;

/// <summary>
/// AT WebSocket Event.
/// </summary>
[JsonConverter(typeof(ATWebSocketEventConverter))]
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