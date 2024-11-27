// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{

    /// <summary>
    /// app.bsky.actor Endpoint Class.
    /// </summary>
    public sealed class BlueskyActor
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueskyActor"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal BlueskyActor(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Get private preferences attached to the current account. Expected use is synchronization between multiple devices, and import/export during account migration. Requires auth.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput?>> GetPreferencesAsync (CancellationToken cancellationToken = default)
        {
            return atp.GetPreferencesAsync(cancellationToken);
        }


        /// <summary>
        /// Get detailed profile view of an actor. Does not require auth, but contains relevant metadata with auth.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed?>> GetProfileAsync (FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            return atp.GetProfileAsync(actor, cancellationToken);
        }


        /// <summary>
        /// Get detailed profile views of multiple actors.
        /// </summary>
        /// <param name="actors"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.GetProfilesOutput?>> GetProfilesAsync (List<FishyFlip.Models.ATIdentifier> actors, CancellationToken cancellationToken = default)
        {
            return atp.GetProfilesAsync(actors, cancellationToken);
        }


        /// <summary>
        /// Get a list of suggested actors. Expected use is discovery of accounts to follow during new account onboarding.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.GetSuggestionsOutput?>> GetSuggestionsAsync (int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetSuggestionsAsync(limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Set the private preferences attached to the account.
        /// </summary>
        /// <param name="preferences">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.AdultContentPref"/> (app.bsky.actor.defs#adultContentPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ContentLabelPref"/> (app.bsky.actor.defs#contentLabelPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPref"/> (app.bsky.actor.defs#savedFeedsPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.SavedFeedsPrefV2"/> (app.bsky.actor.defs#savedFeedsPrefV2) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.PersonalDetailsPref"/> (app.bsky.actor.defs#personalDetailsPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.FeedViewPref"/> (app.bsky.actor.defs#feedViewPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ThreadViewPref"/> (app.bsky.actor.defs#threadViewPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.InterestsPref"/> (app.bsky.actor.defs#interestsPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.MutedWordsPref"/> (app.bsky.actor.defs#mutedWordsPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.HiddenPostsPref"/> (app.bsky.actor.defs#hiddenPostsPref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.BskyAppStatePref"/> (app.bsky.actor.defs#bskyAppStatePref) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.LabelersPref"/> (app.bsky.actor.defs#labelersPref) <br/>
        /// </param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> PutPreferencesAsync (List<ATObject> preferences, CancellationToken cancellationToken = default)
        {
            return atp.PutPreferencesAsync(preferences, cancellationToken);
        }


        /// <summary>
        /// Find actors (profiles) matching search criteria. Does not require auth.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput?>> SearchActorsAsync (string? q = default, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.SearchActorsAsync(q, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Find actor suggestions for a prefix search term. Expected use is for auto-completion during text field entry. Does not require auth.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsTypeaheadOutput?>> SearchActorsTypeaheadAsync (string? q = default, int? limit = 10, CancellationToken cancellationToken = default)
        {
            return atp.SearchActorsTypeaheadAsync(q, limit, cancellationToken);
        }

    }
}

