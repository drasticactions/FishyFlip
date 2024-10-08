// <copyright file="EmbedConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace FishyFlip.Tools.Json;

/// <summary>
/// Converts JSON to <see cref="Embed"/> objects and vice versa.
/// </summary>
public class EmbedConverter : JsonConverter<Embed>
{
    private IReadOnlyList<ICustomEmbedConverter> converters;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbedConverter"/> class.
    /// </summary>
    /// <param name="converters">A read-only list of JSON converters specific to <see cref="Embed"/> types. If null, initializes to an empty list.</param>
    public EmbedConverter(IReadOnlyList<ICustomEmbedConverter>? converters = default)
    {
        this.converters = converters ?? new List<ICustomEmbedConverter>();
    }

    /// <inheritdoc/>
    public override Embed? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (JsonDocument.TryParseValue(ref reader, out var doc))
        {
            if (doc.RootElement.TryGetProperty("$type", out var type))
            {
                var text = type.GetString()?.Trim() ?? string.Empty;
                switch (text)
                {
                    case Constants.EmbedTypes.VideoView:
                        var vidView = JsonSerializer.Deserialize<VideoViewEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).VideoViewEmbed);
                        return vidView;
                    case Constants.EmbedTypes.ImageView:
                        if (doc.RootElement.TryGetProperty("images", out var t))
                        {
                            var item = JsonSerializer.Deserialize<ImageViewEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ImageViewEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.ExternalView:
                        if (doc.RootElement.TryGetProperty("external", out var evm))
                        {
                            var item = JsonSerializer.Deserialize<ExternalViewEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ExternalViewEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.Images:
                        if (doc.RootElement.TryGetProperty("images", out var img))
                        {
                            var item = JsonSerializer.Deserialize<ImagesEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ImagesEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.External:
                        if (doc.RootElement.TryGetProperty("external", out var ext))
                        {
                            var test = ext.GetRawText();
                            var item = JsonSerializer.Deserialize<ExternalEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ExternalEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.Record:
                        if (doc.RootElement.TryGetProperty("record", out var value))
                        {
                            var item = JsonSerializer.Deserialize<RecordEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).RecordEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.Video:
                        if (doc.RootElement.TryGetProperty("video", out var vid))
                        {
                            var item = JsonSerializer.Deserialize<VideoEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).VideoEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.RecordView:
                        if (doc.RootElement.TryGetProperty("record", out var rec))
                        {
                            var item = JsonSerializer.Deserialize<RecordViewEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).RecordViewEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.RecordWithMediaView:
                        RecordViewEmbed? record1 = null;
                        Embed? media1 = null;

                        if (doc.RootElement.TryGetProperty("record", out var recordVal2))
                        {
                            record1 = JsonSerializer.Deserialize<RecordViewEmbed>(recordVal2.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).RecordViewEmbed);
                        }

                        if (doc.RootElement.TryGetProperty("media", out var mediaVal2))
                        {
                            if (mediaVal2.TryGetProperty("$type", out var mediaType2))
                            {
                                var mediaText = mediaType2.GetString()?.Trim() ?? string.Empty;
                                switch (mediaText)
                                {
                                    case Constants.EmbedTypes.ImageView:
                                        media1 = JsonSerializer.Deserialize<ImageViewEmbed>(mediaVal2.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ImageViewEmbed);
                                        break;
                                    case Constants.EmbedTypes.VideoView:
                                        media1 = JsonSerializer.Deserialize<VideoViewEmbed>(mediaVal2.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).VideoViewEmbed);
                                        break;
                                }
                            }
                        }

                        return new RecordWithMediaViewEmbed(record1, media1);
                    case Constants.EmbedTypes.RecordWithMedia:
                        RecordEmbed? record = null;
                        Embed? media = null;

                        if (doc.RootElement.TryGetProperty("record", out var recordVal))
                        {
                            record = JsonSerializer.Deserialize<RecordEmbed>(recordVal.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).RecordEmbed);
                        }

                        if (doc.RootElement.TryGetProperty("media", out var mediaVal))
                        {
                            if (mediaVal.TryGetProperty("$type", out var mediaType))
                            {
                                var mediaText = mediaType.GetString()?.Trim() ?? string.Empty;
                                switch (mediaText)
                                {
                                    case Constants.EmbedTypes.Images:
                                        media = JsonSerializer.Deserialize<ImagesEmbed>(mediaVal.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ImagesEmbed);
                                        break;
                                    case Constants.EmbedTypes.VideoView:
                                        media = JsonSerializer.Deserialize<VideoViewEmbed>(mediaVal.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).VideoViewEmbed);
                                        break;
                                }
                            }
                        }

                        return new RecordWithMediaEmbed(record, media);
                    default:
                        foreach (var converter in this.converters)
                        {
                            if (converter.SupportedTypes.Contains(text))
                            {
                                var item = converter.Read(text, doc.RootElement.GetRawText());
                                if (item is not null)
                                {
                                    return item;
                                }
                            }
                        }

                        break;
                }
            }
        }

        return default;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Embed value, JsonSerializerOptions options)
    {
        foreach (var converter in this.converters)
        {
            if (converter.SupportedTypes.Contains(value.Type))
            {
                converter.Write(writer, value, options);
                return;
            }
        }

        JsonSerializer.Serialize(writer, value, value.GetType(), (JsonSerializerContext)options.TypeInfoResolver!);
    }
}
