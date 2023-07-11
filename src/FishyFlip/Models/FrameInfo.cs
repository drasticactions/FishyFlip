namespace FishyFlip.Models;

/// <summary>
/// Frame Info.
/// </summary>
public class FrameInfo
{
    public FrameInfo(CBORObject obj)
    {
        this.Name = obj["name"]?.AsString();
        this.Message = obj["message"]?.AsString();
    }
    
    public string? Name { get; }
    
    public string? Message { get; }
}