// <copyright file="FrameFooter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class FrameFooter
{
    public FrameFooter(CBORObject obj)
    {
        this.Did = AtDid.Create(obj["did"].AsString());
        this.Version = obj["version"].AsInt32();
        this.Prev = obj["prev"].ToCid();
        this.Data = obj["data"].ToCid();
        this.Sig = obj["sig"].GetByteString();
    }

    /// <summary>
    /// Gets the Did.
    /// </summary>
    public AtDid Did { get; }

    public int Version { get; }

    public Cid? Prev { get; }

    public Cid? Data { get; }

    public byte[] Sig { get; }
}
