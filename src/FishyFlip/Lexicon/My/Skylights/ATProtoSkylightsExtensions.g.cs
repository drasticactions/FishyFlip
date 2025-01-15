// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Lexicon.My.Skylights
{

    /// <summary>
    /// Extension methods for my.skylights.
    /// </summary>
    public static class ATProtoSkylightsExtensions
    {

        /// <summary>
        /// Create a Rel record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateRelAsync(this FishyFlip.ATProtocol atp, FishyFlip.Lexicon.My.Skylights.Rel record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.CreateRecordAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "my.skylights.rel", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Create a Rel record.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateRelAsync(this FishyFlip.ATProtocol atp, ATObject? item, FishyFlip.Lexicon.My.Skylights.Rating? rating = default, FishyFlip.Lexicon.My.Skylights.Note? note = default, List<DateTime>? finishedAt = default, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var record = new FishyFlip.Lexicon.My.Skylights.Rel();
            record.Item = item;
            record.Rating = rating;
            record.Note = note;
            record.FinishedAt = finishedAt;
            return atp.CreateRecordAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "my.skylights.rel", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Delete a Rel record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<DeleteRecordOutput?>> DeleteRelAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.DeleteRecordAsync(repo, "my.skylights.rel", rkey, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Put a Rel record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<PutRecordOutput?>> PutRelAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, FishyFlip.Lexicon.My.Skylights.Rel record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.PutRecordAsync(repo, "my.skylights.rel", rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// List Rel records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListRelAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ListRecordsAsync(repo, "my.skylights.rel", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// List Rel records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListRelAsync(this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ListRecordsAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "my.skylights.rel", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// Get Rel records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetRelAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.GetRecordAsync(repo, "my.skylights.rel", rkey, cid, cancellationToken);
        }

        /// <summary>
        /// Get Rel records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetRelAsync(this FishyFlip.ATProtocol atp, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.GetRecordAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "my.skylights.rel", rkey, cid, cancellationToken);
        }
    }
}

