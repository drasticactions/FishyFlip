// <copyright file="ATRecordJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

public class ATRecordJsonConverter : JsonConverter<ATRecord>
{
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

    public override void Write(Utf8JsonWriter writer, ATRecord value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
