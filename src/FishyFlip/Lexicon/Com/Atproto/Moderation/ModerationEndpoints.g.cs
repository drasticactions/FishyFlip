// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Moderation
{

    /// <summary>
    /// com.atproto.moderation Endpoint Group.
    /// </summary>
    public static class ModerationEndpoints
    {

       public const string CreateReport = "/xrpc/com.atproto.moderation.createReport";


        /// <summary>
        /// Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="reasonType">
        /// <br/> Known Values: <br/>
        /// reasonSpam - Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth. <br/>
        /// reasonViolation - Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth. <br/>
        /// reasonMisleading - Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth. <br/>
        /// reasonSexual - Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth. <br/>
        /// reasonRude - Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth. <br/>
        /// reasonOther - Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth. <br/>
        /// reasonAppeal - Submit a moderation report regarding an atproto account or record. Implemented by moderation services (with PDS proxying), and requires auth. <br/>
        /// </param>
        /// <param name="subject"></param>
        /// <param name="reason"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Moderation.CreateReportOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Moderation.CreateReportOutput?>> CreateReportAsync (this FishyFlip.ATProtocol atp, string reasonType, ATObject subject, string? reason = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = CreateReport.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            var inputItem = new CreateReportInput();
            inputItem.ReasonType = reasonType;
            switch (subject.Type)
            {
                case "com.atproto.admin.defs#repoRef":
                case "com.atproto.repo.strongRef":
                    break;
                default:
                    atp.Options.Logger?.LogWarning($"Unknown subject type for union: " + subject.Type);
                    break;
            }
            inputItem.Subject = subject;
            inputItem.Reason = reason;
            return atp.Post<CreateReportInput, FishyFlip.Lexicon.Com.Atproto.Moderation.CreateReportOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoModerationCreateReportInput!, atp.Options.SourceGenerationContext.ComAtprotoModerationCreateReportOutput!, inputItem, cancellationToken, headers);
        }

    }
}

