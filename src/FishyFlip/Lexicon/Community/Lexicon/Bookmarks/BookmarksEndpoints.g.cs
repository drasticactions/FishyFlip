// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Community.Lexicon.Bookmarks
{

    /// <summary>
    /// community.lexicon.bookmarks Endpoint Group.
    /// </summary>
    public static class BookmarksEndpoints
    {

       public const string GetActorBookmarks = "/xrpc/community.lexicon.bookmarks.getActorBookmarks";


        /// <summary>
        /// Get a list of bookmarks by actor. Optionally add a list of tags to include, default will be all bookmarks. Requires auth, actor must be the requesting account.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="tags"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Community.Lexicon.Bookmarks.GetActorBookmarksOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Community.Lexicon.Bookmarks.GetActorBookmarksOutput?>> GetActorBookmarksAsync (this FishyFlip.ATProtocol atp, List<string>? tags = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetActorBookmarks.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (tags != null)
            {
                queryStrings.Add(string.Join("&", tags.Select(n => "tags=" + n)));
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Community.Lexicon.Bookmarks.GetActorBookmarksOutput>(endpointUrl, atp.Options.SourceGenerationContext.CommunityLexiconBookmarksGetActorBookmarksOutput!, cancellationToken, headers);
        }

    }
}

