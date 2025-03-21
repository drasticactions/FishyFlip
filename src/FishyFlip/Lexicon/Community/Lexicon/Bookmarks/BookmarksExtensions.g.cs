// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Lexicon
{

    /// <summary>
    /// Extension methods for community.lexicon.bookmarks.
    /// </summary>
    public static class CommunityLexiconBookmarksExtensions
    {

        /// <summary>
        /// Create a Bookmark record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, FishyFlip.Lexicon.Community.Lexicon.Bookmarks.Bookmark record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "community.lexicon.bookmarks.bookmark", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Create a Bookmark record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="subject"></param>
        /// <param name="createdAt"></param>
        /// <param name="tags">Tags for content the bookmark may be related to, for example 'news' or 'funny videos'</param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, string? subject, DateTime? createdAt = default, List<string>? tags = default, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var record = new FishyFlip.Lexicon.Community.Lexicon.Bookmarks.Bookmark();
            record.Subject = subject;
            record.CreatedAt = createdAt ?? DateTime.UtcNow;
            record.Tags = tags;
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "community.lexicon.bookmarks.bookmark", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Delete a Bookmark record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<DeleteRecordOutput?>> DeleteBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.DeleteRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "community.lexicon.bookmarks.bookmark", rkey, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Put a Bookmark record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<PutRecordOutput?>> PutBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, string rkey, FishyFlip.Lexicon.Community.Lexicon.Bookmarks.Bookmark record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.PutRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "community.lexicon.bookmarks.bookmark", rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// List Bookmark records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.ListRecordsAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "community.lexicon.bookmarks.bookmark", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// List Bookmark records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, FishyFlip.Models.ATIdentifier repo, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.ListRecordsAsync(repo, "community.lexicon.bookmarks.bookmark", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// Get Bookmark records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.GetRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "community.lexicon.bookmarks.bookmark", rkey, cid, cancellationToken);
        }

        /// <summary>
        /// Get Bookmark records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetBookmarkAsync(this FishyFlip.Lexicon.Community.Lexicon.Bookmarks.CommunityLexiconBookmarks atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.GetRecordAsync(repo, "community.lexicon.bookmarks.bookmark", rkey, cid, cancellationToken);
        }
    }
}

