// <copyright file="AtUriJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

public class AtUriJsonConverter : JsonConverter<AtUri>
{
    public override AtUri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            return new AtUri(value);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, AtUri? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}