// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Ma.Tokono.Byov
{
    /// <summary>
    /// A reference to a video.
    /// </summary>
    public partial class Video : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="id"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="title"></param>
        /// <param name="createdAt"></param>
        public Video(string? cid, string? id, string? serviceProvider, string? title, DateTime? createdAt = default)
        {
            this.Cid = cid;
            this.CreatedAt = createdAt ?? DateTime.UtcNow;
            this.Id = id;
            this.ServiceProvider = serviceProvider;
            this.Title = title;
            this.Type = "ma.tokono.byov.video";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        public Video()
        {
            this.Type = "ma.tokono.byov.video";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        public Video(CBORObject obj)
        {
            if (obj["cid"] is not null) this.Cid = obj["cid"].AsString();
            if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
            if (obj["id"] is not null) this.Id = obj["id"].AsString();
            if (obj["serviceProvider"] is not null) this.ServiceProvider = obj["serviceProvider"].AsString();
            if (obj["title"] is not null) this.Title = obj["title"].AsString();
        }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        [JsonPropertyName("cid")]
        public string? Cid { get; set; }

        /// <summary>
        /// Gets or sets the createdAt.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; } = default;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the serviceProvider.
        /// </summary>
        [JsonPropertyName("serviceProvider")]
        public string? ServiceProvider { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        public const string RecordType = "ma.tokono.byov.video";

        public static Video FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Ma.Tokono.Byov.Video>(json, (JsonTypeInfo<FishyFlip.Lexicon.Ma.Tokono.Byov.Video>)SourceGenerationContext.Default.MaTokonoByovVideo)!;
        }
    }
}

