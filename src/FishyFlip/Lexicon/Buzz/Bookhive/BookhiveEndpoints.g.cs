// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Buzz.Bookhive
{

    /// <summary>
    /// buzz.bookhive Endpoint Group.
    /// </summary>
    public static class BookhiveEndpoints
    {

       public const string SearchBooks = "/xrpc/buzz.bookhive.searchBooks";


        /// <summary>
        /// Find books matching the search criteria. Requires authentication.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput?>> SearchBooksAsync (this FishyFlip.ATProtocol atp, string q, int? limit = 25, int? offset = 0, string? id = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchBooks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("q=" + q);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (offset != null)
            {
                queryStrings.Add("offset=" + offset);
            }

            if (id != null)
            {
                queryStrings.Add("id=" + id);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput>(endpointUrl, atp.Options.SourceGenerationContext.BuzzBookhiveSearchBooksOutput!, cancellationToken, headers);
        }

    }
}
