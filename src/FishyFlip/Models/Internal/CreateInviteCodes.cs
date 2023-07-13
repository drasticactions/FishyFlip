// <copyright file="CreateInviteCodes.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models.Internal;

public record CreateInviteCodes(int CodeCount, int UseCount = 1, ATDid[]? ForAccounts = default)
{
}
