// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.App.Bsky.Graph
{
    public partial class GetListOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="list">
        /// <see cref="FishyFlip.Lexicon.App.Bsky.Graph.ListView"/> (app.bsky.graph.defs#listView)
        /// </param>
        /// <param name="items"></param>
        public GetListOutput(string? cursor = default, App.Bsky.Graph.ListView? list = default, List<App.Bsky.Graph.ListItemView>? items = default)
        {
            this.Cursor = cursor;
            this.List = list;
            this.Items = items;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListOutput"/> class.
        /// </summary>
        public GetListOutput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetListOutput"/> class.
        /// </summary>
        public GetListOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["list"] is not null) this.List = new App.Bsky.Graph.ListView(obj["list"]);
            if (obj["items"] is not null) this.Items = obj["items"].Values.Select(n =>new App.Bsky.Graph.ListItemView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the list.
        /// <br/> <see cref="FishyFlip.Lexicon.App.Bsky.Graph.ListView"/> (app.bsky.graph.defs#listView)
        /// </summary>
        [JsonPropertyName("list")]
        [JsonRequired]
        public App.Bsky.Graph.ListView? List { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        [JsonRequired]
        public List<App.Bsky.Graph.ListItemView>? Items { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "app.bsky.graph.getList#GetListOutput";

        public const string RecordType = "app.bsky.graph.getList#GetListOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<App.Bsky.Graph.GetListOutput>(this, (JsonTypeInfo<App.Bsky.Graph.GetListOutput>)SourceGenerationContext.Default.AppBskyGraphGetListOutput)!;
        }

        public static GetListOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<App.Bsky.Graph.GetListOutput>(json, (JsonTypeInfo<App.Bsky.Graph.GetListOutput>)SourceGenerationContext.Default.AppBskyGraphGetListOutput)!;
        }
    }
}

