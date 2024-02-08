// <copyright file="AdminRepoRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents a reference to an admin repository.
/// </summary>
public class AdminRepoRef
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdminRepoRef"/> class.
    /// </summary>
    /// <param name="did">The ATDid.</param>
    [JsonConstructor]
    public AdminRepoRef(ATDid did)
    {
        this.Did = did;
        this.Type = "com.atproto.admin.defs#repoRef";
    }

    /// <summary>
    /// Gets the admin repository's DID (Decentralized Identifier).
    /// </summary>
    public ATDid Did { get; }

    /// <summary>
    /// Gets the type of the admin repository reference.
    /// </summary>
    [JsonPropertyName("$type")]
    public string Type { get; }
}
