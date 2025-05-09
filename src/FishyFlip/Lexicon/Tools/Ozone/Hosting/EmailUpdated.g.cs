// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Hosting
{
    public partial class EmailUpdated : ATObject, ICBOREncodable<EmailUpdated>, IJsonEncodable<EmailUpdated>, IParsable<EmailUpdated>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailUpdated"/> class.
        /// </summary>
        /// <param name="email"></param>
        public EmailUpdated(string email = default)
        {
            this.Email = email;
            this.Type = "tools.ozone.hosting.getAccountHistory#emailUpdated";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmailUpdated"/> class.
        /// </summary>
        public EmailUpdated()
        {
            this.Type = "tools.ozone.hosting.getAccountHistory#emailUpdated";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmailUpdated"/> class.
        /// </summary>
        public EmailUpdated(CBORObject obj)
        {
            if (obj["email"] is not null) this.Email = obj["email"].AsString();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        [JsonRequired]
        public string Email { get; set; }

        public const string RecordType = "tools.ozone.hosting.getAccountHistory#emailUpdated";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Hosting.EmailUpdated>)SourceGenerationContext.Default.ToolsOzoneHostingEmailUpdated);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Hosting.EmailUpdated>)SourceGenerationContext.Default.ToolsOzoneHostingEmailUpdated);
        }

        public static new EmailUpdated FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Hosting.EmailUpdated>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Hosting.EmailUpdated>)SourceGenerationContext.Default.ToolsOzoneHostingEmailUpdated)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new EmailUpdated FromCBORObject(CBORObject obj)
        {
            return new EmailUpdated(obj);
        }

        /// <inheritdoc/>
        public static EmailUpdated Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<EmailUpdated>(s, (JsonTypeInfo<EmailUpdated>)SourceGenerationContext.Default.ToolsOzoneHostingEmailUpdated)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out EmailUpdated result)
        {
            result = JsonSerializer.Deserialize<EmailUpdated>(s, (JsonTypeInfo<EmailUpdated>)SourceGenerationContext.Default.ToolsOzoneHostingEmailUpdated);
            return result != null;
        }
    }
}

