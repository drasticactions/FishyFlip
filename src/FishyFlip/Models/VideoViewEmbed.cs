// <copyright file="VideoViewEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an embed for an video view.
/// </summary>
public class VideoViewEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoViewEmbed"/> class.
    /// </summary>
    /// <param name="cid">Cid.</param>
    /// <param name="playlist">Playlist.</param>
    /// <param name="thumbnail">Thumbnail.</param>
    /// <param name="aspectRatio">Aspect Ratio.</param>
    [JsonConstructor]
    public VideoViewEmbed(ATCid? cid, string? playlist, string? thumbnail, AspectRatio aspectRatio)
    {
        this.Type = Constants.EmbedTypes.VideoView;
        this.Cid = cid;
        this.Playlist = playlist;
        this.Thumbnail = thumbnail;
        this.AspectRatio = aspectRatio;
    }

    /// <summary>
    /// Gets the cid.
    /// </summary>
    public ATCid? Cid { get; }

    /// <summary>
    /// Gets the playlist.
    /// </summary>
    public string? Playlist { get; }

    /// <summary>
    /// Gets the thumbnail.
    /// </summary>
    public string? Thumbnail { get; }

    /// <summary>
    /// Gets the aspect ratio.
    /// </summary>
    public AspectRatio AspectRatio { get; }
}