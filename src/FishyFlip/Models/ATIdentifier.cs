// <copyright file="ATIdentifier.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public abstract class ATIdentifier
{
    public static ATIdentifier? Create(string id)
    {
        if (ATDid.IsValid(id))
        {
            return ATDid.Create(id);
        }

        if (ATHandle.IsValid(id))
        {
            return ATHandle.Create(id);
        }

        return null;
    }
}
