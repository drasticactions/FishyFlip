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
                    case Constants.EmbedTypes.RecordView:
                        if (doc.RootElement.TryGetProperty("record", out var rec))
                        {
                            var item = JsonSerializer.Deserialize<RecordViewEmbed>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).RecordViewEmbed);
                            return item;
                        }

                        break;
                    case Constants.EmbedTypes.RecordWithMediaView:
                        RecordViewEmbed? record1 = null;
                        ImageViewEmbed? media1 = null;

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
                                }
                            }
                        }

                        return new RecordWithMediaViewEmbed(record1, media1);
                    case Constants.EmbedTypes.RecordWithMedia:
                        RecordEmbed? record = null;
                        ImagesEmbed? media = null;

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
                                }
                            }
                        }

                        return new RecordWithMediaEmbed(record, media);
                }
            }
        }

        return default;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Embed value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
