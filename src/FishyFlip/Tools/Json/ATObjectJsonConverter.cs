// <copyright file="ATObjectJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// ATObject Json Converter.
/// </summary>
public class ATObjectJsonConverter : JsonConverter<ATObject?>
{
    private readonly IReadOnlyList<ICustomATObjectConverter> converters;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATObjectJsonConverter"/> class.
    /// </summary>
    /// <param name="converters">A read-only list of JSON converters specific to <see cref="ATObject"/> types. If null, initializes to an empty list.</param>
    public ATObjectJsonConverter(IReadOnlyList<ICustomATObjectConverter>? converters = null)
    {
        this.converters = converters ?? new List<ICustomATObjectConverter>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATObjectJsonConverter"/> class.
    /// </summary>
    public ATObjectJsonConverter()
    {
        this.converters = new List<ICustomATObjectConverter>();
    }

    /// <inheritdoc/>
    public override ATObject? Read(ref Utf8JsonReader clonedReader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Rewind to start of object for full deserialization
        Utf8JsonReader reader = clonedReader;

        ATObject? atObject = null;

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected start of object");
        }

        // Read the first property which should be $type
        reader.Read();
        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException("Expected property name");
        }

        string? propertyName = reader.GetString();
        if (propertyName != "$type")
        {
            throw new JsonException("First property must be $type");
        }

        // Read the type value
        reader.Read();
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected string value for $type");
        }

        string typeValue = reader.GetString() ?? throw new JsonException("$type value cannot be null");

        atObject = ATObject.ToATObject(ref clonedReader, typeValue);

        if (atObject is not null)
        {
            return atObject;
        }

        foreach (var converter in this.converters)
        {
            if (converter.SupportedTypes.Contains(typeValue))
            {
                atObject = converter.Read(clonedReader, typeValue, options);
                break;
            }
        }

        return atObject ?? new UnknownATObject() { Type = typeValue };
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATObject? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteStringValue(value.ToJson());
        }
    }
}