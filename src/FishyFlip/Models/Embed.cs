// <copyright file="Embed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public abstract class Embed : ATRecord
{
    public Embed(string? type)
        : base(type)
    {
    }

    public Embed()
    {
    }
}
