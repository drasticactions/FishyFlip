// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Lexicon
{

    /// <summary>
    /// Extension methods for com.whtwnd.blog.
    /// </summary>
    public static class ComWhtwndBlogExtensions
    {

        /// <summary>
        /// Create a Entry record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, FishyFlip.Lexicon.Com.Whtwnd.Blog.Entry record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "com.whtwnd.blog.entry", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Create a Entry record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="content"></param>
        /// <param name="createdAt"></param>
        /// <param name="title"></param>
        /// <param name="ogp">
        /// <see cref="FishyFlip.Lexicon.Com.Whtwnd.Blog.Ogp"/> (com.whtwnd.blog.defs#ogp)
        /// </param>
        /// <param name="theme"></param>
        /// <param name="blobs"></param>
        /// <param name="visibility">Tells the visibility of the article to AppView.</param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, string? content, DateTime? createdAt = default, string? title = default, Com.Whtwnd.Blog.Ogp? ogp = default, string? theme = default, List<Com.Whtwnd.Blog.BlobMetadata>? blobs = default, string? visibility = default, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var record = new FishyFlip.Lexicon.Com.Whtwnd.Blog.Entry();
            record.Content = content;
            record.CreatedAt = createdAt ?? DateTime.UtcNow;
            record.Title = title;
            record.Ogp = ogp;
            record.Theme = theme;
            record.Blobs = blobs;
            record.Visibility = visibility;
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "com.whtwnd.blog.entry", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Delete a Entry record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<DeleteRecordOutput?>> DeleteEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.DeleteRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "com.whtwnd.blog.entry", rkey, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Put a Entry record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<PutRecordOutput?>> PutEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, string rkey, FishyFlip.Lexicon.Com.Whtwnd.Blog.Entry record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.PutRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "com.whtwnd.blog.entry", rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// List Entry records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.ListRecordsAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "com.whtwnd.blog.entry", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// List Entry records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, FishyFlip.Models.ATIdentifier repo, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.ListRecordsAsync(repo, "com.whtwnd.blog.entry", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// Get Entry records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.GetRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "com.whtwnd.blog.entry", rkey, cid, cancellationToken);
        }

        /// <summary>
        /// Get Entry records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetEntryAsync(this FishyFlip.Lexicon.Com.Whtwnd.Blog.ComWhtwndBlog atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.GetRecordAsync(repo, "com.whtwnd.blog.entry", rkey, cid, cancellationToken);
        }
    }
}

