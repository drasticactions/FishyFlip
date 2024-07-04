// <copyright file="ICustomEmbedConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// Defines the interface for custom embed converters used in JSON serialization and deserialization.
/// </summary>
public interface ICustomEmbedConverter
{
    /// <summary>
    /// Gets the list of supported types by the converter.
    /// </summary>
    IReadOnlyList<string> SupportedTypes { get; }

    /// <summary>
    /// Reads and converts the JSON into an <see cref="Embed"/> object.
    /// </summary>
    /// <param name="embedType">The reader for reading the JSON.</param>
    /// <param name="rawText">The type of object to convert to.</param>
    /// <returns>An <see cref="Embed"/> object or null if the conversion is not possible.</returns>
    Embed? Read(string embedType, string rawText);

    /// <summary>
    /// Writes the <see cref="Embed"/> object to the JSON writer.
    /// </summary>
    /// <param name="writer">The writer for writing the JSON.</param>
    /// <param name="value">The <see cref="Embed"/> value to write.</param>
    /// <param name="options">Options for the serializer.</param>
    void Write(Utf8JsonWriter writer, Embed value, JsonSerializerOptions options);
}