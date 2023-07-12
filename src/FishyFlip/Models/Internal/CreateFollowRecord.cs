// <copyright file="CreateFollowRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record CreateFollowRecord(string Collection, string Repo, FollowRecord Record);