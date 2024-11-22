// <copyright file="ATUriJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// Converts an <see cref="ATUri"/> object to and from JSON.
/// </summary>
public class ATUriJsonConverter : JsonConverter<ATUri?>
{
    /// <inheritdoc/>
    public override ATUri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            return new ATUri(value);
        }
        catch (Exception)
        {
            return default;
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATUri? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
