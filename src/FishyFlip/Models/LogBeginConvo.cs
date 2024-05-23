// <copyright file="LogBeginConvo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a log begin conversation message.
/// </summary>
public class LogBeginConvo : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogBeginConvo"/> class.
    /// </summary>
    /// <param name="convoId">The conversation ID.</param>
    /// <param name="rev">The revision.</param>
    /// <param name="type">The type of the record. Optional.</param>
    public LogBeginConvo(string convoId, string rev, string? type = default)
        : base(type)
    {
        this.ConvoId = convoId;
        this.Rev = rev;
        this.Type = type ?? Constants.ConversationTypes.LogBeginConvo;
    }

    /// <summary>
    /// Gets the conversation ID.
    /// </summary>
    public string ConvoId { get; }

    /// <summary>
    /// Gets the revision.
    /// </summary>
    public string Rev { get; }
}