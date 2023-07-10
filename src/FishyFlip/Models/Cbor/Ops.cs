namespace FishyFlip.Models.Cbor;

/// <summary>
/// BlueSky Sync Operations.
/// </summary>
public class Ops
{
    /// <summary>
    /// Gets or sets the CID.
    /// </summary>
    public Cid? Cid { get; set; }

    /// <summary>
    /// Gets or sets the Path.
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Gets or sets the given action.
    /// </summary>
    public string? Action { get; set; }
}