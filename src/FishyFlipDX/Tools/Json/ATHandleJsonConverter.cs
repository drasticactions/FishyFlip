// <copyright file="ATHandleJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// Provides a converter for the ATHandle type for JSON serialization and deserialization.
/// </summary>
public class ATHandleJsonConverter : JsonConverter<ATHandle>
{
    /// <inheritdoc/>
    public override ATHandle? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            if (ATHandle.TryCreate(value, out ATHandle? uri))
            {
                return uri;
            }
        }
        catch (Exception)
        {
            return default;
        }

        return default;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATHandle? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}