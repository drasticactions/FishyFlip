namespace FishyFlip.Models;

public class FollowRecord : ATRecord
{
    [JsonConstructor]
    public FollowRecord(ATDid? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.GraphTypes.Block;
    }

    public ATDid? Subject { get; }

    public DateTime CreatedAt { get; }
}