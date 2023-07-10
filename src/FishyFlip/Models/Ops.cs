// <copyright file="Ops.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Org.BouncyCastle.Ocsp;

namespace FishyFlip.Models;

/// <summary>
/// BlueSky Sync Operations.
/// </summary>
public class Ops
{
    public Ops(CBORObject obj)
    {
        this.Action = obj["action"].AsString();
        this.Cid = obj["cid"].ToCid();
        this.Path = obj["path"].AsString();
    }

    /// <summary>
    /// Gets or sets the CID.
    /// </summary>
    public Cid? Cid { get; set; }

    /// <summary>
    /// Gets or sets the Path.
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Gets or sets the given action.
    /// </summary>
    public string? Action { get; set; }
}