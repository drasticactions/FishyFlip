// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{
    public partial class SearchAccountsOutput : ATObject
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
            this.Type = "com.atproto.admin.searchAccounts#SearchAccountsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountsOutput"/> class.
        /// </summary>
        public SearchAccountsOutput()
        {
            this.Type = "com.atproto.admin.searchAccounts#SearchAccountsOutput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountsOutput"/> class.
        /// </summary>
        public SearchAccountsOutput(CBORObject obj)
        {
            if (obj["cursor"] is not null) this.Cursor = obj["cursor"].AsString();
            if (obj["accounts"] is not null) this.Accounts = obj["accounts"].Values.Select(n =>new FishyFlip.Lexicon.Com.Atproto.Admin.AccountView(n)).ToList();
        }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        [JsonPropertyName("accounts")]
        [JsonRequired]
        public List<FishyFlip.Lexicon.Com.Atproto.Admin.AccountView> Accounts { get; set; }

        public const string RecordType = "com.atproto.admin.searchAccounts#SearchAccountsOutput";

        public static SearchAccountsOutput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Admin.SearchAccountsOutput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Admin.SearchAccountsOutput>)SourceGenerationContext.Default.ComAtprotoAdminSearchAccountsOutput)!;
        }
    }
}

