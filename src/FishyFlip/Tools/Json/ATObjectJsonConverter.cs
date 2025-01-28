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
    public override ATObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Rewind to start of object for full deserialization
        ATObject? atObject = null;

        if (JsonDocument.TryParseValue(ref reader, out var doc))
        {
            var rawText = doc.RootElement.GetRawText();
            if (doc.RootElement.TryGetProperty("$type", out var type))
            {
                var text = type.GetString()?.Trim() ?? string.Empty;
                atObject = ATObject.ToATObject(rawText, text);

                if (atObject is not null)
                {
                    return atObject;
                }

                foreach (var converter in this.converters)
                {
                    if (converter.SupportedTypes.Contains(text))
                    {
                        atObject = converter.Read(rawText, text, options);
                        break;
                    }
                }

                return atObject ?? new UnknownATObject() { Type = text };
            }
        }

        return atObject;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATObject? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            return;
        }

        var bytes = value.ToUtf8Json();
        if (bytes is not null)
        {
            writer.WriteRawValue(bytes);
        }

        foreach (var converter in this.converters)
        {
            if (converter.SupportedTypes.Contains(value.Type))
            {
                converter.Write(writer, value, options);
                return;
            }
        }
    }
}