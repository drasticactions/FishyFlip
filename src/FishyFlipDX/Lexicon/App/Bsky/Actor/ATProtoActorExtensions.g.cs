// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Lexicon.App.Bsky.Actor
{

    /// <summary>
    /// Extension methods for app.bsky.actor.
    /// </summary>
    public static class ATProtoActorExtensions
    {

        /// <summary>
        /// Create a Profile record.
        /// </summary>
        public static Task<Result<CreateRecordOutput?>> CreateProfileRecordAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, App.Bsky.Actor.Profile record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.CreateRecordAsync(repo, "app.bsky.actor.profile", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Delete a Profile record.
        /// </summary>
        public static Task<Result<DeleteRecordOutput?>> DeleteProfileRecordAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.DeleteRecordAsync(repo, "app.bsky.actor.profile", rkey, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Put a Profile record.
        /// </summary>
        public static Task<Result<PutRecordOutput?>> PutProfileRecordAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, string rkey, App.Bsky.Actor.Profile record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.PutRecordAsync(repo, "app.bsky.actor.profile", rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// List Profile records.
        /// </summary>
        public static Task<Result<ListRecordsOutput?>> ListProfileRecordsAsync(this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier repo, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ListRecordsAsync(repo, "app.bsky.actor.profile", limit, cursor, reverse, cancellationToken);
        }
    }
}

