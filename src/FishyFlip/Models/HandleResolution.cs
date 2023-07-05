// <copyright file="HandleResolution.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace FishyFlip.Models;

public class HandleResolution
{
    [JsonConstructor]
    public HandleResolution(AtUri did)
    {
        this.Did = did;
    }

    public AtUri Did { get; }
}