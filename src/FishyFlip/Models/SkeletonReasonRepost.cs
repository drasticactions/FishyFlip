// <copyright file="SkeletonReasonRepost.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a skeleton reason for reposting.
/// </summary>
public class SkeletonReasonRepost
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SkeletonReasonRepost"/> class.
    /// </summary>
    /// <param name="reason">Reason value.</param>
    /// <param name="type">The Type.</param>
    [JsonConstructor]
    public SkeletonReasonRepost(ATUri? reason, string? type)
    {
        this.Reason = reason;
        this.Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkeletonReasonRepost"/> class.
    /// </summary>
    /// <param name="obj">CBorObject.</param>
    public SkeletonReasonRepost(CBORObject obj)
    {
        if (obj.ContainsKey("reason"))
        {
            this.Reason = ATUri.Create(obj["reason"].AsString());
        }

        this.Type = obj["$type"].AsString();
    }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("reason")]
    public ATUri? Reason { get; internal set; }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("$type")]
    public string? Type { get; internal set; }
}