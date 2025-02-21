// <copyright file="ATObjectJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// ATObject Json Converter.
/// </summary>
public class ATObjectJsonConverter : JsonConverter<ATObject?>
{
    private readonly IReadOnlyList<ICustomATObjectJsonConverter> converters;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATObjectJsonConverter"/> class.
    /// </summary>
    /// <param name="converters">A read-only list of JSON converters specific to <see cref="ATObject"/> types. If null, initializes to an empty list.</param>
    public ATObjectJsonConverter(IReadOnlyList<ICustomATObjectJsonConverter>? converters = null)
    {
        this.converters = converters ?? new List<ICustomATObjectJsonConverter>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATObjectJsonConverter"/> class.
    /// </summary>
    public ATObjectJsonConverter()
    {
        this.converters = new List<ICustomATObjectJsonConverter>();
    }

    /// <inheritdoc/>
    public override ATObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ATObjectJsonReader.Read(ref reader, this.converters, options);
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
        else
        {
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
}