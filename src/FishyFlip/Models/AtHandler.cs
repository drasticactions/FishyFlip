// <copyright file="ATHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ATHandler : ATIdentifier
{
    protected ATHandler(string ident)
    {
        this.Handler = ident;
    }

    public string Handler { get; }

    public static ATHandler Create(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        HandleValidator.EnsureValidHandle(uri);
        return new ATHandler(uri);
    }

    public override string ToString()
    {
        return this.Handler.ToString();
    }

    internal static ATHandler? Create(ATUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = HandleValidator.EnsureValidHandle(uri.Hostname);
        if (valid)
        {
            return new ATHandler(uri.Hostname);
        }

        return null;
    }
}
