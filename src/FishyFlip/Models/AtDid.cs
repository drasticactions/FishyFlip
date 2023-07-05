// <copyright file="AtDid.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Exceptions;
using FishyFlip.Tools;

namespace FishyFlip.Models;

public class AtDid
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

        try
        {
            DIDValidator.EnsureValidDid(uri.Hostname);
            return new AtDid(uri.Hostname);
        }
        catch (InvalidDidError)
        {
        }

        return null;
    }
}