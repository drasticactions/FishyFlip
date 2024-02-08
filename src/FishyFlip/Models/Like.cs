// <copyright file="Like.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a Like record.
/// </summary>
public class Like : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Like"/> class.
    /// </summary>
    /// <param name="subject">The subject of the Like.</param>
    /// <param name="createdAt">The creation date of the Like.</param>
    /// <param name="type">The type of the Like.</param>
    [JsonConstructor]
    public Like(Subject? subject, DateTime? createdAt, string? type)
        : base(type)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Like"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object representing the Like.</param>
    /// <param name="logger">The logger to use for logging.</param>
    public Like(CBORObject obj, ILogger? logger = default)
    {
        var cid = obj["subject"]["cid"].ToCid(logger);
        var uri = new ATUri(obj["subject"]["uri"].AsString());
        this.Subject = new Subject(cid, uri);
        this.CreatedAt = obj["createdAt"].ToDateTime();
        this.Type = Constants.FeedType.Like;
    }

    /// <summary>
    /// Gets the subject of the Like.
    /// </summary>
    public Subject? Subject { get; }

    /// <summary>
    /// Gets the creation date of the Like.
    /// </summary>
    public DateTime? CreatedAt { get; }
}