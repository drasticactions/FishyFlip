// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Lexicon
{

    /// <summary>
    /// Extension methods for app.bsky.actor.
    /// </summary>
    public static class BlueskyActorExtensions
    {

        /// <summary>
        /// Create a Profile record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateProfileAsync(this FishyFlip.Lexicon.App.Bsky.Actor.BlueskyActor atp, FishyFlip.Lexicon.App.Bsky.Actor.Profile record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "app.bsky.actor.profile", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Create a Profile record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="displayName"></param>
        /// <param name="description">Free-form profile description text.</param>
        /// <param name="avatar">Small image to be displayed next to posts from account. AKA, 'profile picture'</param>
        /// <param name="banner">Larger horizontal image to display behind profile view.</param>
        /// <param name="labels">Self-label values, specific to the Bluesky application, on the overall account.
        /// Union Types:
        /// com.atproto.label.defs#selfLabels
        /// </param>
        /// <param name="joinedViaStarterPack">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="pinnedPost">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="createdAt"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateProfileAsync(this FishyFlip.Lexicon.App.Bsky.Actor.BlueskyActor atp, string? displayName = default, string? description = default, Blob? avatar = default, Blob? banner = default, Com.Atproto.Label.SelfLabels? labels = default, Com.Atproto.Repo.StrongRef? joinedViaStarterPack = default, Com.Atproto.Repo.StrongRef? pinnedPost = default, DateTime? createdAt = default, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var record = new FishyFlip.Lexicon.App.Bsky.Actor.Profile();
            record.DisplayName = displayName;
            record.Description = description;
            record.Avatar = avatar;
            record.Banner = banner;
            record.Labels = labels;
            record.JoinedViaStarterPack = joinedViaStarterPack;
            record.PinnedPost = pinnedPost;
            record.CreatedAt = createdAt ?? DateTime.UtcNow;
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "app.bsky.actor.profile", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Delete a Profile record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<DeleteRecordOutput?>> DeleteProfileAsync(this FishyFlip.Lexicon.App.Bsky.Actor.BlueskyActor atp, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.DeleteRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "app.bsky.actor.profile", rkey, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Put a Profile record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<PutRecordOutput?>> PutProfileAsync(this FishyFlip.Lexicon.App.Bsky.Actor.BlueskyActor atp, string rkey, FishyFlip.Lexicon.App.Bsky.Actor.Profile record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.PutRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "app.bsky.actor.profile", rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// List Profile records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListProfilesAsync(this FishyFlip.Lexicon.App.Bsky.Actor.BlueskyActor atp, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.ListRecordsAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "app.bsky.actor.profile", limit, cursor, reverse, cancellationToken);
        }
    }
}
