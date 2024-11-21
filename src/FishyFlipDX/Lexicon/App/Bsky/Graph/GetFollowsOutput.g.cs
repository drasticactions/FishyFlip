// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetFollowsOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowsOutput"/> class.
        /// </summary>
        public GetFollowsOutput(App.Bsky.Actor.ProfileView? subject = default, string? cursor = default, List<App.Bsky.Actor.ProfileView>? follows = default)
        {
            this.Subject = subject;
            this.Cursor = cursor;
            this.Follows = follows;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowsOutput"/> class.
        /// </summary>
        public GetFollowsOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowsOutput"/> class.
        /// </summary>
        public GetFollowsOutput(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new App.Bsky.Actor.ProfileView(obj["subject"]);
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["follows"] is not null) this.Follows = obj["follows"].Values.Select(n =>new App.Bsky.Actor.ProfileView(n)).ToList();
        }

        [JsonPropertyName("subject")]
        [JsonRequired]
        public App.Bsky.Actor.ProfileView? Subject { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        [JsonPropertyName("follows")]
        [JsonRequired]
        public List<App.Bsky.Actor.ProfileView>? Follows { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getFollows#GetFollowsOutput";

        public const string RecordType = "app.bsky.graph.getFollows#GetFollowsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.GetFollowsOutput>(this, (JsonTypeInfo<App.Bsky.Graph.GetFollowsOutput>)SourceGenerationContext.Default.AppBskyGraphGetFollowsOutput)!;
        }

        public static GetFollowsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.GetFollowsOutput>(json, (JsonTypeInfo<App.Bsky.Graph.GetFollowsOutput>)SourceGenerationContext.Default.AppBskyGraphGetFollowsOutput)!;
        }
    }
}

