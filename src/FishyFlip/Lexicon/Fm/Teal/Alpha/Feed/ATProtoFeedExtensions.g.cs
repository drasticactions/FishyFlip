// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Lexicon.Fm.Teal.Alpha.Feed
{

    /// <summary>
    /// Extension methods for fm.teal.alpha.feed.
    /// </summary>
    public static class ATProtoFeedExtensions
    {

        /// <summary>
        /// Create a Play record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreatePlayAsync(this FishyFlip.ATProtocol atp, FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.Play record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.CreateRecordAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "fm.teal.alpha.feed.play", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Create a Play record.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="collection"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreatePlayAsync(this FishyFlip.ATProtocol atp, string? trackName, List<string>? artistNames, string? trackMbId = default, string? recordingMbId = default, long? duration = default, List<string>? artistMbIds = default, string? releaseName = default, string? releaseMbId = default, string? isrc = default, string? originUrl = default, string? musicServiceBaseDomain = default, string? submissionClientAgent = default, DateTime? playedTime = default, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var record = new FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.Play();
            record.TrackName = trackName;
            record.TrackMbId = trackMbId;
            record.RecordingMbId = recordingMbId;
            record.Duration = duration;
            record.ArtistNames = artistNames;
            record.ArtistMbIds = artistMbIds;
            record.ReleaseName = releaseName;
            record.ReleaseMbId = releaseMbId;
            record.Isrc = isrc;
            record.OriginUrl = originUrl;
            record.MusicServiceBaseDomain = musicServiceBaseDomain;
            record.SubmissionClientAgent = submissionClientAgent;
            record.PlayedTime = playedTime;
            return atp.CreateRecordAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "fm.teal.alpha.feed.play", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Delete a Play record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<DeleteRecordOutput?>> DeletePlayAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.DeleteRecordAsync(repo, "fm.teal.alpha.feed.play", rkey, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Put a Play record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<PutRecordOutput?>> PutPlayAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, FishyFlip.Lexicon.Fm.Teal.Alpha.Feed.Play record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.PutRecordAsync(repo, "fm.teal.alpha.feed.play", rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// List Play records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListPlayAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ListRecordsAsync(repo, "fm.teal.alpha.feed.play", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// List Play records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListPlayAsync(this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ListRecordsAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "fm.teal.alpha.feed.play", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// Get Play records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetPlayAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.GetRecordAsync(repo, "fm.teal.alpha.feed.play", rkey, cid, cancellationToken);
        }

        /// <summary>
        /// Get Play records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetPlayAsync(this FishyFlip.ATProtocol atp, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.GetRecordAsync(atp.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "fm.teal.alpha.feed.play", rkey, cid, cancellationToken);
        }
    }
}

