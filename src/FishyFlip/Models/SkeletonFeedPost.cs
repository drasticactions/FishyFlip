namespace FishyFlip.Models;

public record SkeletonFeed(SkeletonFeedPost[] Feed, string? Cursor);

public record SkeletonFeedPost(ATUri Post, SkeletonReasonRepost? Reason);

public record SkeletonReasonRepost(ATUri Repost);