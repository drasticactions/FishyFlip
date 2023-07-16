// <copyright file="ATDid.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ATDid : ATIdentifier
{
    protected ATDid(string ident)
    {
        this.Handler = ident;
    }

    public string Handler { get; }

    public static ATDid? Create(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = DIDValidator.EnsureValidDid(uri);
        if (!valid)
        {
            return null;
        }

        return new ATDid(uri);
    }

    public static bool IsValid(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        return DIDValidator.EnsureValidDid(uri);
    }

    public override string ToString()
    {
        return this.Handler.ToString();
    }

    internal static ATDid? Create(ATUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = DIDValidator.EnsureValidDid(uri.Hostname);
        if (valid)
        {
            return new ATDid(uri.Hostname);
        }

        return null;
    }
}
