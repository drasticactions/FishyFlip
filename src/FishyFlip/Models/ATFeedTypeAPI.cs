// <copyright file="ATFeedTypeAPI.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an abstract base class for ATFeedTypeAPI.
/// </summary>
public abstract class ATFeedTypeAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATFeedTypeAPI"/> class.
    /// </summary>
    /// <param name="uri">The URI of the feed type.</param>
    /// <param name="cid">The CID of the feed type.</param>
    [JsonConstructor]
    public ATFeedTypeAPI(ATUri? uri, ATCid? cid)
    {
        this.Uri = uri;
        this.Cid = cid;
    }

    /// <summary>
    /// Gets the URI.
    /// </summary>
    public ATUri? Uri { get; }

    /// <summary>
    /// Gets the CID.
    /// </summary>
    public ATCid? Cid { get; }
}
