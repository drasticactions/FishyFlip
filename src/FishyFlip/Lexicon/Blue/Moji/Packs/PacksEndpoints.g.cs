// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Packs
{

    /// <summary>
    /// blue.moji.packs Endpoint Group.
    /// </summary>
    public static class PacksEndpoints
    {

       public const string GroupNamespace = "blue.moji.packs";

       public const string GetActorPacks = "/xrpc/blue.moji.packs.getActorPacks";

       public const string GetPack = "/xrpc/blue.moji.packs.getPack";

       public const string GetPacks = "/xrpc/blue.moji.packs.getPacks";


        /// <summary>
        /// Get a list of Bluemoji packs created by the actor.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Blue.Moji.Packs.GetActorPacksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Blue.Moji.Packs.GetActorPacksOutput?>> GetActorPacksAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetActorPacks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            if (atp.TryFetchProxy(GroupNamespace, out var proxyUrl))
            {
                headers.Add(Constants.AtProtoProxy, proxyUrl);
            }
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Blue.Moji.Packs.GetActorPacksOutput>(endpointUrl, atp.Options.SourceGenerationContext.BlueMojiPacksGetActorPacksOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Gets a 'view' (with additional context) of a specified pack.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="pack">Reference (AT-URI) of the pack record to hydrate.</param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput?>> GetPackAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri pack, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetPack.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("pack=" + pack);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            if (atp.TryFetchProxy(GroupNamespace, out var proxyUrl))
            {
                headers.Add(Constants.AtProtoProxy, proxyUrl);
            }
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Blue.Moji.Packs.GetPackOutput>(endpointUrl, atp.Options.SourceGenerationContext.BlueMojiPacksGetPackOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Get views for a list of Bluemoji packs.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uris"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Blue.Moji.Packs.GetPacksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Blue.Moji.Packs.GetPacksOutput?>> GetPacksAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATUri> uris, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetPacks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", uris.Select(n => "uris=" + n)));

            var headers = new Dictionary<string, string>();
            if (atp.TryFetchProxy(GroupNamespace, out var proxyUrl))
            {
                headers.Add(Constants.AtProtoProxy, proxyUrl);
            }
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Blue.Moji.Packs.GetPacksOutput>(endpointUrl, atp.Options.SourceGenerationContext.BlueMojiPacksGetPacksOutput!, cancellationToken, headers);
        }

    }
}

