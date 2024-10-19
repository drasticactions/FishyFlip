// <copyright file="ActorAccount.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an actor account.
/// </summary>
/// <param name="Active">Indicates whether the actor account is active.</param>
/// <param name="Did">The decentralized identifier of the actor.</param>
/// <param name="Seq">The sequence number associated with the actor.</param>
/// <param name="Time">The timestamp associated with the actor.</param>
public record ActorAccount(bool Active, ATDid? Did, double Seq, DateTime? Time);