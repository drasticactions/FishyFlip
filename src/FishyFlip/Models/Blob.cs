// <copyright file="Blob.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a blob of binary data.
/// </summary>
public class Blob
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Blob"/> class.
    /// </summary>
    /// <param name="data">The binary data.</param>
    public Blob(byte[]? data)
    {
        this.Data = data;
    }

    /// <summary>
    /// Gets the binary data.
    /// </summary>
    public byte[]? Data { get; }
}
