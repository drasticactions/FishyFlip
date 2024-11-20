// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Notification
{
    public partial class GetUnreadCountOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUnreadCountOutput"/> class.
        /// </summary>
        public GetUnreadCountOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetUnreadCountOutput"/> class.
        /// </summary>
        public GetUnreadCountOutput(CBORObject obj)
        {
            if (obj["count"] is not null) this.Count = obj["count"].AsInt64Value();
        }

        [JsonPropertyName("count")]
        [JsonRequired]
        public long? Count { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.notification.getUnreadCount#GetUnreadCountOutput";

        public const string RecordType = "app.bsky.notification.getUnreadCount#GetUnreadCountOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Notification.GetUnreadCountOutput>(this, (JsonTypeInfo<App.Bsky.Notification.GetUnreadCountOutput>)SourceGenerationContext.Default.AppBskyNotificationGetUnreadCountOutput)!;
        }

        public static GetUnreadCountOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Notification.GetUnreadCountOutput>(json, (JsonTypeInfo<App.Bsky.Notification.GetUnreadCountOutput>)SourceGenerationContext.Default.AppBskyNotificationGetUnreadCountOutput)!;
        }
    }
}
