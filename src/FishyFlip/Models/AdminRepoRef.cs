// <copyright file="AdminRepoRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public class AdminRepoRef
{
    [JsonConstructor]
    public AdminRepoRef(ATDid did)
    {
        this.Did = did;
        this.Type = "com.atproto.admin.defs#repoRef";
    }

    public ATDid Did { get; }

    [JsonPropertyName("$type")]
    public string Type { get; }
}
