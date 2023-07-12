// <copyright file="Blob.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Blob
{
    public Blob(byte[]? data)
    {
        this.Data = data;
    }

    public byte[]? Data { get; }
}
