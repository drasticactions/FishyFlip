namespace FishyFlip.Models;

public record CreatePostRecord(string Collection, string Repo, PostRecord Record, string? Rkey = null, string? SwapCommit = null);

public record PutPostRecord(string Collection, string Repo, PostRecord Record, string? Rkey = null, string? SwapRecord = null, string? SwapCommit = null);