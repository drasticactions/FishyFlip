// <copyright file="SubscribeLabelMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a message for subscribing to a labeler.
/// </summary>
public class SubscribeLabelMessage
{
    /// <summary>
    /// Gets the header of the message.
    /// </summary>
    public FrameHeader? Header { get; internal set; }

    /// <summary>
    /// Gets the atError of the message.
    /// </summary>
    public FrameError? Error { get; internal set; }

    /// <summary>
    /// Gets the info of the message.
    /// </summary>
    public FrameInfo? Info { get; internal set; }

    /// <summary>
    /// Gets the labels of the message.
    /// </summary>
    public FishyFlip.Lexicon.Com.Atproto.Label.Labels? Labels { get; internal set; }
}
