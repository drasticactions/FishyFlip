// <copyright file="IRepoStorage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental;

/// <summary>
/// Interface for repository storage operations.
/// </summary>
public interface IRepoStorage
{
    /// <summary>
    /// Gets the root CID.
    /// </summary>
    /// <returns>The root CID.</returns>
    Task<ATCid?> GetRoot();

    /// <summary>
    /// Puts a block into the storage.
    /// </summary>
    /// <param name="cid">The CID of the block.</param>
    /// <param name="block">The block data.</param>
    /// <param name="rev">The revision string.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PutBlock(ATCid cid, byte[] block, string rev);

    /// <summary>
    /// Puts multiple blocks into the storage.
    /// </summary>
    /// <param name="blocks">The blocks to put.</param>
    /// <param name="rev">The revision string.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PutMany(BlockMap blocks, string rev);

    /// <summary>
    /// Updates the root CID.
    /// </summary>
    /// <param name="cid">The new root CID.</param>
    /// <param name="rev">The revision string.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateRoot(ATCid cid, string rev);

    /// <summary>
    /// Applies a commit to the storage.
    /// </summary>
    /// <param name="commit">The commit data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ApplyCommit(CommitData commit);

    /// <summary>
    /// Gets the bytes of a block by its CID.
    /// </summary>
    /// <param name="cid">The CID of the block.</param>
    /// <returns>The block bytes.</returns>
    Task<byte[]?> GetBytes(ATCid cid);

    /// <summary>
    /// Checks if a block exists in the storage.
    /// </summary>
    /// <param name="cid">The CID of the block.</param>
    /// <returns>True if the block exists, otherwise false.</returns>
    Task<bool> Has(ATCid cid);

    /// <summary>
    /// Gets multiple blocks by their CIDs.
    /// </summary>
    /// <param name="cids">The CIDs of the blocks.</param>
    /// <returns>A tuple containing the blocks and the missing CIDs.</returns>
    Task<(BlockMap blocks, ATCid[] missing)> GetBlocks(ATCid[] cids);

    /// <summary>
    /// Reads an object and its bytes by its CID.
    /// </summary>
    /// <param name="cid">The CID of the object.</param>
    /// <returns>A tuple containing the object and its bytes.</returns>
    Task<(CBORObject obj, byte[] bytes)> ReadObjAndBytes(ATCid cid);

    /// <summary>
    /// Attempts to read an object and its bytes by its CID.
    /// </summary>
    /// <param name="cid">The CID of the object.</param>
    /// <returns>A tuple containing the object and its bytes, or null if not found.</returns>
    Task<(CBORObject obj, byte[] bytes)?> AttemptRead(ATCid cid);
}