// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Embed
{
    public partial class ViewRecordDef : ATObject, ICBOREncodable<ViewRecordDef>, IJsonEncodable<ViewRecordDef>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRecordDef"/> class.
        /// </summary>
        /// <param name="record">
        /// <br/> Union Types: <br/>
        /// #viewRecord <br/>
        /// #viewNotFound <br/>
        /// #viewBlocked <br/>
        /// #viewDetached <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView"/> (app.bsky.feed.defs#generatorView) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.ListView"/> (app.bsky.graph.defs#listView) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Labeler.LabelerView"/> (app.bsky.labeler.defs#labelerView) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic"/> (app.bsky.graph.defs#starterPackViewBasic) <br/>
        /// </param>
        public ViewRecordDef(ATObject record = default)
        {
            this.Record = record;
            this.Type = "app.bsky.embed.record#view";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRecordDef"/> class.
        /// </summary>
        public ViewRecordDef()
        {
            this.Type = "app.bsky.embed.record#view";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRecordDef"/> class.
        /// </summary>
        public ViewRecordDef(CBORObject obj)
        {
            if (obj["record"] is not null) this.Record = obj["record"].ToATObject();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the record.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.ViewRecord"/> (app.bsky.embed.record#viewRecord) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.ViewNotFound"/> (app.bsky.embed.record#viewNotFound) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.ViewBlocked"/> (app.bsky.embed.record#viewBlocked) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Embed.ViewDetached"/> (app.bsky.embed.record#viewDetached) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Feed.GeneratorView"/> (app.bsky.feed.defs#generatorView) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.ListView"/> (app.bsky.graph.defs#listView) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Labeler.LabelerView"/> (app.bsky.labeler.defs#labelerView) <br/>
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.StarterPackViewBasic"/> (app.bsky.graph.defs#starterPackViewBasic) <br/>
        /// </summary>
        [JsonPropertyName("record")]
        [JsonRequired]
        public ATObject Record { get; set; }

        public const string RecordType = "app.bsky.embed.record#view";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.ViewRecordDef>)SourceGenerationContext.Default.AppBskyEmbedViewRecordDef);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.ViewRecordDef>)SourceGenerationContext.Default.AppBskyEmbedViewRecordDef);
        }

        public static new ViewRecordDef FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Embed.ViewRecordDef>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Embed.ViewRecordDef>)SourceGenerationContext.Default.AppBskyEmbedViewRecordDef)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ViewRecordDef FromCBORObject(CBORObject obj)
        {
            return new ViewRecordDef(obj);
        }

    }
}

