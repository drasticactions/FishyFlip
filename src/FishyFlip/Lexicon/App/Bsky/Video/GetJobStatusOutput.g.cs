// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Video
{
    public partial class GetJobStatusOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetJobStatusOutput"/> class.
        /// </summary>
        /// <param name="jobStatus">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Video.JobStatus"/> (app.bsky.video.defs#jobStatus)
        /// </param>
        public GetJobStatusOutput(App.Bsky.Video.JobStatus? jobStatus = default)
        {
            this.JobStatus = jobStatus;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetJobStatusOutput"/> class.
        /// </summary>
        public GetJobStatusOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetJobStatusOutput"/> class.
        /// </summary>
        public GetJobStatusOutput(CBORObject obj)
        {
            if (obj["jobStatus"] is not null) this.JobStatus = new App.Bsky.Video.JobStatus(obj["jobStatus"]);
        }

        /// <summary>
        /// Gets or sets the jobStatus.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Video.JobStatus"/> (app.bsky.video.defs#jobStatus)
        /// </summary>
        [JsonPropertyName("jobStatus")]
        [JsonRequired]
        public App.Bsky.Video.JobStatus? JobStatus { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.video.getJobStatus#GetJobStatusOutput";

        public const string RecordType = "app.bsky.video.getJobStatus#GetJobStatusOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Video.GetJobStatusOutput>(this, (JsonTypeInfo<App.Bsky.Video.GetJobStatusOutput>)SourceGenerationContext.Default.AppBskyVideoGetJobStatusOutput)!;
        }

        public static GetJobStatusOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Video.GetJobStatusOutput>(json, (JsonTypeInfo<App.Bsky.Video.GetJobStatusOutput>)SourceGenerationContext.Default.AppBskyVideoGetJobStatusOutput)!;
        }
    }
}

