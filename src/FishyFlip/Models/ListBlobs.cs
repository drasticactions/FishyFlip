namespace FishyFlip.Models;

/// <summary>
/// List Blobs Response Record.
/// </summary>
/// <param name="Cids">Array of Cids.</param>
/// <param name="Cursor">Cursor.</param>
public record ListBlobs(string[] Cids, string? Cursor);