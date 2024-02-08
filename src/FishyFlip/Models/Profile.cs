// <copyright file="Profile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a user profile.
/// </summary>
public class Profile : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Profile"/> class with the specified display name, description, avatar, and banner.
    /// </summary>
    /// <param name="displayName">The display name of the profile.</param>
    /// <param name="description">The description of the profile.</param>
    /// <param name="avatar">The avatar image of the profile.</param>
    /// <param name="banner">The banner image of the profile.</param>
    [JsonConstructor]
    public Profile(string? displayName, string description, Image? avatar, Image? banner)
    {
        this.Type = Constants.ActorTypes.Profile;
        this.Description = description;
        this.DisplayName = displayName;
        this.Banner = banner;
        this.Avatar = avatar;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Profile"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the profile.</param>
    public Profile(CBORObject obj)
    {
        this.Type = Constants.ActorTypes.Profile;
        this.Description = obj["description"]?.AsString();
        this.DisplayName = obj["displayName"]?.AsString();
        this.Banner = obj["banner"] is not null ? new Image(obj["banner"]) : null;
        this.Avatar = obj["avatar"] is not null ? new Image(obj["avatar"]) : null;
    }

    /// <summary>
    /// Gets the avatar image of the profile.
    /// </summary>
    public Image? Avatar { get; }

    /// <summary>
    /// Gets the banner image of the profile.
    /// </summary>
    public Image? Banner { get; }

    /// <summary>
    /// Gets the display name of the profile.
    /// </summary>
    public string? DisplayName { get; }

    /// <summary>
    /// Gets the description of the profile.
    /// </summary>
    public string? Description { get; }
}
