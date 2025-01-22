// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Actor
{
    public partial class ContentLabelPref : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLabelPref"/> class.
        /// </summary>
        /// <param name="labelerDid">Which labeler does this preference apply to? If undefined, applies globally.</param>
        /// <param name="label"></param>
        /// <param name="visibility">
        /// <br/> Known Values: <br/>
        /// ignore <br/>
        /// show <br/>
        /// warn <br/>
        /// hide <br/>
        /// </param>
        public ContentLabelPref(FishyFlip.Models.ATDid? labelerDid = default, string label = default, string visibility = default)
        {
            this.LabelerDid = labelerDid;
            this.Label = label;
            this.Visibility = visibility;
            this.Type = "app.bsky.actor.defs#contentLabelPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLabelPref"/> class.
        /// </summary>
        public ContentLabelPref()
        {
            this.Type = "app.bsky.actor.defs#contentLabelPref";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLabelPref"/> class.
        /// </summary>
        public ContentLabelPref(CBORObject obj)
        {
            if (obj["labelerDid"] is not null) this.LabelerDid = obj["labelerDid"].ToATDid();
            if (obj["label"] is not null) this.Label = obj["label"].AsString();
            if (obj["visibility"] is not null) this.Visibility = obj["visibility"].AsString();
        }

        /// <summary>
        /// Gets or sets the labelerDid.
        /// <br/> Which labeler does this preference apply to? If undefined, applies globally.
        /// </summary>
        [JsonPropertyName("labelerDid")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? LabelerDid { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonPropertyName("label")]
        [JsonRequired]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// <br/> Known Values: <br/>
        /// ignore <br/>
        /// show <br/>
        /// warn <br/>
        /// hide <br/>
        /// </summary>
        [JsonPropertyName("visibility")]
        [JsonRequired]
        public string Visibility { get; set; }

        public const string RecordType = "app.bsky.actor.defs#contentLabelPref";

        public static ContentLabelPref FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Actor.ContentLabelPref>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Actor.ContentLabelPref>)SourceGenerationContext.Default.AppBskyActorContentLabelPref)!;
        }
    }
}

