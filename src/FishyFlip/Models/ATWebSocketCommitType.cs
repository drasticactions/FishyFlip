// <copyright file="ATWebSocketCommitType.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// AT WebSocket Commit Type.
/// </summary>
public enum ATWebSocketCommitType
{
    /// <summary>
    /// Unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// Create.
    /// </summary>
    Create,

    /// <summary>
    /// Update.
    /// </summary>
    Update,

    /// <summary>
    /// Delete.
    /// </summary>
    Delete,
}