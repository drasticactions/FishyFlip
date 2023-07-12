// <copyright file="AtUriJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

public class AtUriJsonConverter : JsonConverter<ATUri>
{
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

    public override void Write(Utf8JsonWriter writer, ATUri? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}