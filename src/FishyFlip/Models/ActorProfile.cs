// <copyright file="ActorProfile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record ActorProfile(ATDid? Did, string? Handle, string? DisplayName, string? Description, string? Avatar, DateTime? IndexedAt, Viewer? Viewer, Label[]? Labels);
