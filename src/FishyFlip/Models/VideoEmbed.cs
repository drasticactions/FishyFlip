// <copyright file="VideoEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Models;

/// <summary>
/// Video Embed.
/// </summary>
public class VideoEmbed : Embed
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoEmbed"/> class.
    /// A video embedded in a Bluesky record (eg, a post).
    /// </summary>
    /// <param name="video">Video.</param>
    /// <param name="captions">Captions.</param>
    /// <param name="alt">Alt text.</param>
    /// <param name="aspectRatio">Aspect Ratio.</param>
    [JsonConstructor]
    public VideoEmbed(BlobRecord video, Caption[]? captions, string? alt, AspectRatio? aspectRatio)
        : base(Constants.EmbedTypes.Video)
    {
        this.Video = video;
        this.Captions = captions;
        this.Alt = alt;
        this.AspectRatio = aspectRatio;
    }

    /// <summary>
    /// Gets the video.
    /// </summary>
    public BlobRecord Video { get; }

    /// <summary>
    /// Gets the captions.
    /// </summary>
    public Caption[]? Captions { get; }

    /// <summary>
    /// Gets the alt text.
    /// </summary>
    public string? Alt { get; }

    /// <summary>
    /// Gets the aspect ratio.
    /// </summary>
    public AspectRatio? AspectRatio { get; }
}