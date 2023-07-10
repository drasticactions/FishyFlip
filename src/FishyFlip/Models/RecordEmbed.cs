// <copyright file="RecordEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Record Embed.
/// </summary>
public class RecordEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecordEmbed"/> class.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    public RecordEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Record;
        this.Cid = obj["cid"].ToCid();
        this.Uri = obj["uri"].AsString();
    }

    /// <summary>
    /// Gets the Cid.
    /// </summary>
    public Cid? Cid { get; }

    /// <summary>
    /// Gets the Uri.
    /// </summary>
    public string Uri { get; }
}