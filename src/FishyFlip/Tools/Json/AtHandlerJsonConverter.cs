// <copyright file="AtHandlerJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Serialization;
using FishyFlip.Models;

namespace FishyFlip.Tools.Json;

public class AtHandlerJsonConverter : JsonConverter<AtHandler>
{
    public override AtHandler? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            var atUri = new AtUri(value);
            return atUri.Handler;
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, AtHandler? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}