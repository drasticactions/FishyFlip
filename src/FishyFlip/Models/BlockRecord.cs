namespace FishyFlip.Models;

public class BlockRecord : ATRecord
{
    [JsonConstructor]
    public BlockRecord(ATDid? subject, DateTime? createdAt)
    {
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.Type = Constants.GraphTypes.Block;
    }

    public ATDid? Subject { get; }

    public DateTime CreatedAt { get; }
}