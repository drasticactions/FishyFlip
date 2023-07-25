// <copyright file="ExternalViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ExternalViewEmbed : Embed
{
    public ExternalViewEmbed(ExternalView external)
    {
        this.External = external;
        this.Type = Constants.EmbedTypes.ExternalView;
    }

    public ExternalView External { get; }
}

public record ExternalView(string Thumb, string Title, string Uri, string Description);