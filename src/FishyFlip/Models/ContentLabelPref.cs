// <copyright file="ContentLabelPref.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Content Label Preference.
/// </summary>
public class ContentLabelPref : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContentLabelPref"/> class.
    /// </summary>
    /// <param name="label">Label for the content.</param>
    /// <param name="visibility">Value for the level of visibility.</param>
    /// <param name="type">ATRecord Type.</param>
    [JsonConstructor]
    public ContentLabelPref(string label, string visibility, string? type = default)
        : base(type)
    {
        this.Type = type ?? Constants.ActorTypes.ContentLabelPref;
        this.Label = label;
        this.Visibility = visibility;
    }

    /// <summary>
    /// Gets the label for the content.
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// Gets the value for the level of visibility.
    /// </summary>
    public string Visibility { get; }
}