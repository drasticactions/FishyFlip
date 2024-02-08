// <copyright file="InviteCodes.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record that contains an account name and an array of invite codes.
/// </summary>
public record InviteCodes(string Account, string[] Codes);