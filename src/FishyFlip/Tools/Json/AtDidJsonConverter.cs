﻿// <copyright file="AtDidJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

public class AtDidJsonConverter : JsonConverter<ATDid>
{
    public override ATDid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            var atUri = new ATUri(value);
            return atUri.Did;
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, ATDid? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
