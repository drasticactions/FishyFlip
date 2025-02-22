// <copyright file="BlockMap.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental;

/// <summary>
/// Represents a map of blocks.
/// </summary>
public class BlockMap
{
    private readonly Dictionary<string, byte[]> map = new();

    /// <summary>
    /// Gets an iterator over the map.
    /// </summary>
    public IEnumerable<(ATCid Key, byte[] Value)> Iterator => this.map.Select(kv => (ATCid.Decode(kv.Key), kv.Value));

    /// <summary>
    /// Gets the entries in the map as FrameEvent objects.
    /// </summary>
    public FrameEvent[] Entries => this.map.Select(kv => new FrameEvent(ATCid.Decode(kv.Key), kv.Value)).ToArray();

    /// <summary>
    /// Gets the CIDs in the map.
    /// </summary>
    public ATCid[] Cids => this.map.Keys.Select(ATCid.Decode).ToArray();

    /// <summary>
    /// Gets the number of entries in the map.
    /// </summary>
    public int Size => this.map.Count;

    /// <summary>
    /// Gets the total byte size of all entries in the map.
    /// </summary>
    public ulong ByteSize => (ulong)this.map.Values.Sum(b => (long)b.Length);

    /// <summary>
    /// Checks if the map contains the specified CID.
    /// </summary>
    /// <param name="cid">The CID to check.</param>
    /// <returns>True if the map contains the CID, otherwise false.</returns>
    public bool Has(ATCid cid)
    {
        return this.map.ContainsKey(cid.ToString());
    }

    /// <summary>
    /// Gets the block associated with the specified CID.
    /// </summary>
    /// <param name="cid">The CID of the block to get.</param>
    /// <returns>The block associated with the CID, or null if not found.</returns>
    public byte[]? Get(ATCid cid)
    {
        this.map.TryGetValue(cid.ToString(), out var value);
        return value;
    }

    /// <summary>
    /// Deletes the block associated with the specified CID.
    /// </summary>
    /// <param name="cid">The CID of the block to delete.</param>
    public void Delete(ATCid cid)
    {
        this.map.Remove(cid.ToString());
    }

    /// <summary>
    /// Clears all entries in the map.
    /// </summary>
    public void Clear()
    {
        this.map.Clear();
    }

    /// <summary>
    /// Adds a new block to the map.
    /// </summary>
    /// <param name="data">The data to add as a block.</param>
    /// <returns>The CID of the added block.</returns>
    public ATCid Add(CBORObject data)
    {
        var block = CBORBlock.Encode(data);
        this.Set(block.Cid, block.Bytes);
        return block.Cid;
    }

    /// <summary>
    /// Adds all blocks from another BlockMap to this map.
    /// </summary>
    /// <param name="toAdd">The BlockMap to add blocks from.</param>
    public void AddMap(BlockMap toAdd)
    {
        foreach (var (cid, block) in toAdd.Iterator)
        {
            this.Set(cid, block);
        }
    }

    /// <summary>
    /// Sets a block in the map.
    /// </summary>
    /// <param name="cid">The CID of the block.</param>
    /// <param name="block">The block data.</param>
    public void Set(Cid cid, byte[] block)
    {
        this.map[cid.ToString()] = block;
    }

    /// <summary>
    /// Gets multiple blocks from the map.
    /// </summary>
    /// <param name="cids">The CIDs of the blocks to get.</param>
    /// <returns>A tuple containing a BlockMap of found blocks and an array of missing CIDs.</returns>
    public (BlockMap blocks, Cid[] missing) GetMany(Cid[] cids)
    {
        var missing = new List<Cid>();
        var blocks = new BlockMap();
        foreach (var cid in cids)
        {
            var got = this.Get(cid);
            if (got != null)
            {
                blocks.Set(cid, got);
            }
            else
            {
                missing.Add(cid);
            }
        }

        return (blocks, missing.ToArray());
    }
}

