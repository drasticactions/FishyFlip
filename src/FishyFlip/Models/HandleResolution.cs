// <copyright file="HandleResolution.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class HandleResolution
{
    [JsonConstructor]
    public HandleResolution(ATDid? did)
    {
        this.Did = did;
    }

    public ATDid? Did { get; }
}
