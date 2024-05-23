// <copyright file="LogCreateMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a log creation message.
/// </summary>
public class LogCreateMessage : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogCreateMessage"/> class.
    /// </summary>
    /// <param name="convoId">The conversation ID.</param>
    /// <param name="message">The message view.</param>
    /// <param name="rev">The revision.</param>
    /// <param name="type">The type of the record. Optional.</param>
    public LogCreateMessage(string convoId, MessageView message, string rev, string? type = default)
        : base(type)
    {
        this.ConvoId = convoId;
        this.Message = message;
        this.Rev = rev;
        this.Type = type ?? Constants.ConversationTypes.LogCreateMessage;
    }

    /// <summary>
    /// Gets the conversation ID.
    /// </summary>
    public string ConvoId { get; }

    /// <summary>
    /// Gets the message view.
    /// </summary>
    public MessageView Message { get; }

    /// <summary>
    /// Gets the revision.
    /// </summary>
    public string Rev { get; }
}