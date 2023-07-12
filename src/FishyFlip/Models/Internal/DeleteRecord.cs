namespace FishyFlip.Models.Internal;

public record DeleteRecord(string Collection, string Repo, string Rkey, Cid? SwapRecord = null, Cid? SwapCommit = null);