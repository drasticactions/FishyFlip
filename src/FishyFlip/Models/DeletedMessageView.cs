// <copyright file="DeletedMessageView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a view of a message in a chat conversation.
/// </summary>
public class DeletedMessageView : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletedMessageView"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the message.</param>
    /// <param name="rev">The revision of the message.</param>
    /// <param name="sender">The sender of the message.</param>
    /// <param name="sentAt">The date and time when the message was sent.</param>
    /// <param name="type">The type of the message. If not provided, defaults to <see cref="Constants.ConversationTypes.DeletedMessageView"/>.</param>
    [JsonConstructor]
    public DeletedMessageView(string id, string rev, ChatSender sender, DateTime sentAt, string? type = default)
        : base(type)
    {
        this.Id = id;
        this.Rev = rev;
        this.Sender = sender;
        this.SentAt = sentAt;
        this.Type = type ?? Constants.ConversationTypes.DeletedMessageView;
    }

    /// <summary>
    /// Gets the unique identifier of the message.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the revision of the message.
    /// </summary>
    public string Rev { get; }

    /// <summary>
    /// Gets the sender of the message.
    /// </summary>
    public ChatSender Sender { get; }

    /// <summary>
    /// Gets the date and time when the message was sent.
    /// </summary>
    public DateTime SentAt { get; }
}