// <copyright file="FacetActorIdentifier.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Facet Actor Identifier, used to identify an actor in a facet to create a mention.
/// The Handle and Did are not validated to be for the same actor.
/// </summary>
/// <param name="Handle"><see cref="ATHandle."/>.</param>
/// <param name="Did"><see cref="ATDid"/>.</param>
public record FacetActorIdentifier(ATHandle Handle, ATDid Did);