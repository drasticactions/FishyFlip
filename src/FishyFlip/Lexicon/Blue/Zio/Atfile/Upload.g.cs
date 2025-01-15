// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Blue.Zio.Atfile
{
    /// <summary>
    /// A reference to an uploaded blob.
    /// </summary>
    public partial class Upload : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Upload"/> class.
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="checksum"></param>
        /// <param name="createdAt"></param>
        /// <param name="file"></param>
        /// <param name="finger">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Blue.Zio.Atfile.Browser"/> (blue.zio.atfile.finger#browser) <br/>
        /// <see cref="FishyFlip.Lexicon.Blue.Zio.Atfile.Machine"/> (blue.zio.atfile.finger#machine) <br/>
        /// </param>
        /// <param name="meta">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Blue.Zio.Atfile.Unknown"/> (blue.zio.atfile.meta#unknown) <br/>
        /// </param>
        public Upload(Blob? blob = default, Blue.Zio.Atfile.Checksum? checksum = default, DateTime? createdAt = default, Blue.Zio.Atfile.File? file = default, ATObject? finger = default, FishyFlip.Lexicon.Blue.Zio.Atfile.Unknown? meta = default)
        {
            this.Blob = blob;
            this.Checksum = checksum;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.File = file;
            this.Finger = finger;
            this.Meta = meta;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Upload"/> class.
        /// </summary>
        public Upload()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Upload"/> class.
        /// </summary>
        public Upload(CBORObject obj)
        {
            if (obj["blob"] is not null) this.Blob = new FishyFlip.Models.Blob(obj["blob"]);
            // Temp checksum
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            // Temp file
            if (obj["finger"] is not null) this.Finger = obj["finger"].ToATObject();
            if (obj["meta"] is not null) this.Meta = new FishyFlip.Lexicon.Blue.Zio.Atfile.Unknown(obj["meta"]);
        }

        /// <summary>
        /// Gets or sets the blob.
        /// </summary>
        [JsonPropertyName("blob")]
        public Blob? Blob { get; set; }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        [JsonPropertyName("checksum")]
        public Blue.Zio.Atfile.Checksum? Checksum { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        [JsonPropertyName("file")]
        public Blue.Zio.Atfile.File? File { get; set; }

        /// <summary>
        /// Gets or sets the finger.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Blue.Zio.Atfile.Browser"/> (blue.zio.atfile.finger#browser) <br/>
        /// <see cref="FishyFlip.Lexicon.Blue.Zio.Atfile.Machine"/> (blue.zio.atfile.finger#machine) <br/>
        /// </summary>
        [JsonPropertyName("finger")]
        public ATObject? Finger { get; set; }

        /// <summary>
        /// Gets or sets the meta.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Blue.Zio.Atfile.Unknown"/> (blue.zio.atfile.meta#unknown) <br/>
        /// </summary>
        [JsonPropertyName("meta")]
        public FishyFlip.Lexicon.Blue.Zio.Atfile.Unknown? Meta { get; set; }

        /// <summary>
        /// Gets the ATRecord Type.
        /// </summary>
        [JsonPropertyName("$type")]
        public override string Type => "blue.zio.atfile.upload";

        public const string RecordType = "blue.zio.atfile.upload";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Blue.Zio.Atfile.Upload>(this, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Zio.Atfile.Upload>)SourceGenerationContext.Default.BlueZioAtfileUpload)!;
        }

        public static Upload FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Blue.Zio.Atfile.Upload>(json, (JsonTypeInfo<FishyFlip.Lexicon.Blue.Zio.Atfile.Upload>)SourceGenerationContext.Default.BlueZioAtfileUpload)!;
        }
    }

    public partial class Checksum
    {
        /// <summary>
        /// Gets or sets the algo.
        /// </summary>
        [JsonPropertyName("algo")]
        public string? Algo { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }

    }

    public partial class File
    {
        /// <summary>
        /// Gets or sets the mimeType.
        /// </summary>
        [JsonPropertyName("mimeType")]
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the modifiedAt.
        /// </summary>
        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        [JsonPropertyName("size")]
        public long? Size { get; set; }

    }
}

