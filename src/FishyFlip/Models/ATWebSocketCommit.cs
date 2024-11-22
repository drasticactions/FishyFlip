// <copyright file="ATWebSocketCommit.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon;
using FishyFlip.Tools.Json;

namespace FishyFlip.Models;

/// <summary>
/// AT WebSocket Commit.
/// </summary>
public class ATWebSocketCommit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketCommit"/> class.
    /// </summary>
    /// <param name="rev">The revision identifier.</param>
    /// <param name="operation">The type of the WebSocket commit.</param>
    /// <param name="collection">The collection name.</param>
    /// <param name="rKey">The record key.</param>
    /// <param name="record">The record associated with the commit.</param>
    /// <param name="cid">The CID associated with the commit.</param>
    public ATWebSocketCommit(string? rev, ATWebSocketCommitType operation, string? collection, string? rKey, ATObject? record, ATCid? cid)
    {
        this.Rev = rev;
        this.Operation = operation;
        this.Collection = collection;
        this.RKey = rKey;
        this.Record = record;
        this.Cid = cid;
    }

    /// <summary>
    /// Gets the revision identifier.
    /// </summary>
    public string? Rev { get; }

    /// <summary>
    /// Gets the type of the WebSocket commit.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<ATWebSocketCommitType>))]
    public ATWebSocketCommitType Operation { get; }

    /// <summary>
    /// Gets the type of the WebSocket commit.
    /// </summary>
    [Obsolete("Use Operation instead.")]
    public ATWebSocketCommitType Type => this.Operation;

    /// <summary>
    /// Gets the collection name.
    /// </summary>
    public string? Collection { get; }

    /// <summary>
    /// Gets the record key.
    /// </summary>
    public string? RKey { get; }

    /// <summary>
    /// Gets the record associated with the commit.
    /// </summary>
    public ATObject? Record { get; }

    /// <summary>
    /// Gets the CID associated with the commit.
    /// </summary>
    [JsonConverter(typeof(ATCidJsonConverter))]
    public ATCid? Cid { get; }
}