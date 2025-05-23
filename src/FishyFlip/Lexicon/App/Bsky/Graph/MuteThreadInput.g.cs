// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class MuteThreadInput : ATObject, ICBOREncodable<MuteThreadInput>, IJsonEncodable<MuteThreadInput>, IParsable<MuteThreadInput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MuteThreadInput"/> class.
        /// </summary>
        /// <param name="root"></param>
        public MuteThreadInput(FishyFlip.Models.ATUri root = default)
        {
            this.Root = root;
            this.Type = "app.bsky.graph.muteThread#MuteThreadInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteThreadInput"/> class.
        /// </summary>
        public MuteThreadInput()
        {
            this.Type = "app.bsky.graph.muteThread#MuteThreadInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuteThreadInput"/> class.
        /// </summary>
        public MuteThreadInput(CBORObject obj)
        {
            if (obj["root"] is not null) this.Root = obj["root"].ToATUri();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the root.
        /// </summary>
        [JsonPropertyName("root")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]
        public FishyFlip.Models.ATUri Root { get; set; }

        public const string RecordType = "app.bsky.graph.muteThread#MuteThreadInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.MuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphMuteThreadInput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.MuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphMuteThreadInput);
        }

        public static new MuteThreadInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.MuteThreadInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.MuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphMuteThreadInput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new MuteThreadInput FromCBORObject(CBORObject obj)
        {
            return new MuteThreadInput(obj);
        }

        /// <inheritdoc/>
        public static MuteThreadInput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<MuteThreadInput>(s, (JsonTypeInfo<MuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphMuteThreadInput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out MuteThreadInput result)
        {
            result = JsonSerializer.Deserialize<MuteThreadInput>(s, (JsonTypeInfo<MuteThreadInput>)SourceGenerationContext.Default.AppBskyGraphMuteThreadInput);
            return result != null;
        }
    }
}

