// <copyright file="LatestCommit.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the latest commit information.
/// </summary>
public record LatestCommit(ATCid? ATCid, string? Rev);