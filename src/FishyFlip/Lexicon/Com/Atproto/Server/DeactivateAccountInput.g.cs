// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Server
{
    public partial class DeactivateAccountInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateAccountInput"/> class.
        /// </summary>
        /// <param name="deleteAfter">A recommendation to server as to how long they should hold onto the deactivated account before deleting.</param>
        public DeactivateAccountInput(DateTime? deleteAfter = default)
        {
            this.DeleteAfter = deleteAfter;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateAccountInput"/> class.
        /// </summary>
        public DeactivateAccountInput()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateAccountInput"/> class.
        /// </summary>
        public DeactivateAccountInput(CBORObject obj)
        {
            if (obj["deleteAfter"] is not null) this.DeleteAfter = obj["deleteAfter"].ToDateTime();
        }

        /// <summary>
        /// Gets or sets the deleteAfter.
        /// <br/> A recommendation to server as to how long they should hold onto the deactivated account before deleting.
        /// </summary>
        [JsonPropertyName("deleteAfter")]
        public DateTime? DeleteAfter { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "com.atproto.server.deactivateAccount#DeactivateAccountInput";

        public const string RecordType = "com.atproto.server.deactivateAccount#DeactivateAccountInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Com.Atproto.Server.DeactivateAccountInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.DeactivateAccountInput>)SourceGenerationContext.Default.ComAtprotoServerDeactivateAccountInput)!;
        }

        public static DeactivateAccountInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Com.Atproto.Server.DeactivateAccountInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Com.Atproto.Server.DeactivateAccountInput>)SourceGenerationContext.Default.ComAtprotoServerDeactivateAccountInput)!;
        }
    }
}

