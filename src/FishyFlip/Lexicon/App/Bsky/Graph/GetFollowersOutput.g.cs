// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetFollowersOutput : ATObject, IBatchItem
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowersOutput"/> class.
        /// </summary>
        /// <param name="subject">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </param>
        /// <param name="cursor"></param>
        /// <param name="followers"></param>
        public GetFollowersOutput(FishyFlip.Lexicon.App.Bsky.Actor.ProfileView subject = default, string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> followers = default)
        {
            this.Subject = subject;
            this.Cursor = cursor;
            this.Followers = followers;
            this.Type = "app.bsky.graph.getFollowers#GetFollowersOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowersOutput"/> class.
        /// </summary>
        public GetFollowersOutput()
        {
            this.Type = "app.bsky.graph.getFollowers#GetFollowersOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowersOutput"/> class.
        /// </summary>
        public GetFollowersOutput(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(obj["subject"]);
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["followers"] is not null) this.Followers = obj["followers"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public FishyFlip.Lexicon.App.Bsky.Actor.ProfileView Subject { get; set; }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the followers.
        /// </summary>
        [JsonPropertyName("followers")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> Followers { get; set; }

        public const string RecordType = "app.bsky.graph.getFollowers#GetFollowersOutput";

        public static GetFollowersOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowersOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowersOutput>)SourceGenerationContext.Default.AppBskyGraphGetFollowersOutput)!;
        }
    }
}

