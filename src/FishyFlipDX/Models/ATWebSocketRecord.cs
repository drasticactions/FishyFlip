// <copyright file="ATWebSocketRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon;
using FishyFlip.Lexicon.App.Bsky.Actor;
using FishyFlip.Lexicon.Com.Atproto.Sync;
using FishyFlip.Tools.Json;

namespace FishyFlip.Models;

/// <summary>
/// AT WebSocket Record.
/// </summary>
public class ATWebSocketRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketRecord"/> class.
    /// </summary>
    /// <param name="kind">Type.</param>
    /// <param name="did">Did.</param>
    /// <param name="commit">Commit.</param>
    /// <param name="identity">Identity.</param>
    /// <param name="account">Account.</param>
    [JsonConstructor]
    public ATWebSocketRecord(ATWebSocketEvent kind, ATDid? did, ATWebSocketCommit? commit, Identity? identity, Account? account)
    {
        this.Kind = kind;
        this.Did = did;
        this.Commit = commit;
        this.Identity = identity;
        this.Account = account;
    }

    /// <summary>
    /// Gets the Type.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<ATWebSocketEvent>))]
    public ATWebSocketEvent Kind { get; }

    /// <summary>
    /// Gets the Type.
    /// </summary>
    [Obsolete("Use Kind instead.")]
    public ATWebSocketEvent Type => this.Kind;

    /// <summary>
    /// Gets the Commit.
    /// </summary>
    public ATWebSocketCommit? Commit { get; }

    /// <summary>
    /// Gets the Did.
    /// </summary>
    [JsonConverter(typeof(ATDidJsonConverter))]
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the Identity.
    /// </summary>
    public Identity? Identity { get; }

    /// <summary>
    /// Gets the Account.
    /// </summary>
    public Account? Account { get; }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    [JsonPropertyName("time_us")]
    public long? TimeUs { get; set; }
}