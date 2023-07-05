// <copyright file="AtUriExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;

namespace FishyFlip.Tools;

public static class AtUriExtensions
{
    public static AtUri ToAtUri(this AtHandler handler)
        => new AtUri(handler.ToString());

    public static AtUri ToAtUri(this AtDid did)
        => new AtUri(did.ToString());
}