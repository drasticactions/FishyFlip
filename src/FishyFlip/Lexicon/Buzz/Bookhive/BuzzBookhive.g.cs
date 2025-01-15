// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Buzz.Bookhive
{

    /// <summary>
    /// buzz.bookhive Endpoint Class.
    /// </summary>
    public sealed class BuzzBookhive
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuzzBookhive"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal BuzzBookhive(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Find books matching the search criteria. Requires authentication.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Buzz.Bookhive.SearchBooksOutput?>> SearchBooksAsync (string q, int? limit = 25, int? offset = 0, string? id = default, CancellationToken cancellationToken = default)
        {
            return atp.SearchBooksAsync(q, limit, offset, id, cancellationToken);
        }

    }
}

