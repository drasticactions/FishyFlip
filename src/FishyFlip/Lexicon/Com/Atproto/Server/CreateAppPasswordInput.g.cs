// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class CreateAppPasswordInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAppPasswordInput"/> class.
        /// </summary>
        /// <param name="name">A short name for the App Password, to help distinguish them.</param>
        /// <param name="privileged">If an app password has 'privileged' access to possibly sensitive account state. Meant for use with trusted clients.</param>
        public CreateAppPasswordInput(string? name = default, bool? privileged = default)
        {
            this.Name = name;
            this.Privileged = privileged;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAppPasswordInput"/> class.
        /// </summary>
        public CreateAppPasswordInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAppPasswordInput"/> class.
        /// </summary>
        public CreateAppPasswordInput(CBORObject obj)
        {
            if (obj["name"] is not null) this.Name = obj["name"].AsString();
            if (obj["privileged"] is not null) this.Privileged = obj["privileged"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the name.
        /// <br/> A short name for the App Password, to help distinguish them.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonRequired]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the privileged.
        /// <br/> If an app password has 'privileged' access to possibly sensitive account state. Meant for use with trusted clients.
        /// </summary>
        [JsonPropertyName("privileged")]
        public bool? Privileged { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.server.createAppPassword#CreateAppPasswordInput";

        public const string RecordType = "com.atproto.server.createAppPassword#CreateAppPasswordInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Server.CreateAppPasswordInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.CreateAppPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerCreateAppPasswordInput)!;
        }

        public static CreateAppPasswordInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.CreateAppPasswordInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.CreateAppPasswordInput>)SourceGenerationContext.Default.ComAtprotoServerCreateAppPasswordInput)!;
        }
    }
}

