namespace FishyFlip.Models.Cbor;

/// <summary>
/// Frame header.
/// </summary>
public class FrameHeader
{
    /// <summary>
    /// Gets or sets the operation type.
    /// </summary>
    public FrameHeaderOperation Operation { get; set; }

    /// <summary>
    /// Gets or sets the type of header.
    /// </summary>
    public string? Type { get; set; }
}