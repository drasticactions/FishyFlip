﻿// <copyright file="ATRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// AT Record.
/// </summary>
public abstract class ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATRecord"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    [JsonConstructor]
    public ATRecord(string? type)
    {
        this.Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATRecord"/> class.
    /// </summary>
    public ATRecord()
    {
    }

    /// <summary>
    /// Gets the Record Type.
    /// </summary>
    [JsonPropertyName("$type")]
    public string? Type { get; internal set; }

    /// <summary>
    /// Creates an AT Record from a CBORObject.
    /// </summary>
    /// <param name="blockObj">The CBORObject to convert.</param>
    /// <param name="logger">The logger to use. This is optional and defaults to null.</param>
    /// <returns>An AT Record if the conversion is successful; otherwise, null.</returns>
    public static ATRecord FromCBORObject(CBORObject blockObj, ILogger? logger = default)
    {
#if DEBUG
        var rawObj = blockObj.ToJSONString();
        logger?.LogDebug($"Raw Object: {rawObj}");
#endif

        if (blockObj["$type"] is not null)
        {
            switch (blockObj["$type"].AsString())
            {
                case Constants.FeedType.Post:
                    return new Post(blockObj);
                case Constants.FeedType.Like:
                    return new Like(blockObj, logger);
                case Constants.FeedType.Generator:
                    return new FeedGenerator(blockObj);
                case Constants.FeedType.Repost:
                    return new Repost(blockObj, logger);
                case Constants.GraphTypes.Follow:
                    return new Follow(blockObj);
                case Constants.GraphTypes.List:
                    return new BSList(blockObj);
                case Constants.GraphTypes.ListItem:
                    return new BSListItem(blockObj);
                case Constants.GraphTypes.Block:
                    return new Block(blockObj);
                case Constants.ActorTypes.Profile:
                    return new Profile(blockObj);
                case Constants.FeedType.ThreadGate:
                    return new ThreadGate(blockObj);
                default:
                    logger?.LogDebug($"Unknown type: {blockObj["$type"].AsString()}");
                    return new UnknownRecord(blockObj["$type"].AsString());
            }
        }

        return new UnknownRecord(string.Empty);
    }
}
