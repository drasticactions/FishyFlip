// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Chat.Bsky.Convo
{
    /// <summary>
    /// GetMessagesOutput Collection.
    /// </summary>
    public class GetMessagesOutputCollection : ATObjectCollectionBase<ATObject>, IAsyncEnumerable<ATObject>
    {

        public GetMessagesOutputCollection(FishyFlip.ATProtocol atp, string convoId, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.ConvoId = convoId;
            this.Limit = limit;
            this.Cursor = cursor;
            this.CancellationToken = cancellationToken;
        }

        public string ConvoId { get; }

        /// <inheritdoc/>
        public override async Task<(IList<ATObject> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.GetMessagesAsync(convoId: this.ConvoId, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Messages == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Messages, result.Cursor ?? string.Empty);
        }

        public static GetMessagesOutputCollection Create(FishyFlip.ATProtocol atp, string convoId, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, convoId: convoId, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}

