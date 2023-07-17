// <copyright file="Profile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;
public class Profile : ATRecord
{
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
    /// Initializes a new instance of the <see cref="Profile"/> class.
    /// </summary>
    public Profile(CBORObject obj)
    {
        this.Type = Constants.ActorTypes.Profile;
        this.Description = obj["description"]?.AsString();
        this.DisplayName = obj["displayName"]?.AsString();
        this.Banner = obj["banner"] is not null ? new Image(obj["banner"]) : null;
        this.Avatar = obj["avatar"] is not null ? new Image(obj["avatar"]) : null;
    }

    public Image? Avatar { get; }

    public Image? Banner { get; }

    public string? DisplayName { get; }

    public string? Description { get; }
}
