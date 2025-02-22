// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class DescribeServerOutput : ATObject, ICBOREncodable<DescribeServerOutput>, IJsonEncodable<DescribeServerOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DescribeServerOutput"/> class.
        /// </summary>
        /// <param name="inviteCodeRequired">If true, an invite code must be supplied to create an account on this instance.</param>
        /// <param name="phoneVerificationRequired">If true, a phone verification token must be supplied to create an account on this instance.</param>
        /// <param name="availableUserDomains">List of domain suffixes that can be used in account handles.</param>
        /// <param name="links">URLs of service policy documents.
        /// com.atproto.server.defs#links <br/>
        /// </param>
        /// <param name="contact">Contact information
        /// com.atproto.server.defs#contact <br/>
        /// </param>
        /// <param name="did"></param>
        public DescribeServerOutput(bool? inviteCodeRequired = default, bool? phoneVerificationRequired = default, List<string> availableUserDomains = default, FishyFlip.Lexicon.Com.Atproto.Server.Links? links = default, FishyFlip.Lexicon.Com.Atproto.Server.Contact? contact = default, FishyFlip.Models.ATDid did = default)
        {
            this.InviteCodeRequired = inviteCodeRequired;
            this.PhoneVerificationRequired = phoneVerificationRequired;
            this.AvailableUserDomains = availableUserDomains;
            this.Links = links;
            this.Contact = contact;
            this.Did = did;
            this.Type = "com.atproto.server.describeServer#DescribeServerOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DescribeServerOutput"/> class.
        /// </summary>
        public DescribeServerOutput()
        {
            this.Type = "com.atproto.server.describeServer#DescribeServerOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DescribeServerOutput"/> class.
        /// </summary>
        public DescribeServerOutput(CBORObject obj)
        {
            if (obj["inviteCodeRequired"] is not null) this.InviteCodeRequired = obj["inviteCodeRequired"].AsBoolean();
            if (obj["phoneVerificationRequired"] is not null) this.PhoneVerificationRequired = obj["phoneVerificationRequired"].AsBoolean();
            if (obj["availableUserDomains"] is not null) this.AvailableUserDomains = obj["availableUserDomains"].Values.Select(n =>n.AsString()).ToList();
            if (obj["links"] is not null) this.Links = new FishyFlip.Lexicon.Com.Atproto.Server.Links(obj["links"]);
            if (obj["contact"] is not null) this.Contact = new FishyFlip.Lexicon.Com.Atproto.Server.Contact(obj["contact"]);
            if (obj["did"] is not null) this.Did = obj["did"].ToATDid();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the inviteCodeRequired.
        /// <br/> If true, an invite code must be supplied to create an account on this instance.
        /// </summary>
        [JsonPropertyName("inviteCodeRequired")]
        public bool? InviteCodeRequired { get; set; }

        /// <summary>
        /// Gets or sets the phoneVerificationRequired.
        /// <br/> If true, a phone verification token must be supplied to create an account on this instance.
        /// </summary>
        [JsonPropertyName("phoneVerificationRequired")]
        public bool? PhoneVerificationRequired { get; set; }

        /// <summary>
        /// Gets or sets the availableUserDomains.
        /// <br/> List of domain suffixes that can be used in account handles.
        /// </summary>
        [JsonPropertyName("availableUserDomains")]
        [JsonRequired]
        public List<string> AvailableUserDomains { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// <br/> URLs of service policy documents.
        /// com.atproto.server.defs#links <br/>
        /// </summary>
        [JsonPropertyName("links")]
        public FishyFlip.Lexicon.Com.Atproto.Server.Links? Links { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// <br/> Contact information
        /// com.atproto.server.defs#contact <br/>
        /// </summary>
        [JsonPropertyName("contact")]
        public FishyFlip.Lexicon.Com.Atproto.Server.Contact? Contact { get; set; }

        /// <summary>
        /// Gets or sets the did.
        /// </summary>
        [JsonPropertyName("did")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid Did { get; set; }

        public const string RecordType = "com.atproto.server.describeServer#DescribeServerOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.DescribeServerOutput>)SourceGenerationContext.Default.ComAtprotoServerDescribeServerOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.DescribeServerOutput>)SourceGenerationContext.Default.ComAtprotoServerDescribeServerOutput);
        }

        public static new DescribeServerOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.DescribeServerOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.DescribeServerOutput>)SourceGenerationContext.Default.ComAtprotoServerDescribeServerOutput)!;
        }

         /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

         /// <inheritdoc/>
        public static new DescribeServerOutput FromCBORObject(CBORObject obj)
        {
            return new DescribeServerOutput(obj);
        }

    }
}

