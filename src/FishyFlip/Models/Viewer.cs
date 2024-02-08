// <copyright file="Viewer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a viewer in the FishyFlip application.
/// </summary>
public record Viewer(
    bool Muted,
    bool BlockedBy,
    ATUri Following);