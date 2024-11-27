// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetKnownFollowersOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetKnownFollowersOutput"/> class.
        /// </summary>
        /// <param name="subject">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </param>
        /// <param name="cursor"></param>
        /// <param name="followers"></param>
        public GetKnownFollowersOutput(App.Bsky.Actor.ProfileView? subject = default, string? cursor = default, List<App.Bsky.Actor.ProfileView>? followers = default)
        {
            this.Subject = subject;
            this.Cursor = cursor;
            this.Followers = followers;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetKnownFollowersOutput"/> class.
        /// </summary>
        public GetKnownFollowersOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetKnownFollowersOutput"/> class.
        /// </summary>
        public GetKnownFollowersOutput(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new App.Bsky.Actor.ProfileView(obj["subject"]);
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["followers"] is not null) this.Followers = obj["followers"].Values.Select(n =>new App.Bsky.Actor.ProfileView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public App.Bsky.Actor.ProfileView? Subject { get; set; }

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
        public List<App.Bsky.Actor.ProfileView>? Followers { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getKnownFollowers#GetKnownFollowersOutput";

        public const string RecordType = "app.bsky.graph.getKnownFollowers#GetKnownFollowersOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.GetKnownFollowersOutput>(this, (JsonTypeInfo<App.Bsky.Graph.GetKnownFollowersOutput>)SourceGenerationContext.Default.AppBskyGraphGetKnownFollowersOutput)!;
        }

        public static GetKnownFollowersOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.GetKnownFollowersOutput>(json, (JsonTypeInfo<App.Bsky.Graph.GetKnownFollowersOutput>)SourceGenerationContext.Default.AppBskyGraphGetKnownFollowersOutput)!;
        }
    }
}

