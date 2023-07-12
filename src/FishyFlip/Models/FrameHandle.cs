// <copyright file="FrameHandle.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class FrameHandle
{
    public FrameHandle(CBORObject obj)
    {
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Seq = obj["seq"].AsInt32();
        this.Handle = obj["handle"]?.AsString();
        this.Time = obj["time"] is not null ? obj["time"].ToDateTime() : null;
    }

    public ATDid? Did { get; }

    public int Seq { get; }

    public string? Handle { get; }

    public DateTime? Time { get; }
}
