// <copyright file="CreateInviteCodes.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents the parameters for creating invite codes.
/// </summary>
/// <param name="CodeCount">The number of invite codes to create.</param>
/// <param name="UseCount">The number of times each invite code can be used.</param>
/// <param name="ForAccounts">The array of account IDs for which the invite codes are intended.</param>
public record CreateInviteCodes(int CodeCount, int UseCount = 1, ATDid[]? ForAccounts = default)
{
}
