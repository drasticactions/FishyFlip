// <copyright file="MessageView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a view of a message in a chat conversation.
/// </summary>
public class MessageView : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageView"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the message.</param>
    /// <param name="rev">The revision of the message.</param>
    /// <param name="sender">The sender of the message.</param>
    /// <param name="text">The text content of the message.</param>
    /// <param name="sentAt">The date and time when the message was sent.</param>
    /// <param name="type">The type of the message. If not provided, defaults to <see cref="Constants.ConversationTypes.MessageView"/>.</param>
    [JsonConstructor]
    public MessageView(string id, string rev, ChatSender sender, string text, DateTime sentAt, string? type = default)
        : base(type)
    {
        this.Id = id;
        this.Rev = rev;
        this.Sender = sender;
        this.Text = text;
        this.SentAt = sentAt;
        this.Type = type ?? Constants.ConversationTypes.MessageView;
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
    /// Gets the text content of the message.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets the date and time when the message was sent.
    /// </summary>
    public DateTime SentAt { get; }
}