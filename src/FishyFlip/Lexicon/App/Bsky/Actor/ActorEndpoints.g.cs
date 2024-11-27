// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Actor
{

    /// <summary>
    /// app.bsky.actor Endpoint Group.
    /// </summary>
    public static class ActorEndpoints
    {

       public const string GetPreferences = "/xrpc/app.bsky.actor.getPreferences";

       public const string GetProfile = "/xrpc/app.bsky.actor.getProfile";

       public const string GetProfiles = "/xrpc/app.bsky.actor.getProfiles";

       public const string GetSuggestions = "/xrpc/app.bsky.actor.getSuggestions";

       public const string PutPreferences = "/xrpc/app.bsky.actor.putPreferences";

       public const string SearchActors = "/xrpc/app.bsky.actor.searchActors";

       public const string SearchActorsTypeahead = "/xrpc/app.bsky.actor.searchActorsTypeahead";


        /// <summary>
        /// Get private preferences attached to the current account. Expected use is synchronization between multiple devices, and import/export during account migration. Requires auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput?>> GetPreferencesAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetPreferences.ToString();
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Actor.GetPreferencesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyActorGetPreferencesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get detailed profile view of an actor. Does not require auth, but contains relevant metadata with auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed?>> GetProfileAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATIdentifier actor, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetProfile.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("actor=" + actor);

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Actor.ProfileViewDetailed>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyActorProfileViewDetailed!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get detailed profile views of multiple actors.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="actors"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Actor.GetProfilesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.GetProfilesOutput?>> GetProfilesAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATIdentifier> actors, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetProfiles.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", actors.Select(n => "actors=" + n)));

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Actor.GetProfilesOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyActorGetProfilesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a list of suggested actors. Expected use is discovery of accounts to follow during new account onboarding.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Actor.GetSuggestionsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.GetSuggestionsOutput?>> GetSuggestionsAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetSuggestions.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Actor.GetSuggestionsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyActorGetSuggestionsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Set the private preferences attached to the account.
        /// </summary>
        /// <param name="atp"></param>
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
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> PutPreferencesAsync (this FishyFlip.ATProtocol atp, List<ATObject> preferences, CancellationToken cancellationToken = default)
        {
            var endpointUrl = PutPreferences.ToString();
            var inputItem = new PutPreferencesInput();
            inputItem.Preferences = preferences;
            return atp.Client.Post<PutPreferencesInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyActorPutPreferencesInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Find actors (profiles) matching search criteria. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput?>> SearchActorsAsync (this FishyFlip.ATProtocol atp, string? q = default, int? limit = 25, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchActors.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (q != null)
            {
                queryStrings.Add("q=" + q);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyActorSearchActorsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Find actor suggestions for a prefix search term. Expected use is for auto-completion during text field entry. Does not require auth.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsTypeaheadOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsTypeaheadOutput?>> SearchActorsTypeaheadAsync (this FishyFlip.ATProtocol atp, string? q = default, int? limit = 10, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchActorsTypeahead.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (q != null)
            {
                queryStrings.Add("q=" + q);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.App.Bsky.Actor.SearchActorsTypeaheadOutput>(endpointUrl, atp.Options.SourceGenerationContext.AppBskyActorSearchActorsTypeaheadOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }

    }
}

