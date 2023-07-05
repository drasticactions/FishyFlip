// <copyright file="AtIdentifier.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Exceptions;
using FishyFlip.Tools;

namespace FishyFlip.Models;

public class AtIdentifier
{
    protected AtIdentifier(string value, bool isDid)
    {
        this.Value = value;
        this.IsDid = isDid;
        this.IsHandle = !isDid;
    }

    public bool IsDid { get; }

    public bool IsHandle { get; }

    public string Value { get; }

    public override string ToString()
    {
        return this.Value;
    }

    internal static AtIdentifier? Create(AtUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        try
        {
            HandleValidator.EnsureValidHandle(uri.Hostname);
            return new AtIdentifier(uri.Hostname, false);
        }
        catch (InvalidHandleError)
        {
        }

        try
        {
            DIDValidator.EnsureValidDid(uri.Hostname);
            return new AtIdentifier(uri.Hostname, true);
        }
        catch (InvalidDidError)
        {
        }

        return null;
    }
}
