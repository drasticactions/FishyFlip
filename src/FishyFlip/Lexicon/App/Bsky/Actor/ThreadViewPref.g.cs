// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class ThreadViewPref : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadViewPref"/> class.
        /// </summary>
        /// <param name="sort">Sorting mode for threads.
        /// <br/> Known Values: <br/>
        /// oldest <br/>
        /// newest <br/>
        /// most-likes <br/>
        /// random <br/>
        /// hotness <br/>
        /// </param>
        /// <param name="prioritizeFollowedUsers">Show followed users at the top of all replies.</param>
        public ThreadViewPref(string? sort = default, bool? prioritizeFollowedUsers = default)
        {
            this.Sort = sort;
            this.PrioritizeFollowedUsers = prioritizeFollowedUsers;
            this.Type = "app.bsky.actor.defs#threadViewPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadViewPref"/> class.
        /// </summary>
        public ThreadViewPref()
        {
            this.Type = "app.bsky.actor.defs#threadViewPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadViewPref"/> class.
        /// </summary>
        public ThreadViewPref(CBORObject obj)
        {
            if (obj["sort"] is not null) this.Sort = obj["sort"].AsString();
            if (obj["prioritizeFollowedUsers"] is not null) this.PrioritizeFollowedUsers = obj["prioritizeFollowedUsers"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the sort.
        /// <br/> Sorting mode for threads.
        /// <br/> Known Values: <br/>
        /// oldest <br/>
        /// newest <br/>
        /// most-likes <br/>
        /// random <br/>
        /// hotness <br/>
        /// </summary>
        [JsonPropertyName("sort")]
        public string? Sort { get; set; }

        /// <summary>
        /// Gets or sets the prioritizeFollowedUsers.
        /// <br/> Show followed users at the top of all replies.
        /// </summary>
        [JsonPropertyName("prioritizeFollowedUsers")]
        public bool? PrioritizeFollowedUsers { get; set; }

        public const string RecordType = "app.bsky.actor.defs#threadViewPref";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Bsky.Actor.ThreadViewPref>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ThreadViewPref>)SourceGenerationContext.Default.AppBskyActorThreadViewPref)!;
        }

        public static ThreadViewPref FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.ThreadViewPref>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ThreadViewPref>)SourceGenerationContext.Default.AppBskyActorThreadViewPref)!;
        }
    }
}

