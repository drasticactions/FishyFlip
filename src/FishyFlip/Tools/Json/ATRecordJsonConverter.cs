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
                    case Constants.ConversationTypes.LogCreateMessage:
                        return JsonSerializer.Deserialize<LogCreateMessage>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogCreateMessage);
                    case Constants.ConversationTypes.LogDeleteMessage:
                        return JsonSerializer.Deserialize<LogDeleteMessage>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogDeleteMessage);
                    case Constants.ConversationTypes.LogLeaveConvo:
                        return JsonSerializer.Deserialize<LogLeaveConvo>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogLeaveConvo);
                    case Constants.ConversationTypes.LogBeginConvo:
                        return JsonSerializer.Deserialize<LogBeginConvo>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogBeginConvo);
                    case Constants.ConversationTypes.MessageView:
                        return JsonSerializer.Deserialize<MessageView>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).MessageView);
                    case Constants.ConversationTypes.DeletedMessageView:
                        return JsonSerializer.Deserialize<DeletedMessageView>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).DeletedMessageView);
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
        switch (value.Type)
        {
            case Constants.FeedType.Like:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((Like)value, ((SourceGenerationContext)options.TypeInfoResolver!).Like));
                break;
            case Constants.FeedType.Post:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((Post)value, ((SourceGenerationContext)options.TypeInfoResolver!).Post));
                break;
            case Constants.FeedType.Repost:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((Repost)value, ((SourceGenerationContext)options.TypeInfoResolver!).Repost));
                break;
            case Constants.GraphTypes.Follow:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((Follow)value, ((SourceGenerationContext)options.TypeInfoResolver!).Follow));
                break;
            case Constants.ActorTypes.AdultContentPref:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((AdultContentPref)value, ((SourceGenerationContext)options.TypeInfoResolver!).AdultContentPref));
                break;
            case Constants.ActorTypes.ContentLabelPref:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((ContentLabelPref)value, ((SourceGenerationContext)options.TypeInfoResolver!).ContentLabelPref));
                break;
            case Constants.ActorTypes.FeedViewPref:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((FeedViewPref)value, ((SourceGenerationContext)options.TypeInfoResolver!).FeedViewPref));
                break;
            case Constants.ActorTypes.SavedFeedsPref:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((SavedFeedsPref)value, ((SourceGenerationContext)options.TypeInfoResolver!).SavedFeedsPref));
                break;
            case Constants.ConversationTypes.MessageView:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((MessageView)value, ((SourceGenerationContext)options.TypeInfoResolver!).MessageView));
                break;
            case Constants.ConversationTypes.DeletedMessageView:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((DeletedMessageView)value, ((SourceGenerationContext)options.TypeInfoResolver!).DeletedMessageView));
                break;
            case Constants.ConversationTypes.LogCreateMessage:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((LogCreateMessage)value, ((SourceGenerationContext)options.TypeInfoResolver!).LogCreateMessage));
                break;
            case Constants.ConversationTypes.LogDeleteMessage:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((LogDeleteMessage)value, ((SourceGenerationContext)options.TypeInfoResolver!).LogDeleteMessage));
                break;
            case Constants.ConversationTypes.LogLeaveConvo:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((LogLeaveConvo)value, ((SourceGenerationContext)options.TypeInfoResolver!).LogLeaveConvo));
                break;
            case Constants.ConversationTypes.LogBeginConvo:
                writer.WriteRawValue(JsonSerializer.SerializeToUtf8Bytes((LogBeginConvo)value, ((SourceGenerationContext)options.TypeInfoResolver!).LogBeginConvo));
                break;
            default:
#if DEBUG
                System.Diagnostics.Debugger.Break();
#endif
                break;
        }
    }
}
