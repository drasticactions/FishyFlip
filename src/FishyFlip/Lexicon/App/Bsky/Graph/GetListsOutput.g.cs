// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetListsOutput : ATObject, ICBOREncodable<GetListsOutput>, IJsonEncodable<GetListsOutput>, IParsable<GetListsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="lists"></param>
        public GetListsOutput(string? cursor = default, List<FishyFlip.Lexicon.App.Bsky.Graph.ListView> lists = default)
        {
            this.Cursor = cursor;
            this.Lists = lists;
            this.Type = "app.bsky.graph.getLists#GetListsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListsOutput"/> class.
        /// </summary>
        public GetListsOutput()
        {
            this.Type = "app.bsky.graph.getLists#GetListsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListsOutput"/> class.
        /// </summary>
        public GetListsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["lists"] is not null) this.Lists = obj["lists"].Values.Select(n =>new FishyFlip.Lexicon.App.Bsky.Graph.ListView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the lists.
        /// </summary>
        [JsonPropertyName("lists")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.App.Bsky.Graph.ListView> Lists { get; set; }

        public const string RecordType = "app.bsky.graph.getLists#GetListsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>)SourceGenerationContext.Default.AppBskyGraphGetListsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>)SourceGenerationContext.Default.AppBskyGraphGetListsOutput);
        }

        public static new GetListsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Graph.GetListsOutput>)SourceGenerationContext.Default.AppBskyGraphGetListsOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetListsOutput FromCBORObject(CBORObject obj)
        {
            return new GetListsOutput(obj);
        }

        /// <inheritdoc/>
        public static GetListsOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetListsOutput>(s, (JsonTypeInfo<GetListsOutput>)SourceGenerationContext.Default.AppBskyGraphGetListsOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetListsOutput result)
        {
            result = JsonSerializer.Deserialize<GetListsOutput>(s, (JsonTypeInfo<GetListsOutput>)SourceGenerationContext.Default.AppBskyGraphGetListsOutput);
            return result != null;
        }
    }
}

