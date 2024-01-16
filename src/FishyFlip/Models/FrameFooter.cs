// <copyright file="FrameFooter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class FrameFooter
{
    public FrameFooter(CBORObject obj, ILogger? logger = default)
    {
        this.Did = ATDid.Create(obj["did"].AsString());
        this.Version = obj["version"].AsInt32();
        this.Prev = obj["prev"].ToCid(logger);
        this.Data = obj["data"].ToCid(logger);
        this.Sig = obj["sig"].GetByteString();
    }

    /// <summary>
    /// Gets the Did.
    /// </summary>
    public ATDid? Did { get; }

    public int Version { get; }

    public Cid? Prev { get; }

    public Cid? Data { get; }

    public byte[] Sig { get; }

    public static FrameFooter? FromCBORObject(CBORObject blockObj, ILogger? logger = default)
    {
        if (blockObj["sig"] is not null)
        {
            return new FrameFooter(blockObj, logger);
        }

        return null;
    }
}
