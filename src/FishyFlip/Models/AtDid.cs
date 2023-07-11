// <copyright file="AtDid.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class AtDid : ATIdentifier
{
    protected AtDid(string ident)
    {
        this.Handler = ident;
    }

    public string Handler { get; }

    public static AtDid Create(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        DIDValidator.EnsureValidDid(uri);
        return new AtDid(uri);
    }

    public override string ToString()
    {
        return this.Handler.ToString();
    }

    internal static AtDid? Create(AtUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = DIDValidator.EnsureValidDid(uri.Hostname);
        if (valid)
        {
            return new AtDid(uri.Hostname);
        }

        return null;
    }
}
