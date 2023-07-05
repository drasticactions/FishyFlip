// <copyright file="Like.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record Like(Profile Actor, DateTime CreatedAt, DateTime IndexedAt);