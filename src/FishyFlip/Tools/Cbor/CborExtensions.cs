// <copyright file="CborExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Cbor;

/// <summary>
/// CBOR Extensions.
/// </summary>
internal static class CborExtensions
{
    /// <summary>
    /// CBOR to ATCid.
    /// </summary>
    /// <param name="obj">Nullable CBORobject.</param>
    /// <param name="logger">ILogger.</param>
    /// <returns>ATCid.</returns>
    public static ATCid? ToATCid(this CBORObject? obj, ILogger? logger = default)
    {
        if (obj is null)
        {
            return null;
        }

        if (obj.IsNull)
        {
            return null;
        }

        try
        {
            switch (obj.Type)
            {
                case CBORType.ByteString:
                    var cid = obj.GetByteString();
                    return Cid.Read(cid);
                case CBORType.TextString:
                    return Cid.Decode(obj.AsString());
            }
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Failed to convert to ATCid.");
        }

        return null;
    }

    /// <summary>
    /// Cast CBOR to string.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    /// <returns>string.</returns>
    public static string? ToRawString(this CBORObject obj)
    {
        if (obj.IsNull)
        {
            return null;
        }

        try
        {
            return obj.AsString();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Cast CBOR to DateTime.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    /// <returns>DateTime.</returns>
    public static DateTime? ToDateTime(this CBORObject obj)
    {
        if (obj.IsNull)
        {
            return null;
        }

        if (obj.IsNumber)
        {
            return null;
        }

        try
        {
            return DateTime.Parse(obj.AsString());
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Cast CBOR to Embed.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    /// <returns>Embed.</returns>
    public static Embed ToEmbed(this CBORObject obj)
    {
        var type = obj["$type"].ToString() ?? string.Empty;
        type = type.Replace("\"", string.Empty);
        switch (type)
        {
            case Constants.EmbedTypes.Record:
                return new RecordEmbed(obj["record"]);
            case Constants.EmbedTypes.Images:
                return new ImagesEmbed(obj["images"]);
            case Constants.EmbedTypes.RecordWithMedia:
                return new RecordWithMediaEmbed(obj["record"], obj["media"]);
            case Constants.EmbedTypes.External:
                return new ExternalEmbed(obj["external"]);
            case Constants.EmbedTypes.Video:
                return new VideoEmbed(obj);
            default:
                return new UnknownEmbed(type);
        }
    }

    /// <summary>
    /// Cast CBOR to Reply.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    /// <returns>Reply.</returns>
    public static Reply? ToReply(this CBORObject? obj)
    {
        if (obj is null)
        {
            return null;
        }

        var root = obj["root"];
        var parent = obj["parent"];
        if (root.IsNull || parent.IsNull)
        {
            return null;
        }

        var rootRef = new ReplyRef(root["cid"].ToATCid()!, ATUri.Create(root["uri"].AsString()));
        var parentRef = new ReplyRef(parent["cid"].ToATCid()!, ATUri.Create(parent["uri"].AsString()));
        return new Reply(rootRef, parentRef);
    }
}