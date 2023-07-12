// <copyright file="AtHandlerJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

public class AtHandlerJsonConverter : JsonConverter<ATHandler>
{
    public override ATHandler? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            var atUri = new ATUri(value);
            return atUri.Handler;
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, ATHandler? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}