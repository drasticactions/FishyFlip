using System.Text.Json.Serialization;

namespace FishyFlip.Models;

public class Embed
{
    [JsonPropertyName("$type")]
    public string Type { get; set; }
    
    public EmbedImages[] Images { get; set; } = Array.Empty<EmbedImages>();
    
    public EmbedView External { get; set; }
}

public class EmbedView
{
    public string Description { get; set; }
    
    public string Thumb { get; set; }
    
    public string Title { get; set; }
    
    public Uri Uri { get; set; }
}

public class EmbedImages
{
    public string Alt { get; set; }
    
    public string Thumb { get; set; }
    
    public string FullSize { get; set; }
}