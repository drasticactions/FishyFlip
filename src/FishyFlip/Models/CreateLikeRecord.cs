// <copyright file="CreateLikeRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record CreateLikeRecord(string Collection, string Repo, LikeRecord Record);