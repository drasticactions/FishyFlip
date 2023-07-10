// <copyright file="ExternalEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ExternalEmbed : Embed
{
    public ExternalEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.External;
    }
}