// <copyright file="MemoryBlockStore.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental;

/// <summary>
/// In-memory implementation of the IRepoStorage interface.
/// </summary>
public class MemoryBlockStore : IRepoStorage
{
    private readonly BlockMap blocks;
    private string? rev;
    private Cid? root;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryBlockStore"/> class.
    /// </summary>
    /// <param name="blocks">Blocks.</param>
    public MemoryBlockStore(BlockMap? blocks)
    {
        this.blocks = new BlockMap();
        if (blocks != null)
        {
            this.blocks.AddMap(blocks);
        }
    }

    /// <inheritdoc/>
    public Task ApplyCommit(CommitData commit)
    {
        this.root = commit.Cid;
        this.rev = commit.Rev;
        foreach (var (cid, block) in commit.NewBlocks.Iterator)
        {
            this.blocks.Set(cid, block);
        }

        foreach (var cid in commit.RemovedCids.ToArray())
        {
            this.blocks.Delete(cid);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task<(CBORObject obj, byte[] bytes)?> AttemptRead(ATCid cid)
    {
        try
        {
            var bytes = await this.GetBytes(cid);
            var obj = CBORObject.DecodeFromBytes(bytes);
            return (obj, bytes!);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public Task<(BlockMap blocks, ATCid[] missing)> GetBlocks(ATCid[] cids)
    {
        var missing = cids.Where(c => !this.blocks.Has(c)).ToArray();
        var blocks = new BlockMap();
        foreach (var cid in cids)
        {
            if (this.blocks.Has(cid))
            {
                blocks.Set(cid, this.blocks.Get(cid)!);
            }
        }

        return Task.FromResult((blocks, missing));
    }

    /// <inheritdoc/>
    public Task<byte[]?> GetBytes(ATCid cid)
    {
        if (!this.blocks.Has(cid))
        {
            throw new Exception("Block not found");
        }

        return Task.FromResult(this.blocks.Get(cid));
    }

    /// <inheritdoc/>
    public Task<ATCid?> GetRoot()
    {
         return Task.FromResult(this.root);
    }

    /// <inheritdoc/>
    public Task<bool> Has(ATCid cid)
    {
        return Task.FromResult(this.blocks.Has(cid));
    }

    /// <inheritdoc/>
    public Task PutBlock(ATCid cid, byte[] block, string rev)
    {
        this.blocks.Set(cid, block);
        this.rev = rev;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task PutMany(BlockMap blocks, string rev)
    {
        this.blocks.AddMap(blocks);
        this.rev = rev;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<(CBORObject obj, byte[] bytes)> ReadObjAndBytes(ATCid cid)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task UpdateRoot(ATCid cid, string rev)
    {
        this.root = cid;
        this.rev = rev;
        return Task.CompletedTask;
    }
}