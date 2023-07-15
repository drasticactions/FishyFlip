// <copyright file="ActorResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record ActorResponse(ActorProfile[]? Actors, string? Cursor);
