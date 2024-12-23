// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

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
        public Status(List<App.Netlify.Aniblue.StatusDef>? status)
        {
            this.StatusValue = status;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status(CBORObject obj)
        {
            if (obj["status"] is not null) this.StatusValue = obj["status"].Values.Select(n =>new App.Netlify.Aniblue.StatusDef(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonPropertyName("status")]
        public List<App.Netlify.Aniblue.StatusDef>? StatusValue { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.netlify.aniblue.status";

        public const string RecordType = "app.netlify.aniblue.status";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Netlify.Aniblue.Status>(this, (JsonTypeInfo<App.Netlify.Aniblue.Status>)SourceGenerationContext.Default.AppNetlifyAniblueStatus)!;
        }

        public static Status FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Netlify.Aniblue.Status>(json, (JsonTypeInfo<App.Netlify.Aniblue.Status>)SourceGenerationContext.Default.AppNetlifyAniblueStatus)!;
        }
    }
}
