// <copyright file="GenericListConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// Generic List Converter.
/// </summary>
/// <typeparam name="T">Object.</typeparam>
/// <typeparam name="TConverter">Json Converter.</typeparam>
public class GenericListConverter<T, TConverter> : JsonConverter<List<T?>>
    where TConverter : JsonConverter<T?>, new()
{
    private readonly TConverter elementConverter;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericListConverter{T, TConverter}"/> class.
    /// </summary>
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