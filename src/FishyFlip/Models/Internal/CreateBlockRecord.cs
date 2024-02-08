// <copyright file="CreateBlockRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record for creating a block in a collection and repository.
/// </summary>
/// <param name="Collection">The name of the collection.</param>
/// <param name="Repo">The name of the repository.</param>
/// <param name="Record">The block record.</param>
public record CreateBlockRecord(string Collection, string Repo, BlockRecord Record);