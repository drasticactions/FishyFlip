// <copyright file="FrameHeaderOperation.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// The operation of the frame header.
/// </summary>
public enum FrameHeaderOperation
{
    /// <summary>
    /// Unknown frame header.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Regular frame.
    /// </summary>
    Frame = 1,

    /// <summary>
    /// ATError frame.
    /// </summary>
    Error = -1,
}