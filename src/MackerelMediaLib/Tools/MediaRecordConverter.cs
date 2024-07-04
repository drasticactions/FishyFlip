// <copyright file="MediaRecordConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using MackerelMediaLib.Models;

namespace MackerelMediaLib.Tools;

/// <summary>
/// Media Record Converter.
/// </summary>
public class MediaRecordConverter : ICustomATRecordConverter
{
    private readonly SourceGenerationContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaRecordConverter"/> class.
    /// </summary>
    /// <param name="context">Json Type Info for Media Embed.</param>
    internal MediaRecordConverter(SourceGenerationContext context)
    {
        this.context = context;
        this.SupportedTypes = new List<string> { Constants.MackerelMediaTypes.Media };
    }

    /// <inheritdoc/>
    public IReadOnlyList<string> SupportedTypes { get; }

    /// <inheritdoc/>
    public ATRecord? Read(string embedType, string rawText)
    {
        return JsonSerializer.Deserialize<Media>(rawText, this.context.Media);
    }

    /// <inheritdoc/>
    public void Write(Utf8JsonWriter writer, ATRecord value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), this.context);
    }
}