// <copyright file="ActorResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the response containing actor data.
/// </summary>
public record ActorResponse(ActorProfile[]? Actors, string? Cursor);
