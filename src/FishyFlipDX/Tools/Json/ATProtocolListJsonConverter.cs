using FishyFlip.Lexicon;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Json;

public class ATProtocolListJsonConverter : JsonConverter<List<ATObject?>>
{
    private readonly ATProtocolJsonConverter _elementConverter;

    public ATProtocolListJsonConverter(ATProtocolJsonConverter elementConverter)
    {
        _elementConverter = elementConverter;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(List<ATObject?>);
    }

    public override List<ATObject?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array");
        }

        var list = new List<ATObject?>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                break;
            }

            // Handle null elements
            if (reader.TokenType == JsonTokenType.Null)
            {
                list.Add(null);
                continue;
            }

            try
            {
                var element = _elementConverter.Read(ref reader, typeof(ATObject), options);
                list.Add(element);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Error reading ATObject element: {ex.Message}", ex);
            }
        }

        return list;
    }

    public override void Write(Utf8JsonWriter writer, List<ATObject?> value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartArray();

        foreach (var element in value)
        {
            if (element == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                _elementConverter.Write(writer, element, options);
            }
        }

        writer.WriteEndArray();
    }
}
