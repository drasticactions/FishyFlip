// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Netlify.Aniblue
{
    /// <summary>
    /// A record that stores the status of the anime.
    /// </summary>
    public partial class Status : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="status"></param>
        public Status(List<FishyFlip.Lexicon.App.Netlify.Aniblue.StatusDef>? status)
        {
            this.StatusValue = status;
            this.Type = "app.netlify.aniblue.status";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status()
        {
            this.Type = "app.netlify.aniblue.status";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status(CBORObject obj)
        {
            if (obj["status"] is not null) this.StatusValue = obj["status"].Values.Select(n =>new FishyFlip.Lexicon.App.Netlify.Aniblue.StatusDef(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonPropertyName("status")]
        public List<FishyFlip.Lexicon.App.Netlify.Aniblue.StatusDef>? StatusValue { get; set; }

        public const string RecordType = "app.netlify.aniblue.status";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.App.Netlify.Aniblue.Status>(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Netlify.Aniblue.Status>)SourceGenerationContext.Default.AppNetlifyAniblueStatus)!;
        }

        public static Status FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Netlify.Aniblue.Status>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Netlify.Aniblue.Status>)SourceGenerationContext.Default.AppNetlifyAniblueStatus)!;
        }
    }
}

