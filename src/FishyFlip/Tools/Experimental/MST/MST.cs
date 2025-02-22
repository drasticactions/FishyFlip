// <copyright file="MST.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Linq;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Represents a Merkle Patricia Trie.
/// </summary>
public record MST : INodeEntry
{
    /// <summary>
    /// The storage associated with the trie.
    /// </summary>
    public readonly IRepoStorage Storage;

    /// <summary>
    /// The entries in the trie.
    /// </summary>
    private INodeEntry[]? entries;

    /// <summary>
    /// Initializes a new instance of the <see cref="MST"/> class.
    /// </summary>
    /// <param name="storage">The storage associated with the trie.</param>
    /// <param name="pointer">The CID pointer of the trie.</param>
    /// <param name="entries">The entries in the trie.</param>
    /// <param name="layer">The layer of the trie.</param>
    public MST(IRepoStorage storage, Cid pointer, INodeEntry[]? entries = null, int? layer = null)
    {
        this.Storage = storage;
        this.Pointer = pointer;
        this.entries = entries;
        this.Layer = layer;
    }

    /// <summary>
    /// Gets the CID pointer of the trie.
    /// </summary>
    public Cid Pointer { get; private set; }

    /// <summary>
    /// Gets the layer of the trie.
    /// </summary>
    public int? Layer { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the pointer is outdated.
    /// </summary>
    public bool OutdatedPointer { get; private set; }

    /// <summary>
    /// Loads a Merkle Patricia Trie from the specified storage and pointer.
    /// </summary>
    /// <param name="storage">The storage associated with the trie.</param>
    /// <param name="pointer">The CID pointer of the trie.</param>
    /// <param name="opts">The options.</param>
    /// <returns><see cref="MST"/>.</returns>
    public static MST Load(IRepoStorage storage, ATCid pointer, MstOpts? opts = null)
    {
        return new MST(storage, pointer, null, opts?.Layer);
    }

    /// <summary>
    /// Creates a new Merkle Patricia Trie.
    /// </summary>
    /// <param name="storage">The storage associated with the trie.</param>
    /// <param name="entries">The entries in the trie.</param>
    /// <param name="opts">The options.</param>
    /// <returns><see cref="MST"/>.</returns>
    public static MST Create(IRepoStorage storage, INodeEntry[] entries, MstOpts? opts = null)
    {
        var pointer = Util.CidForEntries(entries);
        return new MST(storage, pointer, entries, opts?.Layer);
    }

    /// <summary>
    /// Creates a new Merkle Patricia Trie from node data.
    /// </summary>
    /// <param name="storage">The storage associated with the trie.</param>
    /// <param name="data">The node data.</param>
    /// <param name="opts">The options.</param>
    /// <returns><see cref="MST"/>.</returns>
    public static MST FromData(IRepoStorage storage, NodeData data, MstOpts? opts = null)
    {
        var entries = Util.DeserializeNodeData(storage, data, opts);
        var pointer = CBORBlock.Encode(data).Cid;
        return new MST(storage, pointer, entries, opts?.Layer);
    }

    /// <summary>
    /// Creates a new Merkle Patricia Trie with the specified entries.
    /// </summary>
    /// <param name="entries">The entries in the trie.</param>
    /// <returns><see cref="MST"/>.</returns>
    public MST NewTree(INodeEntry[] entries)
    {
        return new MST(this.Storage, Util.CidForEntries(entries), entries, this.Layer)
        {
            OutdatedPointer = true,
        };
    }

    /// <summary>
    /// Gets the entries in the trie.
    /// </summary>
    /// <returns>An array of <see cref="INodeEntry"/>.</returns>
    public async Task<INodeEntry[]> GetEntries()
    {
        if (this.entries != null)
        {
            return this.entries;
        }

        var data = await this.Storage.ReadObjAndBytes(this.Pointer);
        var nodeData = NodeData.FromCBORObject(data.obj);
        TreeEntry? firstLeaf = nodeData.Entries.Count > 0 ? nodeData.Entries[0] : null;
        int? layer = firstLeaf != null ? Util.LeadingZerosOnHash(firstLeaf.Value.Key) : null;
        this.entries = Util.DeserializeNodeData(this.Storage, nodeData, layer == null ? null : new MstOpts(layer.Value));

        return this.entries;
    }

    /// <summary>
    /// Gets the CID pointer of the trie.
    /// </summary>
    /// <returns>The CID pointer.</returns>
    public async Task<Cid> GetPointer()
    {
        if (!this.OutdatedPointer)
        {
            return this.Pointer;
        }

        var (cid, bytes) = await this.Serialize();
        this.Pointer = cid;
        this.OutdatedPointer = false;
        return this.Pointer;
    }

    /// <summary>
    /// Gets the layer of the trie.
    /// </summary>
    /// <returns>The layer of the trie.</returns>
    public async Task<int> GetLayer()
    {
        this.Layer = await this.AttemptGetLayer() ?? 0;
        return this.Layer.Value;
    }

    /// <summary>
    /// Attempts to get the layer of the trie.
    /// </summary>
    /// <returns>The layer of the trie, or null if not found.</returns>
    public async Task<int?> AttemptGetLayer()
    {
        if (this.Layer != null)
        {
            return this.Layer.Value;
        }

        var entries = await this.GetEntries();
        var layer = Util.LayerForEntries(entries);
        if (layer == null)
        {
            foreach (var entry in entries)
            {
                if (entry is MST mst)
                {
                    var childLayer = await mst.AttemptGetLayer();
                    if (childLayer != null)
                    {
                        layer = childLayer + 1;
                        break;
                    }
                }
            }
        }

        if (layer != null)
        {
            this.Layer = layer;
        }

        return layer;
    }

    /// <summary>
    /// Serializes the trie.
    /// </summary>
    /// <returns>A tuple containing the CID and the serialized bytes.</returns>
    public async Task<(Cid cid, byte[] bytes)> Serialize()
    {
        var entries = await this.GetEntries();
        var outdated = entries.OfType<MST>().Where(x => x.OutdatedPointer).ToArray();
        if (outdated.Length > 0)
        {
            foreach (var mst in outdated)
            {
                await mst.GetPointer();
            }

            entries = await this.GetEntries();
        }

        var data = Util.SerializeNodeData(entries);
        var block = CBORBlock.Encode(data);
        return (block.Cid, block.Bytes);
    }

    /// <summary>
    /// Gets the unstored blocks of the trie.
    /// </summary>
    /// <returns>A tuple containing the root CID and the block map.</returns>
    public async Task<(Cid root, BlockMap blocks)> GetUnstoredBlocks()
    {
        var blocks = new BlockMap();
        var pointer = await this.GetPointer();
        var alreadyHas = await this.Storage.Has(pointer);
        if (alreadyHas)
        {
            return (pointer, blocks);
        }

        var entries = await this.GetEntries();
        var data = Util.SerializeNodeData(entries);
        blocks.Add(data.ToCBORObject());
        foreach (var entry in entries)
        {
            if (entry is MST mst)
            {
                var (root, childBlocks) = await mst.GetUnstoredBlocks();
                blocks.AddMap(childBlocks);
            }
        }

        return (pointer, blocks);
    }

    /// <summary>
    /// Walks through the trie.
    /// </summary>
    /// <returns>An async enumerable of <see cref="INodeEntry"/>.</returns>
    public async IAsyncEnumerable<INodeEntry> Walk()
    {
        yield return this;
        var entries = await this.GetEntries();
        foreach (var entry in entries)
        {
            if (entry is MST mst)
            {
                await foreach (var subEntry in mst.Walk())
                {
                    yield return subEntry;
                }
            }
            else
            {
                yield return entry;
            }
        }
    }

    /// <summary>
    /// Creates a child trie.
    /// </summary>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> CreateChild()
    {
        var layer = await this.GetLayer();
        return Create(this.Storage, [], new MstOpts(layer - 1));
    }

    /// <summary>
    /// Creates a parent trie.
    /// </summary>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> CreateParent()
    {
        var layer = await this.GetLayer();
        var parent = Create(this.Storage, [this], new MstOpts(layer + 1));
        parent.OutdatedPointer = true;
        return parent;
    }

    /// <summary>
    /// Updates an entry in the trie.
    /// </summary>
    /// <param name="index">The index of the entry to update.</param>
    /// <param name="entry">The new entry.</param>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> UpdateEntry(int index, INodeEntry entry)
    {
        var before = await this.Slice(0, index);
        var after = await this.Slice(index + 1, null);
        return this.NewTree([.. before, entry, .. after]);
    }

    /// <summary>
    /// Replaces an entry with a split in the trie.
    /// </summary>
    /// <param name="index">The index of the entry to replace.</param>
    /// <param name="left">The left subtree.</param>
    /// <param name="leaf">The leaf node.</param>
    /// <param name="right">The right subtree.</param>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> ReplaceWithSplit(int index, MST? left, Leaf leaf, MST? right)
    {
        var update = (await this.Slice(0, index)).ToList();
        if (left != null)
        {
            update.Add(left);
        }

        update.Add(leaf);
        if (right != null)
        {
            update.Add(right);
        }

        update.AddRange(await this.Slice(index + 1, null));
        return this.NewTree(update.ToArray());
    }

    /// <summary>
    /// Removes an entry from the trie.
    /// </summary>
    /// <param name="index">The index of the entry to remove.</param>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> RemoveEntry(int index)
    {
        var before = await this.Slice(0, index);
        var after = await this.Slice(index + 1, null);
        return this.NewTree([.. before, .. after]);
    }

    /// <summary>
    /// Prepends an entry to the trie.
    /// </summary>
    /// <param name="entry">The entry to prepend.</param>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> Prepend(INodeEntry entry)
    {
        var entries = await this.GetEntries();
        return this.NewTree([entry, .. entries]);
    }

    /// <summary>
    /// Appends an entry to the trie.
    /// </summary>
    /// <param name="entry">The entry to append.</param>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> Append(INodeEntry entry)
    {
        var entries = await this.GetEntries();
        return this.NewTree([.. entries, entry]);
    }

    /// <summary>
    /// Slices the entries in the trie.
    /// </summary>
    /// <param name="start">The start index.</param>
    /// <param name="end">The end index.</param>
    /// <returns>An array of <see cref="INodeEntry"/>.</returns>
    public async Task<INodeEntry[]> Slice(int? start, int? end)
    {
#if NET
        var entries = await this.GetEntries();
        start ??= 0;
        end ??= entries.Length;
        return entries[start.Value..end.Value];
#else
        var entries = await this.GetEntries();
        start ??= 0;
        end ??= entries.Length;
        int length = end.Value - start.Value;
        INodeEntry[] result = new INodeEntry[length];
        Array.Copy(entries, start.Value, result, 0, length);
        return result;
#endif
    }

    /// <summary>
    /// Splits the trie around a key.
    /// </summary>
    /// <param name="key">The key to split around.</param>
    /// <returns>A tuple containing the left and right subtrees.</returns>
    public async Task<(MST? left, MST? right)> SplitAround(string key)
    {
        var index = await this.FindGtOrEqualLeafIndex(key);
        var leftData = await this.Slice(0, index);
        var rightData = await this.Slice(index, null);
        var left = this.NewTree(leftData);
        var right = this.NewTree(rightData);

        var lastInLeft = leftData.Length > 0 ? leftData[^1] : null;
        if (lastInLeft is MST mst)
        {
            left = await left.RemoveEntry(leftData.Length - 1);
            var split = await mst.SplitAround(key);
            if (split.left != null)
            {
                left = await left.Append(split.left);
            }

            if (split.right != null)
            {
                right = await right.Prepend(split.right);
            }
        }

        var outLeft = (await left.GetEntries()).Length > 0 ? left : null;
        var outRight = (await right.GetEntries()).Length > 0 ? right : null;
        return (outLeft, outRight);
    }

    /// <summary>
    /// Splices an entry into the trie.
    /// </summary>
    /// <param name="entry">The entry to splice in.</param>
    /// <param name="index">The index to splice at.</param>
    /// <returns>A new <see cref="MST"/>.</returns>
    public async Task<MST> SpliceIn(INodeEntry entry, int index)
    {
        var before = await this.Slice(0, index);
        var after = await this.Slice(index, null);
        return this.NewTree([.. before, entry, .. after]);
    }

    /// <summary>
    /// Gets the entry at the specified index.
    /// </summary>
    /// <param name="index">The index of the entry.</param>
    /// <returns>The entry at the specified index, or null if not found.</returns>
    public async Task<INodeEntry?> AtIndex(int index)
    {
        var entries = await this.GetEntries();
        return index < entries.Length && index >= 0 ? entries[index] : null;
    }

    /// <summary>
    /// Finds the index of the first leaf node with a key greater than or equal to the specified key.
    /// </summary>
    /// <param name="key">The key to compare.</param>
    /// <returns>The index of the first leaf node with a key greater than or equal to the specified key.</returns>
    public async Task<int> FindGtOrEqualLeafIndex(string key)
    {
#if NET
        var entries = await this.GetEntries();
        var maybeIndex = entries.Select((entry, i) => (entry, i))
            .Where(x => x.entry is Leaf leaf && string.CompareOrdinal(leaf.Key, key) >= 0)
            .Select(x => x.i)
            .FirstOrDefault(-1);
        return maybeIndex >= 0 ? maybeIndex : entries.Length;
#else
        var entries = await this.GetEntries();
        var maybeIndex = entries.Select((entry, i) => (entry, i))
            .Where(x => x.entry is Leaf leaf && string.CompareOrdinal(leaf.Key, key) >= 0)
            .Select(x => x.i)
            .FirstOrDefault();
        return maybeIndex >= 0 ? maybeIndex : entries.Length;
#endif
    }
}
