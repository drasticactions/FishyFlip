// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Hosting
{
    public partial class GetAccountHistoryOutput : ATObject, ICBOREncodable<GetAccountHistoryOutput>, IJsonEncodable<GetAccountHistoryOutput>, IParsable<GetAccountHistoryOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountHistoryOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="events"></param>
        public GetAccountHistoryOutput(string? cursor = default, List<FishyFlip.Lexicon.Tools.Ozone.Hosting.Event> events = default)
        {
            this.Cursor = cursor;
            this.Events = events;
            this.Type = "tools.ozone.hosting.getAccountHistory#GetAccountHistoryOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountHistoryOutput"/> class.
        /// </summary>
        public GetAccountHistoryOutput()
        {
            this.Type = "tools.ozone.hosting.getAccountHistory#GetAccountHistoryOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountHistoryOutput"/> class.
        /// </summary>
        public GetAccountHistoryOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["events"] is not null) this.Events = obj["events"].Values.Select(n =>new FishyFlip.Lexicon.Tools.Ozone.Hosting.Event(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        [JsonPropertyName("events")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Tools.Ozone.Hosting.Event> Events { get; set; }

        public const string RecordType = "tools.ozone.hosting.getAccountHistory#GetAccountHistoryOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Hosting.GetAccountHistoryOutput>)SourceGenerationContext.Default.ToolsOzoneHostingGetAccountHistoryOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Hosting.GetAccountHistoryOutput>)SourceGenerationContext.Default.ToolsOzoneHostingGetAccountHistoryOutput);
        }

        public static new GetAccountHistoryOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Hosting.GetAccountHistoryOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Hosting.GetAccountHistoryOutput>)SourceGenerationContext.Default.ToolsOzoneHostingGetAccountHistoryOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new GetAccountHistoryOutput FromCBORObject(CBORObject obj)
        {
            return new GetAccountHistoryOutput(obj);
        }

        /// <inheritdoc/>
        public static GetAccountHistoryOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<GetAccountHistoryOutput>(s, (JsonTypeInfo<GetAccountHistoryOutput>)SourceGenerationContext.Default.ToolsOzoneHostingGetAccountHistoryOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out GetAccountHistoryOutput result)
        {
            result = JsonSerializer.Deserialize<GetAccountHistoryOutput>(s, (JsonTypeInfo<GetAccountHistoryOutput>)SourceGenerationContext.Default.ToolsOzoneHostingGetAccountHistoryOutput);
            return result != null;
        }
    }
}

