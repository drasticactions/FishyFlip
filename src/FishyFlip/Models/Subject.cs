// <copyright file="Subject.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a subject.
/// </summary>
public class Subject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Subject"/> class.
    /// </summary>
    /// <param name="cid">The Cid value.</param>
    /// <param name="uri">The ATUri value.</param>
    [JsonConstructor]
    public Subject(Cid? cid, ATUri? uri)
    {
        this.Cid = cid;
        this.Uri = uri;
    }

    /// <summary>
    /// Gets the Cid value.
    /// </summary>
    public Cid? Cid { get; }

    /// <summary>
    /// Gets the ATUri value.
    /// </summary>
    public ATUri? Uri { get; }
}