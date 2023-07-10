// <copyright file="AtHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class AtHandler
{
    protected AtHandler(string ident)
    {
        this.Handler = ident;
    }

    public string Handler { get; }

    public static AtHandler Create(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        HandleValidator.EnsureValidHandle(uri);
        return new AtHandler(uri);
    }

    public override string ToString()
    {
        return this.Handler.ToString();
    }

    internal static AtHandler? Create(AtUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = HandleValidator.EnsureValidHandle(uri.Hostname);
        if (valid)
        {
            return new AtHandler(uri.Hostname);
        }

        return null;
    }
}
