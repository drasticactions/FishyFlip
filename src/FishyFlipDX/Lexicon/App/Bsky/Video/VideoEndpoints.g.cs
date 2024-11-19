// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Video
{

    /// <summary>
    /// app.bsky.video Endpoint Group.
    /// </summary>
    public static class VideoEndpoints
    {

       public const string GetJobStatus = "/xrpc/app.bsky.video.getJobStatus";

       public const string GetUploadLimits = "/xrpc/app.bsky.video.getUploadLimits";

       public const string UploadVideo = "/xrpc/app.bsky.video.uploadVideo";


        /// <summary>
        /// Get status details for a video processing job.
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Video.GetJobStatusOutput?>> GetJobStatusAsync (this FishyFlip.ATProtocol atp, string jobId, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetJobStatus.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("jobId=" + jobId);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Video.GetJobStatusOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyVideoGetJobStatusOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get video upload limits for the authenticated user.
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Video.GetUploadLimitsOutput?>> GetUploadLimitsAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetUploadLimits.ToString();
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Video.GetUploadLimitsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyVideoGetUploadLimitsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Upload a video to be processed then stored on the PDS.
        /// </summary>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Video.UploadVideoOutput?>> UploadVideoAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UploadVideo.ToString();
            return atp.Client.Post<FishyFlip.Lexicon.App.Bsky.Video.UploadVideoOutput?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyVideoUploadVideoOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }

    }
}

