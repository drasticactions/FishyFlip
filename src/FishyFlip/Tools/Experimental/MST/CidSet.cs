// <copyright file="CidSet.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Represents a set of CIDs.
/// </summary>
public class CidSet
{
    private readonly HashSet<string> set;

    /// <summary>
    /// Initializes a new instance of the <see cref="CidSet"/> class.
    /// </summary>
    /// <param name="arr">Array of CID values.</param>
    public CidSet(ATCid[]? arr = null)
    {
        arr ??= [];
        this.set = [..arr.Select(c => c.ToString())];
    }

    /// <summary>
    /// Adds a CID to the set.
    /// </summary>
    /// <param name="cid">ATCid.</param>
    /// <returns>CidSet.</returns>
    public CidSet Add(ATCid cid)
    {
        this.set.Add(cid.ToString());
        return this;
    }

    /// <summary>
    /// Adds a set of CIDs to the current set.
    /// </summary>
    /// <param name="toMerge">CidSet to merge.</param>
    /// <returns>CidSet.</returns>
    public CidSet AddSet(CidSet toMerge)
    {
        foreach (var cid in toMerge.ToArray())
        {
            this.Add(cid);
        }

        return this;
    }

    /// <summary>
    /// Subtracts a set of CIDs from the current set.
    /// </summary>
    /// <param name="toSubtract">CidSet to Subtract.</param>
    /// <returns>CidSet.</returns>
    public CidSet SubtractSet(CidSet toSubtract)
    {
        foreach (var cid in toSubtract.ToArray())
        {
            this.Delete(cid);
        }

        return this;
    }

    /// <summary>
    /// Deletes a CID from the set.
    /// </summary>
    /// <param name="cid">ATCid.</param>
    /// <returns>CidSet.</returns>
    public CidSet Delete(ATCid cid)
    {
        this.set.Remove(cid.ToString());
        return this;
    }

    /// <summary>
    /// Checks if the set contains a CID.
    /// </summary>
    /// <param name="cid">ATCid.</param>
    /// <returns>Bool.</returns>
    public bool Has(ATCid cid)
    {
        return this.set.Contains(cid.ToString());
    }

    /// <summary>
    /// Gets the size of the set.
    /// </summary>
    /// <returns>Int.</returns>
    public int Size()
    {
        return this.set.Count;
    }

    /// <summary>
    /// Clears the set.
    /// </summary>
    /// <returns>CidSet.</returns>
    public CidSet Clear()
    {
        this.set.Clear();
        return this;
    }

    /// <summary>
    /// Converts the set to an array of ATCids.
    /// </summary>
    /// <returns>Array of ATCid.</returns>
    public ATCid[] ToArray()
    {
        return this.set.Select(ATCid.Decode).ToArray();
    }
}