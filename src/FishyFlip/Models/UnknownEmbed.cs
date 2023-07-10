// <copyright file="UnknownEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Unknown Embed.
/// </summary>
public class UnknownEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownEmbed"/> class.
    /// </summary>
    /// <param name="type">Embed Type.</param>
    public UnknownEmbed(string type)
    {
        this.Type = type;
    }
}