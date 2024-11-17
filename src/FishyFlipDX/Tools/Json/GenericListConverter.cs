// <copyright file="GenericListConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

public class GenericListConverter<T, TConverter> : JsonConverter<List<T?>>
    where TConverter : JsonConverter<T?>, new()
{
    private readonly TConverter elementConverter;

    public GenericListConverter()
    {
        this.elementConverter = new TConverter();
    }

    /// <inheritdoc/>
    public override List<T?>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException($"Expected StartArray token, got {reader.TokenType}");
        }

        var list = new List<T?>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return list;
            }

            var item = this.elementConverter.Read(ref reader, typeof(T), options);
            list.Add(item);
        }

        throw new JsonException("Expected EndArray token");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, List<T?>? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartArray();

        foreach (var item in value)
        {
            this.elementConverter.Write(writer, item, options);
        }

        writer.WriteEndArray();
    }
}

public class GenericListConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        var genericType = typeToConvert.GetGenericTypeDefinition();
        return genericType == typeof(List<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var elementType = typeToConvert.GetGenericArguments()[0];

        // Find the appropriate element converter from the options
        var elementConverter = options.GetConverter(elementType);
        if (elementConverter == null)
        {
            throw new JsonException($"No converter found for element type {elementType}");
        }

        // Create the converter type with the element type and converter type as generic arguments
        var converterType = typeof(GenericListConverter<,>).MakeGenericType(
            elementType,
            elementConverter.GetType());

        // Create an instance of the converter
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}