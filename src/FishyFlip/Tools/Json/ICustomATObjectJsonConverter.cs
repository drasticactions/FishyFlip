// <copyright file="ICustomATObjectJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// Defines the interface for custom ATObject converters used in JSON serialization and deserialization.
/// </summary>
public interface ICustomATObjectJsonConverter
{
    /// <summary>
    /// Gets the list of supported types by the converter.
    /// </summary>
    IReadOnlyList<string> SupportedTypes { get; }

    /// <summary>
    /// Reads and converts the JSON into an <see cref="ATObject"/> object.
    /// </summary>
    /// <param name="text">The JSON text.</param>
    /// <param name="type">The type of ATObject to deserialize.</param>
    /// <param name="options">Options for the serializer.</param>
    /// <returns>An <see cref="ATObject"/> object or null if the conversion is not possible.</returns>
    ATObject? Read(string text, string type, JsonSerializerOptions options);

    /// <summary>
    /// Writes the <see cref="ATObject"/> object to the JSON writer.
    /// </summary>
    /// <param name="writer">The writer for writing the JSON.</param>
    /// <param name="value">The <see cref="ATObject"/> value to write.</param>
    /// <param name="options">Options for the serializer.</param>
    void Write(Utf8JsonWriter writer, ATObject value, JsonSerializerOptions options);
}