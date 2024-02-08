// <copyright file="DescribeServer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a server description.
/// </summary>
/// <param name="AvailableUserDomains">The available user domains.</param>
/// <param name="InviteCodeRequired">Indicates if an invite code is required to join the server.</param>
/// <param name="Links">The server link properties.</param>
public record DescribeServer(string[] AvailableUserDomains, bool InviteCodeRequired, ServerLinkProperties Links);
