// <copyright file="ATObjectJsonReader.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// Static class for handling ATObject JSON reading operations.
/// </summary>
public static class ATObjectJsonReader
{
    /// <summary>
    /// Reads an ATObject from JSON data.
    /// </summary>
    /// <param name="reader">The UTF8 JSON reader.</param>
    /// <param name="converters">Custom converters for specific ATObject types.</param>
    /// <param name="options">JSON serializer options.</param>
    /// <returns>An ATObject instance.</returns>
    public static ATObject Read(ref Utf8JsonReader reader, IReadOnlyList<ICustomATObjectJsonConverter> converters, JsonSerializerOptions options)
    {
        try
        {
            if (JsonDocument.TryParseValue(ref reader, out var doc))
            {
                using (doc)
                {
                    if (doc.RootElement.TryGetProperty("$type", out var type))
                    {
                        var text = type.GetString()?.Trim() ?? string.Empty;
                        var atObject = ATObject.ToATObject(doc.RootElement, text);
                        if (atObject is not null)
                        {
                            return atObject;
                        }

                        var rawText = doc.RootElement.GetRawText();
                        foreach (var converter in converters)
                        {
                            if (converter.SupportedTypes.Contains(text))
                            {
                                atObject = converter.Read(rawText, text, options);
                                break;
                            }
                        }

                        return atObject ?? new UnknownATObject() { Type = text, Json = rawText };
                    }
                }
            }
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }

        return new UnknownATObject();
    }
}