// <copyright file="AccountInviteCode.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an account invite code.
/// </summary>
/// <param name="Code">The invite code.</param>
/// <param name="Available">The number of times the invite code can be used.</param>
/// <param name="Disabled">Indicates whether the invite code is disabled.</param>
/// <param name="ForAccount">The account for which the invite code is created.</param>
/// <param name="CreatedAt">The time when the invite code was created.</param>
/// <param name="Uses">An array of uses of the invite code.</param>
public record AccountInviteCode(string Code, int Available, bool Disabled, ATDid ForAccount, ATDid CreatedAt, Used[] Uses);