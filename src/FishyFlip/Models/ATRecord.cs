// <copyright file="ATRecord.cs" company="Drastic Actions">
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

        ATRecord? record = null;
        if (blockObj["$type"] is not null)
        {
            switch (blockObj["$type"].AsString())
            {
                case Constants.FeedType.Post:
                    record = new Post(blockObj);
                    break;
                case Constants.FeedType.Like:
                    record = new Like(blockObj, logger);
                    break;
                case Constants.FeedType.Generator:
                    record = new FeedGenerator(blockObj);
                    break;
                case Constants.FeedType.Repost:
                    record = new Repost(blockObj, logger);
                    break;
                case Constants.GraphTypes.Follow:
                    record = new Follow(blockObj);
                    break;
                case Constants.GraphTypes.List:
                    record = new BSList(blockObj);
                    break;
                case Constants.GraphTypes.ListItem:
                    record = new BSListItem(blockObj);
                    break;
                case Constants.GraphTypes.Block:
                    record = new Block(blockObj);
                    break;
                case Constants.ActorTypes.Profile:
                    record = new Profile(blockObj);
                    break;
                case Constants.FeedType.ThreadGate:
                    record = new ThreadGate(blockObj);
                    break;
                default:
                    logger?.LogDebug($"Unknown type: {blockObj["$type"].AsString()}");
                    record = new UnknownRecord(blockObj["$type"].AsString());
                    break;
            }
        }

        return record ?? new UnknownRecord("Unknown");
    }
}
