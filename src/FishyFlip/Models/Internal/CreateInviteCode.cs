// <copyright file="CreateInviteCode.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models.Internal;

/// <summary>
/// Represents the data for creating an invite code.
/// </summary>
/// <param name="UseCount">The number of times the invite code can be used.</param>
/// <param name="ForAccount">The account for which the invite code is created.</param>
public record CreateInviteCode(int UseCount, ATDid? ForAccount);
