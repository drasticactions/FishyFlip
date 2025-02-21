// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    /// <summary>
    /// Mute incoming reports from an account
    /// </summary>
    public partial class ModEventMuteReporter : ATObject, ICBOREncodable<ModEventMuteReporter>, IJsonEncodable<ModEventMuteReporter>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventMuteReporter"/> class.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="durationInHours">Indicates how long the account should remain muted. Falsy value here means a permanent mute.</param>
        public ModEventMuteReporter(string? comment = default, long? durationInHours = default)
        {
            this.Comment = comment;
            this.DurationInHours = durationInHours;
            this.Type = "tools.ozone.moderation.defs#modEventMuteReporter";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventMuteReporter"/> class.
        /// </summary>
        public ModEventMuteReporter()
        {
            this.Type = "tools.ozone.moderation.defs#modEventMuteReporter";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ModEventMuteReporter"/> class.
        /// </summary>
        public ModEventMuteReporter(CBORObject obj)
        {
            if (obj["comment"] is not null) this.Comment = obj["comment"].AsString();
            if (obj["durationInHours"] is not null) this.DurationInHours = obj["durationInHours"].AsInt64Value();
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets the durationInHours.
        /// <br/> Indicates how long the account should remain muted. Falsy value here means a permanent mute.
        /// </summary>
        [JsonPropertyName("durationInHours")]
        public long? DurationInHours { get; set; }

        public const string RecordType = "tools.ozone.moderation.defs#modEventMuteReporter";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMuteReporter>)SourceGenerationContext.Default.ToolsOzoneModerationModEventMuteReporter);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMuteReporter>)SourceGenerationContext.Default.ToolsOzoneModerationModEventMuteReporter);
        }

        public static new ModEventMuteReporter FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMuteReporter>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMuteReporter>)SourceGenerationContext.Default.ToolsOzoneModerationModEventMuteReporter)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new ModEventMuteReporter FromCBORObject(CBORObject obj)
        {
            return new ModEventMuteReporter(obj);
        }

    }
}

