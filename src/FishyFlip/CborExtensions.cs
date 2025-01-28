// <copyright file="CborExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Lexicon;

/// <summary>
/// CBOR Extensions.
/// </summary>
public static class CborExtensions
{
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
    /// CBOR to ATHandle.
    /// </summary>
    /// <param name="obj">Nullable CBOR Object.</param>
    /// <param name="logger">ILogger.</param>
    /// <returns>ATHandle.</returns>
    public static ATHandle? ToATHandle(this CBORObject? obj, ILogger? logger = default)
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
            return ATHandle.Create(obj.AsString());
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Failed to convert to ATHandle.");
        }

        return null;
    }

    /// <summary>
    /// CBOR to ATUri.
    /// </summary>
    /// <param name="obj">Nullable CBOR Object.</param>
    /// <param name="logger">ILogger.</param>
    /// <returns>ATUri.</returns>
    public static ATUri? ToATUri(this CBORObject? obj, ILogger? logger = default)
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
            return ATUri.Create(obj.AsString());
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Failed to convert to ATUri.");
        }

        return null;
    }

    /// <summary>
    /// CBOR to ATIdentifier.
    /// </summary>
    /// <param name="obj">Nullable CBOR Object.</param>
    /// <param name="logger">ILogger.</param>
    /// <returns>ATIdentifier.</returns>
    public static ATIdentifier? ToATIdentifier(this CBORObject? obj, ILogger? logger = default)
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
            return ATIdentifier.Create(obj.AsString());
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Failed to convert to ATIdentifier.");
        }

        return null;
    }

    /// <summary>
    /// CBOR to ATDid.
    /// </summary>
    /// <param name="obj">Nullable CBOR Object.</param>
    /// <param name="logger">ILogger.</param>
    /// <returns>ATDid.</returns>
    public static ATDid? ToATDid(this CBORObject? obj, ILogger? logger = default)
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
            return ATDid.Create(obj.AsString());
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Failed to convert to ATDid.");
        }

        return null;
    }

    /// <summary>
    /// CBOR to ATCid.
    /// </summary>
    /// <param name="obj">Nullable CBOR Object.</param>
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
                    if (cid[0] != 0)
                    {
                        logger?.LogError("ATCid CBOR bytes should start with 0.");
                        return null;
                    }

                    return Cid.Read(cid.AsSpan(1).ToArray());
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
}