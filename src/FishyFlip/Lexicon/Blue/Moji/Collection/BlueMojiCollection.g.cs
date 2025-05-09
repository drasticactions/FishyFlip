// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Blue.Moji.Collection
{

    /// <summary>
    /// blue.moji.collection Endpoint Class.
    /// </summary>
    public sealed class BlueMojiCollection
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueMojiCollection"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal BlueMojiCollection(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Get a single emoji from a repository. Requires auth.
        /// </summary>
        /// <param name="repo">The handle or DID of the repo.</param>
        /// <param name="name">The Bluemoji alias/rkey.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Moji.Collection.GetItemOutput?>> GetItemAsync (FishyFlip.Models.ATIdentifier repo, string name, CancellationToken cancellationToken = default)
        {
            return atp.GetItemAsync(repo, name, cancellationToken);
        }


        /// <summary>
        /// List a range of Bluemoji in a repository, matching a specific collection. Requires auth.
        /// </summary>
        /// <param name="limit">The number of records to return.</param>
        /// <param name="cursor"></param>
        /// <param name="reverse">Flag to reverse the order of the returned records.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Moji.Collection.ListCollectionOutput?>> ListCollectionAsync (int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return atp.ListCollectionAsync(limit, cursor, reverse, cancellationToken);
        }

        /// <summary>
        /// List a range of Bluemoji in a repository, matching a specific collection. Requires auth.
        /// </summary>
        /// <param name="limit">The number of records to return.</param>
        /// <param name="cursor"></param>
        /// <param name="reverse">Flag to reverse the order of the returned records.</param>
        /// <param name="cancellationToken"></param>
        public ListCollectionOutputCollection ListCollectionCollectionAsync (int? limit = 50, string? cursor = default, bool? reverse = default, CancellationToken cancellationToken = default)
        {
            return new ListCollectionOutputCollection(atp, limit, cursor, reverse, cancellationToken);
        }


        /// <summary>
        /// Write a Bluemoji record, creating or updating it as needed. Requires auth, implemented by AppView.
        /// </summary>
        /// <param name="repo">The handle or DID of the repo (aka, current account).</param>
        /// <param name="item"></param>
        /// <param name="validate">Can be set to 'false' to skip Lexicon schema validation of record data.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Moji.Collection.PutItemOutput?>> PutItemAsync (FishyFlip.Models.ATIdentifier repo, FishyFlip.Lexicon.Blue.Moji.Collection.ItemView item, bool? validate = default, CancellationToken cancellationToken = default)
        {
            return atp.PutItemAsync(repo, item, validate, cancellationToken);
        }


        /// <summary>
        /// Copy a single emoji from another repo. Requires auth.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.EmojiNotFoundError"/> Indicates the named Bluemoji was not found in the source repo. <br/>
        /// <see cref="FishyFlip.Lexicon.DestinationExistsError"/> Indicates another Bluemoji with the same name already exists in the source repo. Set renameTo to rename. <br/>
        /// </summary>
        /// <param name="source">The handle or DID of the repo to copy from.</param>
        /// <param name="name">The source Bluemoji name/rkey.</param>
        /// <param name="renameTo">The alias to save the Bluemoji to in the current logged-in user's repo.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Blue.Moji.Collection.SaveToCollectionOutput?>> SaveToCollectionAsync (FishyFlip.Models.ATIdentifier source, string name, string? renameTo = default, CancellationToken cancellationToken = default)
        {
            return atp.SaveToCollectionAsync(source, name, renameTo, cancellationToken);
        }

    }
}

