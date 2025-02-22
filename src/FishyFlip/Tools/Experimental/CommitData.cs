// <copyright file="CommitData.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools.Experimental.MST;
using Ipfs;

namespace FishyFlip.Tools.Experimental;

/// <summary>
/// CommitData class.
/// </summary>
public record CommitData(Cid Cid, string Rev, string? Since, Cid? Prev, BlockMap NewBlocks, CidSet RemovedCids)
{
    /// <summary>
    /// Gets the version of the commit data.
    /// </summary>
    public int Version => 3;
}