namespace FishyFlip.Models;

/// <summary>
/// Frame Repo Op.
/// </summary>
public class FrameRepoOp
{
    public FrameRepoOp(CBORObject obj)
    {
        this.Cid = obj["cid"] is not null ? Cid.Decode(obj["cid"].AsString()) : null;
        this.Path = obj["path"]?.AsString();
        this.Action = obj["action"]?.AsString();
    }
    
    public Cid? Cid { get; }
    
    public string? Path { get; }
    
    public string? Action { get; }
}