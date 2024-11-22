// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Labeler
{

    /// <summary>
    /// app.bsky.labeler Endpoint Group.
    /// </summary>
    public static class LabelerEndpoints
    {

       public const string GetServices = "/xrpc/app.bsky.labeler.getServices";


        /// <summary>
        /// Get information about a list of labeler services.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="dids"></param>
        /// <param name="detailed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Labeler.GetServicesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Labeler.GetServicesOutput?>> GetServicesAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATDid> dids, bool? detailed = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetServices.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", dids.Select(n => "dids=" + n)));

            if (detailed != null)
            {
                queryStrings.Add("detailed=" + detailed);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Labeler.GetServicesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyLabelerGetServicesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }

    }
}

