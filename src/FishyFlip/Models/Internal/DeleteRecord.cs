// <copyright file="DeleteRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents a record used for deleting data from a collection in a repository.
/// </summary>
/// <param name="Collection">The name of the collection.</param>
/// <param name="Repo">The name of the repository.</param>
/// <param name="Rkey">The key of the record to be deleted.</param>
/// <param name="SwapRecord">The record to be swapped with the deleted record.</param>
/// <param name="SwapCommit">The commit to be swapped with the deleted record's commit.</param>
public record DeleteRecord(string Collection, string Repo, string Rkey, Cid? SwapRecord = null, Cid? SwapCommit = null);