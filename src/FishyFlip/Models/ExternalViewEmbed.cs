// <copyright file="ExternalViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed for an external view.
/// </summary>
public class ExternalViewEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalViewEmbed"/> class.
    /// </summary>
    /// <param name="external">The external view.</param>
    public ExternalViewEmbed(ExternalView external)
    {
        this.External = external;
        this.Type = Constants.EmbedTypes.ExternalView;
    }

    /// <summary>
    /// Gets the external view.
    /// </summary>
    public ExternalView External { get; }
}