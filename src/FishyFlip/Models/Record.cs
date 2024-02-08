// <copyright file="Record.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a record in the FishyFlip application.
/// </summary>
public class Record
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Record"/> class with the specified Cid and Uri.
    /// </summary>
    /// <param name="cid">The Cid of the record.</param>
    /// <param name="uri">The Uri of the record.</param>
    [JsonConstructor]
    public Record(Cid? cid, string? uri)
    {
        this.Cid = cid;
        this.Uri = uri;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Record"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject containing the record data.</param>
    public Record(CBORObject obj)
    {
        this.Cid = obj["cid"].ToCid();
        this.Uri = obj["uri"].AsString();
    }

    /// <summary>
    /// Gets the Cid of the record.
    /// </summary>
    public Cid? Cid { get; }

    /// <summary>
    /// Gets the Uri of the record.
    /// </summary>
    public string? Uri { get; }
}