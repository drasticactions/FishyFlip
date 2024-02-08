// <copyright file="Embed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an abstract class for embedding content.
/// </summary>
public abstract class Embed : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Embed"/> class with the specified type.
    /// </summary>
    /// <param name="type">The type of the embed.</param>
    public Embed(string? type)
        : base(type)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Embed"/> class.
    /// </summary>
    public Embed()
    {
    }
}
