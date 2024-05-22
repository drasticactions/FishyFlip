// <copyright file="ConversationMessages.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a collection of messages in a conversation.
/// </summary>
/// <param name="Messages">An array of <see cref="MessageView"/> instances representing the messages in the conversation.</param>
/// <param name="Cursor">A string representing the cursor for pagination in the conversation.</param>
public record ConversationMessages(MessageView[] Messages, string Cursor);