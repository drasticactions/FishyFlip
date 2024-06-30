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
        ATRecord? atRecord = null;
        if (JsonDocument.TryParseValue(ref reader, out var doc))
        {
            var rawText = doc.RootElement.GetRawText();
            if (doc.RootElement.TryGetProperty("$type", out var type))
            {
                var text = type.GetString()?.Trim() ?? string.Empty;
                switch (text)
                {
                    case Constants.FeedType.Like:
                        atRecord = JsonSerializer.Deserialize<Like>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Like);
                        break;
                    case Constants.FeedType.Post:
                        atRecord = JsonSerializer.Deserialize<Post>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Post);
                        break;
                    case Constants.FeedType.Repost:
                        atRecord = JsonSerializer.Deserialize<Repost>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Repost);
                        break;
                    case Constants.GraphTypes.Follow:
                        atRecord = JsonSerializer.Deserialize<Follow>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).Follow);
                        break;
                    case Constants.ActorTypes.AdultContentPref:
                        atRecord = JsonSerializer.Deserialize<AdultContentPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).AdultContentPref);
                        break;
                    case Constants.ActorTypes.ContentLabelPref:
                        atRecord = JsonSerializer.Deserialize<ContentLabelPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).ContentLabelPref);
                        break;
                    case Constants.ActorTypes.FeedViewPref:
                        atRecord = JsonSerializer.Deserialize<FeedViewPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).FeedViewPref);
                        break;
                    case Constants.ActorTypes.SavedFeedsPref:
                        atRecord = JsonSerializer.Deserialize<SavedFeedsPref>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).SavedFeedsPref);
                        break;
                    case Constants.ConversationTypes.LogCreateMessage:
                        atRecord = JsonSerializer.Deserialize<LogCreateMessage>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogCreateMessage);
                        break;
                    case Constants.ConversationTypes.LogDeleteMessage:
                        atRecord = JsonSerializer.Deserialize<LogDeleteMessage>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogDeleteMessage);
                        break;
                    case Constants.ConversationTypes.LogLeaveConvo:
                        atRecord = JsonSerializer.Deserialize<LogLeaveConvo>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogLeaveConvo);
                        break;
                    case Constants.ConversationTypes.LogBeginConvo:
                        atRecord = JsonSerializer.Deserialize<LogBeginConvo>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).LogBeginConvo);
                        break;
                    case Constants.ConversationTypes.MessageView:
                        atRecord = JsonSerializer.Deserialize<MessageView>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).MessageView);
                        break;
                    case Constants.ConversationTypes.DeletedMessageView:
                        atRecord = JsonSerializer.Deserialize<DeletedMessageView>(doc.RootElement.GetRawText(), ((SourceGenerationContext)options.TypeInfoResolver!).DeletedMessageView);
                        break;
                    default:
#if DEBUG
                        System.Diagnostics.Debugger.Break();
#endif
                        atRecord = new UnknownRecord(text);
                        break;
                }

                atRecord?.SetJson(rawText);
            }
        }

        return atRecord;
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