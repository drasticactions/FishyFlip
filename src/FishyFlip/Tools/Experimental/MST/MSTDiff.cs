// <copyright file="MSTDiff.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Linq;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// MSTDiff class.
/// </summary>
public static class MSTDiff
{
    /// <summary>
    /// Null Diffing.
    /// </summary>
    /// <param name="tree">Tree.</param>
    /// <returns>Data Diff.</returns>
    public static async Task<DataDiff> NullDiff(MST tree)
    {
        var diff = new DataDiff();
        await foreach (var entry in tree.Walk())
        {
            await diff.NodeAdd(entry);
        }

        return diff;
    }

    /// <summary>
    /// Diffs the specified current and previous MST.
    /// </summary>
    /// <param name="curr">Current MST.</param>
    /// <param name="prev">Previous MST.</param>
    /// <returns>Data Diff.</returns>
    public static async Task<DataDiff> Diff(MST curr, MST? prev)
    {
        await curr.GetPointer();
        if (prev == null)
        {
            return await NullDiff(curr);
        }

        await prev.GetPointer();
        var diff = new DataDiff();

        var leftWalker = new MSTWalker(prev);
        var rightWalker = new MSTWalker(curr);
        while (!leftWalker.Status.Done || !rightWalker.Status.Done)
        {
            // if one walker is finished, continue walking the other & logging all nodes
            if (leftWalker.Status.Done && rightWalker.Status is WalkerStatusProgress rightProgress)
            {
                await diff.NodeAdd(rightProgress.Current);
                await rightWalker.Advance();
                continue;
            }

            if (leftWalker.Status is WalkerStatusProgress leftProgress && rightWalker.Status.Done)
            {
                await diff.NodeDelete(leftProgress.Current);
                await leftWalker.Advance();
                continue;
            }

            if (leftWalker.Status.Done || rightWalker.Status.Done)
            {
                break;
            }

            var left = (leftWalker.Status as WalkerStatusProgress)?.Current;
            var right = (rightWalker.Status as WalkerStatusProgress)?.Current;
            if (left == null || right == null)
            {
                break;
            }

            // if both pointers are leaves, record an update & advance both or record the lowest key and advance that pointer
            if (left is Leaf leftLeaf && right is Leaf rightLeaf)
            {
                if (leftLeaf.Key == rightLeaf.Key)
                {
                    if (!leftLeaf.Value.Equals(rightLeaf.Value))
                    {
                        diff.LeafUpdate(leftLeaf.Key, leftLeaf.Value, rightLeaf.Value);
                    }

                    await leftWalker.Advance();
                    await rightWalker.Advance();
                }
                else if (string.CompareOrdinal(leftLeaf.Key, rightLeaf.Key) < 0)
                {
                    await diff.NodeDelete(leftLeaf);
                    await leftWalker.Advance();
                }
                else
                {
                    await diff.NodeAdd(rightLeaf);
                    await rightWalker.Advance();
                }

                continue;
            }

            // next, ensure that we're on the same layer
            // if one walker is at a higher layer than the other, we need to do one of two things
            // if the higher walker is pointed at a tree, step into that tree to try to catch up with the lower
            // if the higher walker is pointed at a leaf, then advance the lower walker to try to catch up the higher
            if (leftWalker.Layer() > rightWalker.Layer())
            {
                if (left is Leaf)
                {
                    await diff.NodeAdd(right);
                    await rightWalker.Advance();
                }
                else
                {
                    await diff.NodeDelete(left);
                    await leftWalker.StepInto();
                }

                continue;
            }

            if (leftWalker.Layer() < rightWalker.Layer())
            {
                if (right is Leaf)
                {
                    await diff.NodeDelete(left);
                    await leftWalker.Advance();
                }
                else
                {
                    await diff.NodeAdd(right);
                    await rightWalker.StepInto();
                }

                continue;
            }

            // if we're on the same level, and both pointers are trees, do a comparison
            // if they're the same, step over. if they're different, step in to find the subdiff
            if (left is MST leftMst && right is MST rightMst)
            {
                if (leftMst.Pointer == rightMst.Pointer)
                {
                    await leftWalker.StepOver();
                    await rightWalker.StepOver();
                }
                else
                {
                    await diff.NodeAdd(right);
                    await diff.NodeDelete(left);
                    await leftWalker.StepInto();
                    await rightWalker.StepInto();
                }

                continue;
            }

            if (left is Leaf && right is MST)
            {
                await diff.NodeAdd(right);
                await rightWalker.StepInto();
                continue;
            }

            if (left is MST && right is Leaf)
            {
                await diff.NodeDelete(left);
                await leftWalker.StepInto();
                continue;
            }

            throw new Exception("Unidentifiable case in diff walk");
        }

        return diff;
    }
}