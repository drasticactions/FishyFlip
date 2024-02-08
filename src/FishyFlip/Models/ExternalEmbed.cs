// <copyright file="ExternalEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed that contains external content.
/// </summary>
public class ExternalEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalEmbed"/> class with the specified external content and type.
    /// </summary>
    /// <param name="external">The external content.</param>
    /// <param name="type">The type of the embed.</param>
    [JsonConstructor]
    public ExternalEmbed(External external, string? type)
        : base(type)
    {
        this.External = external;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalEmbed"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject representing the external content.</param>
    public ExternalEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Record;
        this.External = new External(obj);
    }

    /// <summary>
    /// Gets the external content of the embed.
    /// </summary>
    public External? External { get; }
}