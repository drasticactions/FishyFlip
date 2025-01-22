// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class RequestEmailUpdateOutput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEmailUpdateOutput"/> class.
        /// </summary>
        /// <param name="tokenRequired"></param>
        public RequestEmailUpdateOutput(bool tokenRequired = default)
        {
            this.TokenRequired = tokenRequired;
            this.Type = "com.atproto.server.requestEmailUpdate#RequestEmailUpdateOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEmailUpdateOutput"/> class.
        /// </summary>
        public RequestEmailUpdateOutput()
        {
            this.Type = "com.atproto.server.requestEmailUpdate#RequestEmailUpdateOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEmailUpdateOutput"/> class.
        /// </summary>
        public RequestEmailUpdateOutput(CBORObject obj)
        {
            if (obj["tokenRequired"] is not null) this.TokenRequired = obj["tokenRequired"].AsBoolean();
        }

        /// <summary>
        /// Gets or sets the tokenRequired.
        /// </summary>
        [JsonPropertyName("tokenRequired")]
        [JsonRequired]
        public bool TokenRequired { get; set; }

        public const string RecordType = "com.atproto.server.requestEmailUpdate#RequestEmailUpdateOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Server.RequestEmailUpdateOutput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RequestEmailUpdateOutput>)SourceGenerationContext.Default.ComAtprotoServerRequestEmailUpdateOutput)!;
        }

        public static RequestEmailUpdateOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.RequestEmailUpdateOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.RequestEmailUpdateOutput>)SourceGenerationContext.Default.ComAtprotoServerRequestEmailUpdateOutput)!;
        }
    }
}

