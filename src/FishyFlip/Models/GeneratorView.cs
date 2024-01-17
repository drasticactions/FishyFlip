namespace FishyFlip.Models;

public record GeneratorFeed(GeneratorView[] Feeds, string? Cursor);

public record GeneratorView(ATUri Uri, Cid Cid, ATDid Did, string Avatar, int LikeCount, string DisplayName, string Description, ActorProfile Creator, Viewer Viewer, DateTime IndexedAt);