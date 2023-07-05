using System.Text.Json.Serialization;

namespace FishyFlip.Models;

public record ThreadPostViewFeed(ThreadView thread);

public class ThreadView
{
    public PostView Post { get; set; }
    
    public ThreadView[]? Replies { get; set; }
    
    [JsonPropertyName("$type")]
    public string Type { get; set; }
}