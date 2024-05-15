// <copyright file="ATRecordJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// Converts JSON to <see cref="ATRecord"/> and vice versa.
/// </summary>
public class ATRecordJsonConverter : JsonConverter<ATRecord>
{
    /// <inheritdoc/>
    public override ATRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (JsonDocument.TryParseValue(ref reader, out var doc))
        {
            if (doc.RootElement.TryGetProperty("$type", out var type))
            {
                var text = type.GetString()?.Trim() ?? string.Empty;
                var rawText = doc.RootElement.GetRawText();
                switch (text)
                {
                    case Constants.FeedType.Like:
                        return JsonSerializer.Deserialize<Like>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Like);
                    case Constants.FeedType.Post:
                        return JsonSerializer.Deserialize<Post>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Post);
                    case Constants.FeedType.Repost:
                        return JsonSerializer.Deserialize<Repost>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Repost);
                    case Constants.GraphTypes.Follow:
                        return JsonSerializer.Deserialize<Follow>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Follow);
                    case Constants.ActorTypes.AdultContentPref:
                        return JsonSerializer.Deserialize<AdultContentPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).AdultContentPref);
                    case Constants.ActorTypes.ContentLabelPref:
                        return JsonSerializer.Deserialize<ContentLabelPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ContentLabelPref);
                    case Constants.ActorTypes.FeedViewPref:
                        return JsonSerializer.Deserialize<FeedViewPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).FeedViewPref);
                    case Constants.ActorTypes.SavedFeedsPref:
                        return JsonSerializer.Deserialize<SavedFeedsPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).SavedFeedsPref);
                    case Constants.WhiteWindTypes.Entry:
                        return JsonSerializer.Deserialize<Models.WhiteWind.Entry>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Entry);
                    default:
#if DEBUG
                        System.Diagnostics.Debugger.Break();
#endif
                        break;
                }
            }
        }

        return null;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATRecord value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
