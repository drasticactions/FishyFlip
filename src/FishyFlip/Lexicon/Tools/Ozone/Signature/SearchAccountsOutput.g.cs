// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Signature
{
    public partial class SearchAccountsOutput : ATObject, ICBOREncodable<SearchAccountsOutput>, IJsonEncodable<SearchAccountsOutput>, IParsable<SearchAccountsOutput>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountsOutput"/> class.
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="accounts"></param>
        public SearchAccountsOutput(string? cursor = default, List<FishyFlip.Lexicon.Com.Atproto.Admin.AccountView> accounts = default)
        {
            this.Cursor = cursor;
            this.Accounts = accounts;
            this.Type = "tools.ozone.signature.searchAccounts#SearchAccountsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountsOutput"/> class.
        /// </summary>
        public SearchAccountsOutput()
        {
            this.Type = "tools.ozone.signature.searchAccounts#SearchAccountsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountsOutput"/> class.
        /// </summary>
        public SearchAccountsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["accounts"] is not null) this.Accounts = obj["accounts"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Admin.AccountView(n)).ToList();
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        [JsonPropertyName("accounts")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Admin.AccountView> Accounts { get; set; }

        public const string RecordType = "tools.ozone.signature.searchAccounts#SearchAccountsOutput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.SearchAccountsOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureSearchAccountsOutput);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.SearchAccountsOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureSearchAccountsOutput);
        }

        public static new SearchAccountsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Signature.SearchAccountsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Signature.SearchAccountsOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureSearchAccountsOutput)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new SearchAccountsOutput FromCBORObject(CBORObject obj)
        {
            return new SearchAccountsOutput(obj);
        }

        /// <inheritdoc/>
        public static SearchAccountsOutput Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<SearchAccountsOutput>(s, (JsonTypeInfo<SearchAccountsOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureSearchAccountsOutput)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out SearchAccountsOutput result)
        {
            result = JsonSerializer.Deserialize<SearchAccountsOutput>(s, (JsonTypeInfo<SearchAccountsOutput>)SourceGenerationContext.Default.ToolsOzoneSignatureSearchAccountsOutput);
            return result != null;
        }
    }
}

