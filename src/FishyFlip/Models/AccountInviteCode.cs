// <copyright file="AccountInviteCode.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record AccountInviteCode(string Code, int Available, bool Disabled, ATDid ForAccount, ATDid CreatedAt, Used[] Uses);
