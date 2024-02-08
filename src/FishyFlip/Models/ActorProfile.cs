// <copyright file="ActorProfile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an actor profile.
/// </summary>
/// <param name="Did">The actor's DID (Decentralized Identifier).</param>
/// <param name="Handle">The actor's handle.</param>
/// <param name="DisplayName">The actor's display name.</param>
/// <param name="Description">The actor's description.</param>
/// <param name="Avatar">The URL of the actor's avatar.</param>
/// <param name="IndexedAt">The date and time when the actor profile was indexed.</param>
/// <param name="Viewer">The viewer information.</param>
/// <param name="Labels">An array of labels associated with the actor profile.</param>
public record ActorProfile(ATDid? Did, string? Handle, string? DisplayName, string? Description, string? Avatar, DateTime? IndexedAt, Viewer? Viewer, Label[]? Labels);
