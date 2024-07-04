// <copyright file="EntryConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using WhiteWindLib.Models.WhiteWind;

namespace WhiteWindLib.Tools;

/// <summary>
/// Entry Converter.
/// </summary>
public class EntryConverter : ICustomATRecordConverter
{
    private readonly SourceGenerationContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntryConverter"/> class.
    /// </summary>
    /// <param name="context">Json Type Info for Media Embed.</param>
    internal EntryConverter(SourceGenerationContext context)
    {
        this.context = context;
        this.SupportedTypes = new List<string> { Constants.WhiteWindTypes.Entry };
    }

    /// <inheritdoc/>
    public IReadOnlyList<string> SupportedTypes { get; }

    /// <inheritdoc/>
    public ATRecord? Read(string embedType, string rawText)
    {
        return JsonSerializer.Deserialize<Entry>(rawText, this.context.Entry);
    }

    /// <inheritdoc/>
    public void Write(Utf8JsonWriter writer, ATRecord value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), this.context);
    }
}