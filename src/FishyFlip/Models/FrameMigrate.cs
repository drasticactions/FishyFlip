namespace FishyFlip.Models;

/// <summary>
/// Frame Migrate.
/// </summary>
public class FrameMigrate
{
    public FrameMigrate(CBORObject obj)
    {
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Seq = obj["seq"].AsInt32();
        this.MigrateTo = obj["migrateTo"]?.AsString();
        this.Time = obj["time"] is not null ? obj["time"].ToDateTime() : null;
    }
    
    public ATDid? Did { get; }

    public int Seq { get; }

    public string? MigrateTo { get; }

    public DateTime? Time { get; }
}