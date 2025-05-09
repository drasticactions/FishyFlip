// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.App.Bsky.Feed
{
    public partial class SkeletonReasonPin : ATObject, ICBOREncodable<SkeletonReasonPin>, IJsonEncodable<SkeletonReasonPin>, IParsable<SkeletonReasonPin>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonReasonPin"/> class.
        /// </summary>
        public SkeletonReasonPin()
        {
            this.Type = "app.bsky.feed.defs#skeletonReasonPin";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonReasonPin"/> class.
        /// </summary>
        public SkeletonReasonPin(CBORObject obj)
        {
            if (obj["$type"] is not null) this.Type = obj["$type"].AsString();
        }

        public const string RecordType = "app.bsky.feed.defs#skeletonReasonPin";

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.SkeletonReasonPin>)SourceGenerationContext.Default.AppBskyFeedSkeletonReasonPin);
        }

        public override byte[] ToUtf8Json()
        {
            return JsonSerializer.SerializeToUtf8Bytes(this, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.SkeletonReasonPin>)SourceGenerationContext.Default.AppBskyFeedSkeletonReasonPin);
        }

        public static new SkeletonReasonPin FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.App.Bsky.Feed.SkeletonReasonPin>(json, (JsonTypeInfo<FishyFlip.Lexicon.App.Bsky.Feed.SkeletonReasonPin>)SourceGenerationContext.Default.AppBskyFeedSkeletonReasonPin)!;
        }

        /// <inheritdoc/>
        public override CBORObject ToCBORObject()
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
            return CBORObject.ReadJSON(jsonStream);
        }

        /// <inheritdoc/>
        public static new SkeletonReasonPin FromCBORObject(CBORObject obj)
        {
            return new SkeletonReasonPin(obj);
        }

        /// <inheritdoc/>
        public static SkeletonReasonPin Parse(string s, IFormatProvider? provider)
        {
            return JsonSerializer.Deserialize<SkeletonReasonPin>(s, (JsonTypeInfo<SkeletonReasonPin>)SourceGenerationContext.Default.AppBskyFeedSkeletonReasonPin)!;
        }

        /// <inheritdoc/>
        public static bool TryParse(string? s, IFormatProvider? provider, out SkeletonReasonPin result)
        {
            result = JsonSerializer.Deserialize<SkeletonReasonPin>(s, (JsonTypeInfo<SkeletonReasonPin>)SourceGenerationContext.Default.AppBskyFeedSkeletonReasonPin);
            return result != null;
        }
    }
}

