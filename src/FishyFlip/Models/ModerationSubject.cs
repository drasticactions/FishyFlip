// <copyright file="ModerationSubject.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents a moderation subject.
/// </summary>
public class ModerationSubject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModerationSubject"/> class.
    /// </summary>
    /// <param name="type">The type of the moderation subject.</param>
    /// <param name="uri">The URI of the moderation subject.</param>
    /// <param name="cid">The CID of the moderation subject.</param>
    /// <param name="did">The ATDID of the moderation subject.</param>
    [JsonConstructor]
    public ModerationSubject(string type, string? uri, Cid? cid, ATDid? did)
    {
        this.Uri = uri;
        this.Type = type;
        this.Cid = cid;
        this.Did = did;
    }

    /// <summary>
    /// Gets the ATDID of the moderation subject.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the URI of the moderation subject.
    /// </summary>
    public string? Uri { get; }

    /// <summary>
    /// Gets the CID of the moderation subject.
    /// </summary>
    public Cid? Cid { get; }

    /// <summary>
    /// Gets the type of the moderation subject.
    /// </summary>
    [JsonPropertyName("$type")]
    public string Type { get; }
}
