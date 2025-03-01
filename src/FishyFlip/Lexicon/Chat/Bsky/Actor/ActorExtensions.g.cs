// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace FishyFlip.Lexicon
{

    /// <summary>
    /// Extension methods for chat.bsky.actor.
    /// </summary>
    public static class ChatBskyActorExtensions
    {

        /// <summary>
        /// Create a Declaration record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="record"></param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, FishyFlip.Lexicon.Chat.Bsky.Actor.Declaration record, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "chat.bsky.actor.declaration", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Create a Declaration record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="allowIncoming">
        /// <br/> Known Values: <br/>
        /// all <br/>
        /// none <br/>
        /// following <br/>
        /// </param>
        /// <param name="rkey"></param>
        /// <param name="validate"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<CreateRecordOutput?>> CreateDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, string? allowIncoming, string? rkey = default, bool? validate = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            var record = new FishyFlip.Lexicon.Chat.Bsky.Actor.Declaration();
            record.AllowIncoming = allowIncoming;
            return atp.ATProtocol.CreateRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "chat.bsky.actor.declaration", record, rkey, validate, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Delete a Declaration record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<DeleteRecordOutput?>> DeleteDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, string rkey, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.DeleteRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "chat.bsky.actor.declaration", rkey, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// Put a Declaration record.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="record"></param>
        /// <param name="validate"></param>
        /// <param name="swapRecord"></param>
        /// <param name="swapCommit"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<PutRecordOutput?>> PutDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, string rkey, FishyFlip.Lexicon.Chat.Bsky.Actor.Declaration record, bool? validate = default, string? swapRecord = default, string? swapCommit = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.PutRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "chat.bsky.actor.declaration", rkey, record, validate, swapRecord, swapCommit, cancellationToken);
        }

        /// <summary>
        /// List Declaration records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.ListRecordsAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "chat.bsky.actor.declaration", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// List Declaration records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="reverse"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<ListRecordsOutput?>> ListDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, FishyFlip.Models.ATIdentifier repo, int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.ListRecordsAsync(repo, "chat.bsky.actor.declaration", limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// Get Declaration records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.GetRecordAsync(atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException("Session did is required."), "chat.bsky.actor.declaration", rkey, cid, cancellationToken);
        }

        /// <summary>
        /// Get Declaration records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="repo"></param>
        /// <param name="rkey"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        public static Task<Result<GetRecordOutput?>> GetDeclarationAsync(this FishyFlip.Lexicon.Chat.Bsky.Actor.ChatBskyActor atp, FishyFlip.Models.ATIdentifier repo, string rkey, string? cid = default, CancellationToken cancellationToken = default)
        {
            return atp.ATProtocol.GetRecordAsync(repo, "chat.bsky.actor.declaration", rkey, cid, cancellationToken);
        }
    }
}

