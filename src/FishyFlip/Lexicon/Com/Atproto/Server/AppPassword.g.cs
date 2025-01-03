// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class AppPassword : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AppPassword"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="createdAt"></param>
        /// <param name="privileged"></param>
        public AppPassword(string? name = default, DateTime? createdAt = default, bool? privileged = default)
        {
            this.Name = name;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Privileged = privileged;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AppPassword"/> class.
        /// </summary>
        public AppPassword()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AppPassword"/> class.
        /// </summary>
        public AppPassword(CBORObject obj)
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
        public string? Name { get; set; }

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
            return JsonSerializer.Serialize<Com.Atproto.Server.AppPassword>(this, (JsonTypeInfo<Com.Atproto.Server.AppPassword>)SourceGenerationContext.Default.ComAtprotoServerAppPassword)!;
        }

        public static AppPassword FromJson(string json)
        {
            return JsonSerializer.Deserialize<Com.Atproto.Server.AppPassword>(json, (JsonTypeInfo<Com.Atproto.Server.AppPassword>)SourceGenerationContext.Default.ComAtprotoServerAppPassword)!;
        }
    }
}

