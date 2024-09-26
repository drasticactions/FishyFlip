// <copyright file="ActorIdentity.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the identity of an actor.
/// </summary>
/// <param name="Did">The decentralized identifier of the actor.</param>
/// <param name="Handle">The handle of the actor.</param>
/// <param name="Seq">The sequence number associated with the actor.</param>
/// <param name="Time">The timestamp associated with the actor.</param>
public record ActorIdentity(ATDid? Did, string? Handle, double Seq, DateTime? Time);