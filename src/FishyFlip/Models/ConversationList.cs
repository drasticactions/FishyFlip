// <copyright file="ConversationList.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a list of conversations.
/// </summary>
/// <param name="Convos">An array of Conversation objects.</param>
/// <param name="Cursor">An optional string that represents the cursor position in the list. Default is an empty string.</param>
public record ConversationList(Conversation[] Convos, string Cursor = "");