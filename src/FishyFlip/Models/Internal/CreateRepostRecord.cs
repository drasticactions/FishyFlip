// <copyright file="CreateRepostRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record CreateRepostRecord(string Collection, string Repo, RepostRecord Record);