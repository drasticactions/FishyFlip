// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Setting
{
    /// <summary>
    /// ListOptionsOutput Collection.
    /// </summary>
    public class ListOptionsOutputCollection : ATObjectCollectionBase<FishyFlip.Lexicon.Tools.Ozone.Setting.Option>, IAsyncEnumerable<FishyFlip.Lexicon.Tools.Ozone.Setting.Option>
    {

        public ListOptionsOutputCollection(FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, string? scope = default, string? prefix = default, List<string>? keys = default, CancellationToken cancellationToken = default)
             : base(atp)
        {
            this.Limit = limit;
            this.Cursor = cursor;
            this.Scope = scope;
            this.Prefix = prefix;
            this.Keys = keys;
            this.CancellationToken = cancellationToken;
        }

        public string? Scope { get; }

        public string? Prefix { get; }

        public List<string>? Keys { get; }

        /// <inheritdoc/>
        public override async Task<(IList<FishyFlip.Lexicon.Tools.Ozone.Setting.Option> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)
        {
            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;
            var (result, error) = await this.ATProtocol.ListOptionsAsync(scope: this.Scope, prefix: this.Prefix, keys: this.Keys, limit: limit, cursor: this.Cursor, cancellationToken: token.Value!);

            this.HandleATError(error);

            if (result == null || result.Options == null)
            {
                throw new InvalidOperationException("The result or its properties cannot be null.");
            }

            return (result.Options, result.Cursor ?? string.Empty);
        }

        public static ListOptionsOutputCollection Create(FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, string? scope = default, string? prefix = default, List<string>? keys = default, CancellationToken cancellationToken = default)
        {
            return new(atp: atp, scope: scope, prefix: prefix, keys: keys, limit: limit, cursor: cursor, cancellationToken: cancellationToken);
        }
    }
}

