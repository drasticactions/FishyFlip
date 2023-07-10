// <copyright file="CborExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Cbor;

internal static class CborExtensions
{
    /// <summary>
    /// CBOR to Cid.
    /// </summary>
    /// <param name="obj">Nullable CBORobject.</param>
    /// <returns>Cid.</returns>
    public static Cid? ToCid(this CBORObject? obj)
    {
        if (obj is null)
        {
            return null;
        }

        if (obj.IsNull)
        {
            return null;
        }

        switch (obj.Type)
        {
            case CBORType.ByteString:
                var cid = obj.GetByteString();
                return Cid.Read(cid);
            case CBORType.TextString:
                return Cid.Decode(obj.AsString());
        }

        return null;
    }

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

        return DateTime.Parse(obj.AsString());
    }

    public static Embed ToEmbed(this CBORObject obj)
    {
        var type = obj["$type"].AsString();
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
        }

        return new UnknownEmbed(type);
    }
}