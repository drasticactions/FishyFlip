namespace FishyFlip.Models;

/// <summary>
/// Frame Tombstone.
/// </summary>
public class FrameTombstone
{
    public FrameTombstone(CBORObject obj)
    {
        this.Did = obj["did"] is not null ? ATDid.Create(obj["did"].AsString()) : null;
        this.Seq = obj["seq"].AsInt32();
        this.Time = obj["time"] is not null ? obj["time"].ToDateTime() : null;
    }
    
    public ATDid? Did { get; }

    public int Seq { get; }

    public DateTime? Time { get; }
}