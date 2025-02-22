// <copyright file="MSTWalker.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// MST Walker class.
/// </summary>
public class MSTWalker
{
    /// <summary>
    /// Root of the tree.
    /// </summary>
    public readonly MST Root;

    /// <summary>
    /// Status of the walker.
    /// </summary>
    public IWalkerStatus Status;

    private readonly Stack<IWalkerStatus> stack = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="MSTWalker"/> class.
    /// </summary>
    /// <param name="root">MST Root.</param>
    public MSTWalker(MST root)
    {
        this.Root = root;
        this.Status = new WalkerStatusProgress(root)
        {
            Walking = null,
            Index = 0,
        };
    }

    /// <summary>
    /// Get the layer of the current node.
    /// </summary>
    /// <returns>Int.</returns>
    public int Layer()
    {
        if (this.Status is WalkerStatusDone)
        {
            throw new InvalidOperationException("Walker is done");
        }

        var progress = (WalkerStatusProgress)this.Status;

        if (progress.Walking != null)
        {
            return progress.Walking.Layer ?? 0;
        }

        if (progress.Current is MST mst)
        {
            return (mst.Layer ?? 0) + 1;
        }

        throw new InvalidOperationException("Could not identify layer of walk");
    }

    /// <summary>
    /// Step over the current node.
    /// </summary>
    /// <returns>Task.</returns>
    public async Task StepOver()
    {
        while (true)
        {
            if (this.Status is WalkerStatusDone)
            {
                return;
            }

            var progress = (WalkerStatusProgress)this.Status;

            // if stepping over the root of the node, we're done
            if (progress.Walking == null)
            {
                this.Status = new WalkerStatusDone();
                return;
            }

            var entries = await progress.Walking.GetEntries();
            progress.Index++;
            var next = entries.Length > progress.Index ? entries[progress.Index] : null;
            if (next == null)
            {
#if NET
                if (!this.stack.TryPop(out var popped))
                {
                    this.Status = new WalkerStatusDone();
                }
                else
                {
                    this.Status = popped;
                    continue;
                }
#else
                if (this.stack.Count == 0)
                {
                    this.Status = new WalkerStatusDone();
                }
                else
                {
                    var popped = this.stack.Pop();
                    this.Status = popped;
                    continue;
                }
#endif
            }
            else
            {
                progress.Current = next;
            }

            break;
        }
    }

    /// <summary>
    /// Step into the current node.
    /// </summary>
    /// <returns>Task.</returns>
    public async Task StepInto()
    {
        if (this.Status is WalkerStatusDone)
        {
            return;
        }

        var progress = (WalkerStatusProgress)this.Status;
        if (progress.Walking == null)
        {
            if (progress.Current is not MST curr)
            {
                throw new InvalidOperationException("The root of the tree is not an MST");
            }

            var next = await curr.AtIndex(0);
            if (next == null)
            {
                this.Status = new WalkerStatusDone();
            }
            else
            {
                this.Status = new WalkerStatusProgress(next!)
                {
                    Walking = curr,
                    Index = 0,
                };
            }
        }
        else
        {
            if (progress.Current is not MST curr)
            {
                throw new InvalidOperationException("No tree at pointer, cannot step into");
            }

            var next = await curr.AtIndex(0);
            if (next == null)
            {
                throw new InvalidOperationException("Tried to step into a node with no children");
            }

            this.stack.Push(this.Status);
            progress.Walking = curr;
            progress.Current = next;
            progress.Index = 0;
        }
    }

    /// <summary>
    /// Advance the walker.
    /// </summary>
    /// <returns>Task.</returns>
    public async Task Advance()
    {
        if (this.Status is WalkerStatusDone)
        {
            return;
        }

        var progress = (WalkerStatusProgress)this.Status;
        if (progress.Current is not MST)
        {
            await this.StepOver();
        }
        else
        {
            await this.StepInto();
        }
    }
}