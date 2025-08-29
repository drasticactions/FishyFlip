// <copyright file="BlueskyAuthenticationSchemeOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authentication;

namespace FishyFlip.AspNetCore.Authentication;

/// <summary>
/// Options for Bluesky cookie authentication scheme.
/// </summary>
public class BlueskyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// Gets or sets the claim type used to store the user's Bluesky DID.
    /// </summary>
    public string DidClaimType { get; set; } = "bluesky_did";

    /// <summary>
    /// Gets or sets the claim type used to store the user's Bluesky handle.
    /// </summary>
    public string HandleClaimType { get; set; } = "bluesky_handle";

    /// <summary>
    /// Gets or sets the claim type used to store the user's email.
    /// </summary>
    public string EmailClaimType { get; set; } = "bluesky_email";

    /// <summary>
    /// Gets or sets the claim type used to store the session ID for session store lookup.
    /// </summary>
    public string SessionIdClaimType { get; set; } = "bluesky_session_id";
}