// <copyright file="RecordEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Record Embed.
/// </summary>
public class RecordEmbed : Embed
{
    [JsonConstructor]
    public RecordEmbed(Record? record, string? type)
        : base(type)
    {
        this.Record = record;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RecordEmbed"/> class.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    public RecordEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Record;
        this.Record = new Record(obj);
    }

    public Record? Record { get; }
}

public class Record
{
    [JsonConstructor]
    public Record(Cid? cid, string? uri)
    {
        this.Cid = cid;
        this.Uri = uri;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Record"/> class.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    public Record(CBORObject obj)
    {
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
    public string? Uri { get; }
}