// <copyright file="DescribeServer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record DescribeServer(string[] AvailableUserDomains, bool InviteCodeRequired, ServerLinkProperties Links);
