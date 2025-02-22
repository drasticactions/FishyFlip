// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Notification
{
    public partial class PutPreferencesInput : ATObject, ICBOREncodable<PutPreferencesInput>, IJsonEncodable<PutPreferencesInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PutPreferencesInput"/> class.
        /// </summary>
        /// <param name="priority"></param>
        public PutPreferencesInput(bool priority = default)
        {
            this.Priority = priority;
            this.Type = "app.bsky.notification.putPreferences#PutPreferencesInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutPreferencesInput"/> class.
        /// </summary>
        public PutPreferencesInput()
        {
            this.Type = "app.bsky.notification.putPreferences#PutPreferencesInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PutPreferencesInput"/> class.
        /// </summary>
        public PutPreferencesInput(CBORObject obj)
        {
            if (obj["priority"] is not null) this.Priority = obj["priority"].AsBoolean();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [JsonPropertyName("priority")]
        [JsonRequired]
        public bool Priority { get; set; }

        public const string RecordType = "app.bsky.notification.putPreferences#PutPreferencesInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.PutPreferencesInput>)SourceGenerationContext.Default.AppBskyNotificationPutPreferencesInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.PutPreferencesInput>)SourceGenerationContext.Default.AppBskyNotificationPutPreferencesInput);
        }

        public static new PutPreferencesInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Notification.PutPreferencesInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Notification.PutPreferencesInput>)SourceGenerationContext.Default.AppBskyNotificationPutPreferencesInput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new PutPreferencesInput FromCBORObject(CBORObject obj)
        {
            return new PutPreferencesInput(obj);
        }

    }
}

