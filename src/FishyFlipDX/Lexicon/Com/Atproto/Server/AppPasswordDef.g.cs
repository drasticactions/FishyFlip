// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class AppPasswordDef : ATObject
    {

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

        [JsonPropertyName("name")]
        [JsonRequired]
        public string? Name { get; set; }

        [JsonPropertyName("createdAt")]
        [JsonRequired]
        public DateTime? CreatedAt { get; set; }

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
            return JsonSerializer.Serialize<Com.Atproto.Server.AppPasswordDef>(this, (JsonTypeInfo<Com.Atproto.Server.AppPasswordDef>)SourceGenerationContext.Default.ComAtprotoServerAppPasswordDef)!;
        }

        public static AppPasswordDef FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Server.AppPasswordDef>(json, (JsonTypeInfo<Com.Atproto.Server.AppPasswordDef>)SourceGenerationContext.Default.ComAtprotoServerAppPasswordDef)!;
        }
    }
}

