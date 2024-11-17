// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Video
{
    public partial class JobStatus : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="JobStatus"/> class.
        /// </summary>
        public JobStatus()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="JobStatus"/> class.
        /// </summary>
        public JobStatus(CBORObject obj)
        {
            if (obj["jobId"] is not null) this.JobId = obj["jobId"].AsString();
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            // enum
            if (obj["progress"] is not null) this.Progress = obj["progress"].AsInt64Value();
            if (obj["blob"] is not null) this.Blob = new FishyFlip.Models.Blob(obj["blob"]);
            if (obj["error"] is not null) this.Error = obj["error"].AsString();
            if (obj["message"] is not null) this.Message = obj["message"].AsString();
        }

        [JsonPropertyName("jobId")]
        [JsonRequired]
        public string? JobId { get; set; }

        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? Did { get; set; }

        /// <summary>
        /// The state of the video processing job. All values not listed as a known value indicate that the job is in process.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonRequired]
        public string? State { get; set; }

        /// <summary>
        /// Progress within the current processing state.
        /// </summary>
        [JsonPropertyName("progress")]
        public long? Progress { get; set; }

        [JsonPropertyName("blob")]
        public Blob? Blob { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.video.defs#jobStatus";

        public const string RecordType = "app.bsky.video.defs#jobStatus";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Video.JobStatus>(this, (JsonTypeInfo<App.Bsky.Video.JobStatus>)SourceGenerationContext.Default.AppBskyVideoJobStatus)!;
        }

        public static JobStatus FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Video.JobStatus>(json, (JsonTypeInfo<App.Bsky.Video.JobStatus>)SourceGenerationContext.Default.AppBskyVideoJobStatus)!;
        }
    }
}

