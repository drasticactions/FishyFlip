// <copyright file="ATWebSocketRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// AT WebSocket Record.
/// </summary>
public class ATWebSocketRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketRecord"/> class.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="did">Did.</param>
    /// <param name="commit">Commit.</param>
    /// <param name="identity">Identity.</param>
    /// <param name="account">Account.</param>
    [JsonConstructor]
    public ATWebSocketRecord(ATWebSocketEvent type, ATDid? did, ATWebSocketCommit? commit, ActorIdentity? identity, ActorAccount? account)
    {
        this.Type = type;
        this.Did = did;
        this.Commit = commit;
        this.Identity = identity;
        this.Account = account;
    }

    /// <summary>
    /// Gets the Type.
    /// </summary>
    public ATWebSocketEvent Type { get; }

    /// <summary>
    /// Gets the Commit.
    /// </summary>
    public ATWebSocketCommit? Commit { get; }

    /// <summary>
    /// Gets the Did.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the Identity.
    /// </summary>
    public ActorIdentity? Identity { get; }

    /// <summary>
    /// Gets the Account.
    /// </summary>
    public ActorAccount? Account { get; }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    [JsonPropertyName("time_us")]
    public long? TimeUs { get; set; }
}