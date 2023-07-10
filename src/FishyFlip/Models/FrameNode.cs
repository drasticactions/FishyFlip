// <copyright file="FrameNode.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame Node.
/// </summary>
public class FrameNode
{
    public FrameNode(CBORObject obj)
    {
        this.Data = obj?.EncodeToBytes();
    }

    public byte[]? Data { get; }
}
