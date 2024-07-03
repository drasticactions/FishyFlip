// <copyright file="MediaRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MackerelMediaLib.Models;

/// <summary>
/// Represents a media record in the Mackerel Media Library.
/// </summary>
/// <remarks>
/// This class extends <see cref="ATFeedTypeAPI"/> to include media-specific information.
/// </remarks>
public class MediaRecord : ATFeedTypeAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MediaRecord"/> class.
    /// </summary>
    /// <param name="uri">The URI associated with the media record.</param>
    /// <param name="cid">The content identifier for the media.</param>
    /// <param name="value">The media object associated with this record.</param>
    [JsonConstructor]
    public MediaRecord(ATUri? uri, Cid? cid, Media? value)
        : base(uri, cid)
    {
        this.Value = value;
    }

    /// <summary>
    /// Gets the media associated with this record.
    /// </summary>
    /// <value>
    /// The <see cref="Media"/> object if available; otherwise, <c>null</c>.
    /// </value>
    public Media? Value { get; }
}