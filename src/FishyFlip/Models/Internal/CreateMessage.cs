// <copyright file="CreateMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Create Message.
/// </summary>
/// <param name="ConvoId">Conversation Id.</param>
/// <param name="Message">Message.</param>
internal record CreateMessage(string ConvoId, CreateMessageMessage Message);