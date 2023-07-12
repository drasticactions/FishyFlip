// <copyright file="ExternalEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class ExternalEmbed : Embed
{
    [JsonConstructor]
    public ExternalEmbed(External external, string? type)
        : base(type)
    {
        this.External = external;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RecordEmbed"/> class.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    public ExternalEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Record;
        this.External = new External(obj);
    }


    public External? External { get; }
}