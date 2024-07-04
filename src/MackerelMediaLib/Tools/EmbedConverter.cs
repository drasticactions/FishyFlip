// <copyright file="EmbedConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;
using MackerelMediaLib.Models;

namespace MackerelMediaLib.Tools;

/// <summary>
/// Converts JSON to <see cref="Embed"/> objects and vice versa.
/// </summary>
public class EmbedConverter : ICustomEmbedConverter
{
    private readonly SourceGenerationContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbedConverter"/> class.
    /// </summary>
    /// <param name="context">Json Type Info for Media Embed.</param>
    internal EmbedConverter(SourceGenerationContext context)
    {
        this.context = context;
        this.SupportedTypes = new List<string>() { Constants.MackerelMediaTypes.MediaEmbed };
    }

    /// <summary>
    /// Gets the list of supported types by the converter.
    /// </summary>
    public IReadOnlyList<string> SupportedTypes { get; }

    /// <summary>
    /// Reads and converts the JSON into an <see cref="Embed"/> object.
    /// </summary>
    /// <param name="embedType">The reader for reading the JSON.</param>
    /// <param name="rawText">The type of object to convert to.</param>
    /// <returns>An <see cref="Embed"/> object or null if the conversion is not possible.</returns>
    public Embed? Read(string embedType, string rawText)
    {
        return JsonSerializer.Deserialize<MediaEmbed>(rawText, this.context.MediaEmbed);
    }

    /// <inheritdoc/>
    public void Write(Utf8JsonWriter writer, Embed value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), this.context);
    }
}