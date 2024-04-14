// <copyright file="ThreadGateReason.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// ThreadGate Reason.
/// </summary>
public class ThreadGateReason
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadGateReason"/> class.
    /// </summary>
    /// <param name="list">List value.</param>
    /// <param name="type">The Type.</param>
    [JsonConstructor]
    public ThreadGateReason(ATUri? list, string? type)
    {
        this.List = list;
        this.Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadGateReason"/> class.
    /// </summary>
    /// <param name="obj">CBorObject.</param>
    public ThreadGateReason(CBORObject obj)
    {
        if (obj.ContainsKey("list"))
        {
            this.List = ATUri.Create(obj["list"].AsString());
        }

        this.Type = obj["$type"].AsString();
    }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("list")]
    public ATUri? List { get; internal set; }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("$type")]
    public string? Type { get; internal set; }

    /// <summary>
    /// Creates a new ThreadGateReason instance for a MentionRule.
    /// </summary>
    /// <returns>A new ThreadGateReason instance with the Type set to MentionRule and the List set to null.</returns>
    public static ThreadGateReason CreateMentionRule() =>
        new ThreadGateReason(null, Constants.ThreadGateReasonType.MentionRule);

    /// <summary>
    /// Creates a new ThreadGateReason instance for a FollowingRule.
    /// </summary>
    /// <returns>A new ThreadGateReason instance with the Type set to FollowingRule and the List set to null.</returns>
    public static ThreadGateReason CreateFollowingRule() =>
        new ThreadGateReason(null, Constants.ThreadGateReasonType.FollowingRule);

    /// <summary>
    /// Creates a new ThreadGateReason instance for a ListRule.
    /// </summary>
    /// <param name="uri">The ATUri of the list associated with the ListRule.</param>
    /// <returns>A new ThreadGateReason instance with the Type set to ListRule and the List set to the provided uri.</returns>
    public static ThreadGateReason CreateListRule(ATUri uri) =>
        new ThreadGateReason(uri, Constants.ThreadGateReasonType.ListRule);
}