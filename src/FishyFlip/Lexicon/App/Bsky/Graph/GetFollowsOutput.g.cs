// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetFollowsOutput : ATObject, ICBOREncodable<GetFollowsOutput>, IJsonEncodable<GetFollowsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowsOutput"/> class.
        /// </summary>
        /// <param name="subject">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Actor.ProfileView"/> (app.bsky.actor.defs#profileView)
        /// </param>
        /// <param name="cursor"></param>
        /// <param name="follows"></param>
        public GetFollowsOutput(FishyFlip.Lexicon.App.Bsky.Actor.ProfileView subject = default, string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> follows = default)
        {
            this.Subject = subject;
            this.Cursor = cursor;
            this.Follows = follows;
            this.Type = "app.bsky.graph.getFollows#GetFollowsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowsOutput"/> class.
        /// </summary>
        public GetFollowsOutput()
        {
            this.Type = "app.bsky.graph.getFollows#GetFollowsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowsOutput"/> class.
        /// </summary>
        public GetFollowsOutput(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(obj["subject"]);
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["follows"] is not null) this.Follows = obj["follows"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Actor.ProfileView(n)).ToList();
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
        /// Gets or sets the follows.
        /// </summary>
        [JsonPropertyName("follows")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Actor.ProfileView> Follows { get; set; }

        public const string RecordType = "app.bsky.graph.getFollows#GetFollowsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput>)SourceGenerationContext.Default.AppBskyGraphGetFollowsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput>)SourceGenerationContext.Default.AppBskyGraphGetFollowsOutput);
        }

        public static new GetFollowsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetFollowsOutput>)SourceGenerationContext.Default.AppBskyGraphGetFollowsOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new GetFollowsOutput FromCBORObject(CBORObject obj)
        {
            return new GetFollowsOutput(obj);
        }

    }
}

