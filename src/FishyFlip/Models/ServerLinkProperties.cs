// <copyright file="ServerLinkProperties.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the properties of a server link, including the terms of service and privacy policy.
/// </summary>
public record ServerLinkProperties(string TermsOfService, string PrivacyPolicy);