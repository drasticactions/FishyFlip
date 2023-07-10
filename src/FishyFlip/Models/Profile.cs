// <copyright file="Profile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;
public class Profile : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Profile"/> class.
    /// </summary>
    public Profile(CBORObject obj)
    {
        this.Type = Constants.ActorTypes.Profile;
    }
}
