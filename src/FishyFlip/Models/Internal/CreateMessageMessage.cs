// <copyright file="CreateMessageMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.Internal;

/// <summary>
/// Create Message, with the message.
/// </summary>
/// <param name="Text">Text of message.</param>
internal record CreateMessageMessage(string Text);