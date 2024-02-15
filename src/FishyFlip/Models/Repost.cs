// <copyright file="Repost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a repost in the application.
/// </summary>
public class Repost : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Repost"/> class.
    /// </summary>
    /// <param name="cid">The unique identifier of the repost.</param>
    /// <param name="uri">The URI of the repost.</param>
    /// <param name="createdAt">The creation date and time of the repost.</param>
    /// <param name="type">The type of the repost.</param>
    [JsonConstructor]
    public Repost(ATCid? cid, ATUri? uri, DateTime? createdAt, string? type)
        : base(type)
    {
        this.Cid = cid;
        this.Uri = uri;
        this.CreatedAt = createdAt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Repost"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the repost.</param>
    /// <param name="logger">The logger to use for logging.</param>
    public Repost(CBORObject obj, ILogger? logger = default)
    {
        this.Cid = obj["subject"]["cid"].ToATCid(logger);
        this.Uri = new ATUri(obj["subject"]["uri"].AsString());
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.FeedType.Repost;
    }

    /// <summary>
    /// Gets the unique identifier of the repost.
    /// </summary>
    public ATCid? Cid { get; }

    /// <summary>
    /// Gets the URI of the repost.
    /// </summary>
    public ATUri? Uri { get; }

    /// <summary>
    /// Gets the creation date and time of the repost.
    /// </summary>
    public DateTime? CreatedAt { get; }
}