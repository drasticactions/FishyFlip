// <copyright file="ActorRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an actor record.
/// </summary>
public class ActorRecord : ATFeedTypeAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActorRecord"/> class.
    /// </summary>
    /// <param name="value">The profile of the actor.</param>
    /// <param name="uri">The ATUri of the actor.</param>
    /// <param name="cid">The Cid of the action.</param>
    [JsonConstructor]
    public ActorRecord(Profile? value, ATUri? uri, Cid? cid)
        : base(uri, cid)
    {
        this.Value = value;
    }

    /// <summary>
    /// Gets the profile value of the actor record.
    /// </summary>
    public Profile? Value { get; }
}