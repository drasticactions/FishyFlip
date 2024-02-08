// <copyright file="RepoRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reference to a repository.
/// </summary>
public record RepoRef(ATDid? Did, Cid? Head);