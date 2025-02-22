// <copyright file="Util.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Security.Cryptography;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Utility class for various MST operations.
/// </summary>
public static partial class Util
{
    /// <summary>
    /// Formats a data key by combining collection and record key.
    /// </summary>
    /// <param name="collection">The collection name.</param>
    /// <param name="rkey">The record key.</param>
    /// <returns>The formatted data key.</returns>
    public static string FormatDataKey(string collection, string rkey)
    {
        return $"{collection}/{rkey}";
    }

    /// <summary>
    /// Parses a data key into collection and record key.
    /// </summary>
    /// <param name="key">The data key.</param>
    /// <returns>A tuple containing the collection and record key.</returns>
    /// <exception cref="Exception">Thrown when the key is invalid.</exception>
    public static (string collection, string rkey) ParseDataKey(string key)
    {
        var split = key.Split('/');
        if (split.Length != 2)
        {
            throw new Exception($"Invalid record key: {key}");
        }

        return (split[0], split[1]);
    }

    /// <summary>
    /// Generates a CID for the given entries.
    /// </summary>
    /// <param name="entry">The node entries.</param>
    /// <returns>The generated CID.</returns>
    public static ATCid CidForEntries(INodeEntry[] entry)
    {
        var data = SerializeNodeData(entry);
        return CBORBlock.Encode(data).Cid;
    }

    /// <summary>
    /// Calculates the number of leading zeros in the SHA256 hash of the given key.
    /// </summary>
    /// <param name="key">The key to hash.</param>
    /// <returns>The number of leading zeros.</returns>
    public static int LeadingZerosOnHash(ReadOnlySpan<byte> key)
    {
#if NET
        var hash = SHA256.HashData(key);
#else
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(key.ToArray());
#endif
        var leadingZeros = 0;
        foreach (var item in hash)
        {
            if (item < 64)
            {
                leadingZeros++;
            }

            if (item < 16)
            {
                leadingZeros++;
            }

            if (item < 4)
            {
                leadingZeros++;
            }

            if (item == 0)
            {
                leadingZeros++;
            }
            else
            {
                break;
            }
        }

        return leadingZeros;
    }

    /// <summary>
    /// Determines the layer for the given entries.
    /// </summary>
    /// <param name="entries">The node entries.</param>
    /// <returns>The layer, or null if no leaf is found.</returns>
    public static int? LayerForEntries(INodeEntry[] entries)
    {
        var firstLeaf = entries.FirstOrDefault(x => x is Leaf);
        if (firstLeaf is not Leaf leaf)
        {
            return null;
        }

        return LeadingZerosOnHash(Encoding.ASCII.GetBytes(leaf.Key));
    }

    /// <summary>
    /// Deserializes node data into node entries.
    /// </summary>
    /// <param name="storage">The repository storage.</param>
    /// <param name="data">The node data.</param>
    /// <param name="opts">The MST options.</param>
    /// <returns>The deserialized node entries.</returns>
    public static INodeEntry[] DeserializeNodeData(IRepoStorage storage, NodeData data, MstOpts? opts)
    {
        var entries = new List<INodeEntry>();
        if (data.Left != null)
        {
            entries.Add(MST.Load(storage, data.Left, opts?.Layer == null ? null : new MstOpts(opts.Layer - 1)));
        }

        var lastKey = string.Empty;
        foreach (var entry in data.Entries)
        {
            var keyStr = entry.KeyString;
            var key = lastKey[..entry.PrefixCount] + keyStr;
            EnsureValidMstKey(key);
            entries.Add(new Leaf(key, entry.Value));
            lastKey = key;
            if (entry.Tree != null)
            {
                entries.Add(MST.Load(storage, entry.Tree, opts?.Layer == null ? null : new MstOpts(opts.Layer - 1)));
            }
        }

        return entries.ToArray();
    }

    /// <summary>
    /// Serializes node entries into node data.
    /// </summary>
    /// <param name="entries">The node entries.</param>
    /// <returns>The serialized node data.</returns>
    /// <exception cref="Exception">Thrown when the node is invalid.</exception>
    public static NodeData SerializeNodeData(INodeEntry[] entries)
    {
        var data = new NodeData
        {
            Left = null,
            Entries = [],
        };

        var i = 0;
        if (entries.Length > 0 && entries[0] is MST mst)
        {
            i++;
            data.Left = mst.Pointer;
        }

        var lastKey = string.Empty;
        while (i < entries.Length)
        {
            var entry = entries[i];
            var next = entries.Length > i + 1 ? entries[i + 1] : null;
            if (entry is not Leaf leaf)
            {
                throw new Exception("Not a valid node: two subtrees next to each other");
            }

            i++;
            Cid? subtree = null;
            if (next is MST mst2)
            {
                subtree = mst2.Pointer;
                i++;
            }

            EnsureValidMstKey(leaf.Key);
            var prefixLen = CountPrefixLen(lastKey, leaf.Key);
            data.Entries.Add(new TreeEntry
            {
                PrefixCount = prefixLen,
                Key = Encoding.ASCII.GetBytes(leaf.Key[prefixLen..]),
                Value = leaf.Value,
                Tree = subtree,
            });

            lastKey = leaf.Key;
        }

        return data;
    }

    /// <summary>
    /// Counts the length of the common prefix between two strings.
    /// </summary>
    /// <param name="a">The first string.</param>
    /// <param name="b">The second string.</param>
    /// <returns>The length of the common prefix.</returns>
    public static int CountPrefixLen(string a, string b)
    {
        var i = 0;
        while (i < a.Length && i < b.Length && a[i] == b[i])
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// Ensures that the given string is a valid MST key.
    /// </summary>
    /// <param name="str">The string to validate.</param>
    /// <returns>True if the key is valid.</returns>
    /// <exception cref="Exception">Thrown when the key is invalid.</exception>
    public static bool EnsureValidMstKey(string str)
    {
        if (!IsValidMstKey(str))
        {
            throw new Exception($"Not a valid MST key: {str}");
        }

        return true;
    }

    /// <summary>
    /// Checks if the given string is a valid MST key.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>True if the key is valid, otherwise false.</returns>
    public static bool IsValidMstKey(string str)
    {
        var split = str.Split('/');
        if (str.Length > 256)
        {
            return false;
        }

        if (split.Length != 2)
        {
            return false;
        }

        if (split[0].Length == 0)
        {
            return false;
        }

        if (split[1].Length == 0)
        {
            return false;
        }

        if (!IsMatch(split[0]))
        {
            return false;
        }

        if (!IsMatch(split[1]))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the given string matches the valid characters regex.
    /// </summary>
    /// <param name="str">String.</param>
    /// <returns>Match.</returns>
    public static bool IsMatch(string str)
    {
#if NET
        return ValidCharsRegex().IsMatch(str);
#else
        return ValidCharsRegex.IsMatch(str);
#endif
    }

    /// <summary>
    /// Returns a regex for validating characters.
    /// </summary>
    /// <returns>The regex for validating characters.</returns>
#if NET
    [GeneratedRegex("^[a-zA-Z0-9_\\-:.]*$")]
    private static partial Regex ValidCharsRegex();
#else
    private static readonly Regex ValidCharsRegex = new Regex("^[a-zA-Z0-9_\\-:.]*$", RegexOptions.Compiled);
#endif
}
