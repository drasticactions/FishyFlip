// <copyright file="InviteCode.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record InviteCode(string Code, int Available, bool Disabled, ATDid ForAccount, ATDid CreatedAt, Used[] Uses);
