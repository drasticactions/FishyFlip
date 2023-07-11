namespace FishyFlip.Models;

public class RepostRecord : ATRecord
{
    [JsonConstructor]
    public RepostRecord(Subject? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.FeedType.Like;
    }

    public Subject? Subject { get; }

    public DateTime CreatedAt { get; }
}
