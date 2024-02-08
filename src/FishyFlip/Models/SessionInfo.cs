// <copyright file="SessionInfo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents session information.
/// </summary>
public record SessionInfo(string Handle, ATDid Did, string? Email);