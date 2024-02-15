// <copyright file="Ops.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// BlueSky Sync Operations.
/// </summary>
public class Ops
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Ops"/> class.
    /// </summary>
    /// <param name="obj">The CBOR Object Representation.</param>
    public Ops(CBORObject obj)
    {
        this.Action = obj["action"].AsString();
        this.Cid = obj["cid"].ToATCid();
        this.Path = obj["path"].AsString();
    }

    /// <summary>
    /// Gets or sets the CID.
    /// </summary>
    public ATCid? Cid { get; set; }

    /// <summary>
    /// Gets or sets the Path.
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Gets or sets the given action.
    /// </summary>
    public string? Action { get; set; }
}