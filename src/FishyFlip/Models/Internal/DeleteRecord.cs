// <copyright file="DeleteRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record DeleteRecord(string Collection, string Repo, string Rkey, Cid? SwapRecord = null, Cid? SwapCommit = null);