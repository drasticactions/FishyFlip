// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class CreateInviteCodeInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInviteCodeInput"/> class.
        /// </summary>
        /// <param name="useCount"></param>
        /// <param name="forAccount"></param>
        public CreateInviteCodeInput(long useCount = default, FishyFlip.Models.ATDid? forAccount = default)
        {
            this.UseCount = useCount;
            this.ForAccount = forAccount;
            this.Type = "com.atproto.server.createInviteCode#CreateInviteCodeInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInviteCodeInput"/> class.
        /// </summary>
        public CreateInviteCodeInput()
        {
            this.Type = "com.atproto.server.createInviteCode#CreateInviteCodeInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInviteCodeInput"/> class.
        /// </summary>
        public CreateInviteCodeInput(CBORObject obj)
        {
            if (obj["useCount"] is not null) this.UseCount = obj["useCount"].AsInt64Value();
            if (obj["forAccount"] is not null) this.ForAccount = obj["forAccount"].ToATDid();
        }

        /// <summary>
        /// Gets or sets the useCount.
        /// </summary>
        [JsonPropertyName("useCount")]
        [JsonRequired]
        public long UseCount { get; set; }

        /// <summary>
        /// Gets or sets the forAccount.
        /// </summary>
        [JsonPropertyName("forAccount")]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid? ForAccount { get; set; }

        public const string RecordType = "com.atproto.server.createInviteCode#CreateInviteCodeInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Server.CreateInviteCodeInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.CreateInviteCodeInput>)SourceGenerationContext.Default.ComAtprotoServerCreateInviteCodeInput)!;
        }

        public static CreateInviteCodeInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.CreateInviteCodeInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.CreateInviteCodeInput>)SourceGenerationContext.Default.ComAtprotoServerCreateInviteCodeInput)!;
        }
    }
}

