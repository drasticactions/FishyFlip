// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class AppPasswordDef : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AppPasswordDef"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createdAt"></param>
        /// <param name="privileged"></param>
        public AppPasswordDef(string name = default, DateTime? createdAt = default, bool? privileged = default)
        {
            this.Name = name;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Privileged = privileged;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AppPasswordDef"/> class.
        /// </summary>
        public AppPasswordDef()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AppPasswordDef"/> class.
        /// </summary>
        public AppPasswordDef(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["privileged"] is not null) this.Privileged = obj["privileged"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonRequired]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the privileged.
        /// </summary>
        [JsonPropertyName("privileged")]
        public bool? Privileged { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.server.listAppPasswords#appPassword";

        public const string RecordType = "com.atproto.server.listAppPasswords#appPassword";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Server.AppPasswordDef>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.AppPasswordDef>)SourceGenerationContext.Default.ComAtprotoServerAppPasswordDef)!;
        }

        public static AppPasswordDef FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.AppPasswordDef>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.AppPasswordDef>)SourceGenerationContext.Default.ComAtprotoServerAppPasswordDef)!;
        }
    }
}

