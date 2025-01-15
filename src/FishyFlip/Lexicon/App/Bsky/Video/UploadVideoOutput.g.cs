// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Video
{
    public partial class UploadVideoOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadVideoOutput"/> class.
        /// </summary>
        /// <param name="jobStatus">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Video.JobStatus"/> (app.bsky.video.defs#jobStatus)
        /// </param>
        public UploadVideoOutput(FishyFlip.Lexicon.App.Bsky.Video.JobStatus jobStatus = default)
        {
            this.JobStatus = jobStatus;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UploadVideoOutput"/> class.
        /// </summary>
        public UploadVideoOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UploadVideoOutput"/> class.
        /// </summary>
        public UploadVideoOutput(CBORObject obj)
        {
            if (obj["jobStatus"] is not null) this.JobStatus = new FishyFlip.Lexicon.App.Bsky.Video.JobStatus(obj["jobStatus"]);
        }

        /// <summary>
        /// Gets or sets the jobStatus.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Video.JobStatus"/> (app.bsky.video.defs#jobStatus)
        /// </summary>
        [JsonPropertyName("jobStatus")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Video.JobStatus JobStatus { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.video.uploadVideo#UploadVideoOutput";

        public const string RecordType = "app.bsky.video.uploadVideo#UploadVideoOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Video.UploadVideoOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Video.UploadVideoOutput>)SourceGenerationContext.Default.AppBskyVideoUploadVideoOutput)!;
        }

        public static UploadVideoOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Video.UploadVideoOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Video.UploadVideoOutput>)SourceGenerationContext.Default.AppBskyVideoUploadVideoOutput)!;
        }
    }
}

