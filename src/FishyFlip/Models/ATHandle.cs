// <copyright file="ATHandle.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ATHandle : ATIdentifier
{
    protected ATHandle(string ident)
    {
        this.Handle = ident;
    }

    public string Handle { get; }

    public static ATHandle Create(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new ArgumentNullException(nameof(uri));
        }

        HandleValidator.EnsureValidHandle(uri);
        return new ATHandle(uri);
    }

    public override string ToString()
    {
        return this.Handle.ToString();
    }

    internal static ATHandle? Create(ATUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var valid = HandleValidator.EnsureValidHandle(uri.Hostname);
        if (valid)
        {
            return new ATHandle(uri.Hostname);
        }

        return null;
    }
}
