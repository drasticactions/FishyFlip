// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Set
{
    public partial class SetView : ATObject, ICBOREncodable<SetView>, IJsonEncodable<SetView>, IParsable<SetView>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SetView"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="setSize"></param>
        /// <param name="createdAt"></param>
        /// <param name="updatedAt"></param>
        public SetView(string name = default, string? description = default, long setSize = default, DateTime? createdAt = default, DateTime? updatedAt = default)
        {
            this.Name = name;
            this.Description = description;
            this.SetSize = setSize;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.UpdatedAt = updatedAt;
            this.Type = "tools.ozone.set.defs#setView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SetView"/> class.
        /// </summary>
        public SetView()
        {
            this.Type = "tools.ozone.set.defs#setView";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SetView"/> class.
        /// </summary>
        public SetView(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["description"] is not null) this.Description = obj["description"].AsString();
            if (obj["setSize"] is not null) this.SetSize = obj["setSize"].AsInt64Value();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["updatedAt"] is not null) this.UpdatedAt = obj["updatedAt"].ToDateTime();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the setSize.
        /// </summary>
        [JsonPropertyName("setSize")]
        [JsonRequired]
        public long SetSize { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonRequired]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the updatedAt.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        [JsonRequired]
        public DateTime? UpdatedAt { get; set; }

        public const string RecordType = "tools.ozone.set.defs#setView";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Set.SetView>)SourceGenerationContext.Default.ToolsOzoneSetSetView);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Set.SetView>)SourceGenerationContext.Default.ToolsOzoneSetSetView);
        }

        public static new SetView FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Set.SetView>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Set.SetView>)SourceGenerationContext.Default.ToolsOzoneSetSetView)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new SetView FromCBORObject(CBORObject obj)
        {
            return new SetView(obj);
        }

        /// <inheritdoc/>
        public static SetView Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<SetView>(s, (JsonTypeInfo<SetView>)SourceGenerationContext.Default.ToolsOzoneSetSetView)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out SetView result)
        {
            result = JsonSerializer.Deserialize<SetView>(s, (JsonTypeInfo<SetView>)SourceGenerationContext.Default.ToolsOzoneSetSetView);
            return result != null;
        }
    }
}

