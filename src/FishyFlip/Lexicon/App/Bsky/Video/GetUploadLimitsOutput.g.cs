// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Video
{
    public partial class GetUploadLimitsOutput : ATObject, ICBOREncodable<GetUploadLimitsOutput>, IJsonEncodable<GetUploadLimitsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUploadLimitsOutput"/> class.
        /// </summary>
        /// <param name="canUpload"></param>
        /// <param name="remainingDailyVideos"></param>
        /// <param name="remainingDailyBytes"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public GetUploadLimitsOutput(bool canUpload = default, long? remainingDailyVideos = default, long? remainingDailyBytes = default, string? message = default, string? error = default)
        {
            this.CanUpload = canUpload;
            this.RemainingDailyVideos = remainingDailyVideos;
            this.RemainingDailyBytes = remainingDailyBytes;
            this.Message = message;
            this.Error = error;
            this.Type = "app.bsky.video.getUploadLimits#GetUploadLimitsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetUploadLimitsOutput"/> class.
        /// </summary>
        public GetUploadLimitsOutput()
        {
            this.Type = "app.bsky.video.getUploadLimits#GetUploadLimitsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetUploadLimitsOutput"/> class.
        /// </summary>
        public GetUploadLimitsOutput(CBORObject obj)
        {
            if (obj["canUpload"] is not null) this.CanUpload = obj["canUpload"].AsBoolean();
            if (obj["remainingDailyVideos"] is not null) this.RemainingDailyVideos = obj["remainingDailyVideos"].AsInt64Value();
            if (obj["remainingDailyBytes"] is not null) this.RemainingDailyBytes = obj["remainingDailyBytes"].AsInt64Value();
            if (obj["message"] is not null) this.Message = obj["message"].AsString();
            if (obj["error"] is not null) this.Error = obj["error"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the canUpload.
        /// </summary>
        [JsonPropertyName("canUpload")]
        [JsonRequired]
        public bool CanUpload { get; set; }

        /// <summary>
        /// Gets or sets the remainingDailyVideos.
        /// </summary>
        [JsonPropertyName("remainingDailyVideos")]
        public long? RemainingDailyVideos { get; set; }

        /// <summary>
        /// Gets or sets the remainingDailyBytes.
        /// </summary>
        [JsonPropertyName("remainingDailyBytes")]
        public long? RemainingDailyBytes { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        public const string RecordType = "app.bsky.video.getUploadLimits#GetUploadLimitsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Video.GetUploadLimitsOutput>)SourceGenerationContext.Default.AppBskyVideoGetUploadLimitsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Video.GetUploadLimitsOutput>)SourceGenerationContext.Default.AppBskyVideoGetUploadLimitsOutput);
        }

        public static new GetUploadLimitsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Video.GetUploadLimitsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Video.GetUploadLimitsOutput>)SourceGenerationContext.Default.AppBskyVideoGetUploadLimitsOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetUploadLimitsOutput FromCBORObject(CBORObject obj)
        {
            return new GetUploadLimitsOutput(obj);
        }

    }
}

