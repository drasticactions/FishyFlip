// <copyright file="CidConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

public class CidConverter : JsonConverter<Cid?>
{
    public override Cid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (JsonDocument.TryParseValue(ref reader, out var doc))
        {
            if (doc.RootElement.ValueKind == JsonValueKind.Object)
            {
                if (doc.RootElement.TryGetProperty("$link", out var type))
                {
                    var typeString = type.GetString()?.Trim() ?? string.Empty;
                    return string.IsNullOrEmpty(typeString) ? null : Cid.Decode(typeString);
                }
            }

            if (doc.RootElement.ValueKind is JsonValueKind.String)
            {
                var typeString = doc.RootElement.GetString()?.Trim() ?? string.Empty;
                return string.IsNullOrEmpty(typeString) ? null : Cid.Decode(typeString);
            }
        }

        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            return Cid.Decode(value);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, Cid? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}