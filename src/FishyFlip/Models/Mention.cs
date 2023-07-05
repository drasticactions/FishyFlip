// <copyright file="Mention.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools;

namespace FishyFlip.Models;

/// <summary>
/// A mention to an actor.
/// </summary>
public class Mention : FacetFeature
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mention"/> class.
    /// Ctor.
    /// </summary>
    /// <param name="did">A <see cref="Did"/> of the actor.</param>
    public Mention(AtDid did)
    {
        this.Did = did;
    }

    /// <summary>
    /// Gets a <see cref="Did"/> of the actor.
    /// </summary>
    public AtDid Did { get; }

    public override string Type => Constants.FacetTypes.Mention;
}