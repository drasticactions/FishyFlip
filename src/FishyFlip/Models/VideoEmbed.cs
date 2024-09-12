// <copyright file="VideoEmbed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <param name="alt">Alt text.</param>
    /// <param name="aspectRatio">Aspect Ratio.</param>
    /// <param name="captions">Captions.</param>
    [JsonConstructor]
    public VideoEmbed(Video video, string? alt = default, AspectRatio? aspectRatio = default, Caption[]? captions = default)
        : base(Constants.EmbedTypes.Video)
    {
        this.Video = video;
        this.Captions = captions;
        this.Alt = alt;
        this.AspectRatio = aspectRatio;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoEmbed"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject representing the external content.</param>
    public VideoEmbed(CBORObject obj)
    {
        this.Type = Constants.EmbedTypes.Video;
        this.Alt = obj["alt"]?.AsString() ?? string.Empty;
        var video = obj["video"];
        this.Video = video is not null ? new Video(video) : null;

        var captions = obj["captions"];
        if (captions is not null)
        {
            this.Captions = captions.Values.Select(caption => new Caption(caption["lang"].AsString(), new CaptionBlob(caption["file"]))).ToArray();
        }

        var aspectRatio = obj["aspectRatio"];
        this.AspectRatio = aspectRatio is not null ? new AspectRatio(aspectRatio["width"].AsInt32(), aspectRatio["height"].AsInt32()) : null;
    }

    /// <summary>
    /// Gets the video.
    /// </summary>
    public Video? Video { get; }

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