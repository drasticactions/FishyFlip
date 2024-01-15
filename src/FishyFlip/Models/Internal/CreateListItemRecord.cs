// <copyright file="CreateListItemRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record CreateListItemRecord(string Collection, string Repo, ListItemRecord Record);