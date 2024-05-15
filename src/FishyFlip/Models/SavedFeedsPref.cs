// <copyright file="SavedFeedsPref.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a saved feed preference.
/// </summary>
public class SavedFeedsPref : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SavedFeedsPref"/> class.
    /// </summary>
    /// <param name="saved">An array of saved ATRecord objects.</param>
    /// <param name="pinned">An array of pinned ATRecord objects.</param>
    /// <param name="type">The type of the record. Optional.</param>
    public SavedFeedsPref(ATUri[]? saved, ATUri[]? pinned, string? type = default)
        : base(type)
    {
        this.Saved = saved;
        this.Pinned = pinned;
        this.Type = type ?? Constants.ActorTypes.SavedFeedsPref;
    }

    /// <summary>
    /// Gets the saved ATRecord objects.
    /// </summary>
    public ATUri[]? Saved { get; }

    /// <summary>
    /// Gets the pinned ATRecord objects.
    /// </summary>
    public ATUri[]? Pinned { get; }
}