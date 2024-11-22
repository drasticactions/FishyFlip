// <copyright file="RecordMessageReceivedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon;

namespace FishyFlip.Events;

/// <summary>
/// Represents the arguments for a record message received event.
/// </summary>
public class RecordMessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecordMessageReceivedEventArgs"/> class.
    /// </summary>
    /// <param name="frameCommit">The frame commit associated with the record.</param>
    /// <param name="record">The record that was received.</param>
    public RecordMessageReceivedEventArgs(FrameCommit frameCommit, ATObject? record)
    {
        this.FrameCommit = frameCommit;
        this.Record = record;
    }

    /// <summary>
    /// Gets the frame commit associated with the record.
    /// </summary>
    public FrameCommit FrameCommit { get; }

    /// <summary>
    /// Gets the record that was received.
    /// </summary>
    public ATObject? Record { get; }
}