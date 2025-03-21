// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Signature
{

    /// <summary>
    /// tools.ozone.signature Endpoint Class.
    /// </summary>
    public sealed class ToolsOzoneSignature
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolsOzoneSignature"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal ToolsOzoneSignature(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Find all correlated threat signatures between 2 or more accounts.
        /// </summary>
        /// <param name="dids"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Tools.Ozone.Signature.FindCorrelationOutput?>> FindCorrelationAsync (List<FishyFlip.Models.ATDid> dids, CancellationToken cancellationToken = default)
        {
            return atp.FindCorrelationAsync(dids, cancellationToken);
        }


        /// <summary>
        /// Get accounts that share some matching threat signatures with the root account.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Tools.Ozone.Signature.FindRelatedAccountsOutput?>> FindRelatedAccountsAsync (FishyFlip.Models.ATDid did, string? cursor = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return atp.FindRelatedAccountsAsync(did, cursor, limit, cancellationToken);
        }

        /// <summary>
        /// Get accounts that share some matching threat signatures with the root account.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public FindRelatedAccountsOutputCollection FindRelatedAccountsCollectionAsync (FishyFlip.Models.ATDid did, string? cursor = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return new FindRelatedAccountsOutputCollection(atp, did, cursor, limit, cancellationToken);
        }


        /// <summary>
        /// Search for accounts that match one or more threat signature values.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Tools.Ozone.Signature.SearchAccountsOutput?>> SearchAccountsAsync (List<string> values, string? cursor = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return atp.SearchAccountsAsync(values, cursor, limit, cancellationToken);
        }

        /// <summary>
        /// Search for accounts that match one or more threat signature values.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public SearchAccountsOutputCollection SearchAccountsCollectionAsync (List<string> values, string? cursor = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return new SearchAccountsOutputCollection(atp, values, cursor, limit, cancellationToken);
        }

    }
}

