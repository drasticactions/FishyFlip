// <copyright file="LeaveConvoResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Response to leaving a conversation.
/// </summary>
/// <param name="ConvoId">Conversation id.</param>
/// <param name="Rev">Rev Id.</param>
public record LeaveConvoResponse(string ConvoId, string Rev);