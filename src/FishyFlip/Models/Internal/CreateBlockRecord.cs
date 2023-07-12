// <copyright file="CreateBlockRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

public record CreateBlockRecord(string Collection, string Repo, BlockRecord Record);