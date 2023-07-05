// <copyright file="AtIdentifier.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Exceptions;
using FishyFlip.Tools;

namespace FishyFlip.Models;

public class AtIdentifier
{
    protected AtIdentifier(AtHandler handler)
    {
        this.Handler = handler;
    }

    protected AtIdentifier(AtDid did)
    {
        this.Did = did;
    }

    public AtHandler? Handler { get; }

    public AtDid? Did { get; }

    public override string ToString()
    {
        return this.Handler?.ToString() ?? this.Did?.ToString() ?? string.Empty;
    }

    internal static AtIdentifier? Create(AtUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var handler = AtHandler.Create(uri);
        var did = AtDid.Create(uri);
        if (handler != null)
        {
            return new AtIdentifier(handler);
        }
        else if (did != null)
        {
            return new AtIdentifier(did);
        }

        return null;
    }
}
