// <copyright file="External.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an external resource with a thumbnail, title, description, and URI.
/// </summary>
public class External
{
    /// <summary>
    /// Initializes a new instance of the <see cref="External"/> class.
    /// </summary>
    /// <param name="thumb">The thumbnail image.</param>
    /// <param name="title">The title of the resource.</param>
    /// <param name="description">The description of the resource.</param>
    /// <param name="uri">The URI of the resource.</param>
    [JsonConstructor]
    public External(Image? thumb, string? title, string? description, string? uri)
    {
        this.Thumb = thumb;
        this.Title = title;
        this.Description = description;
        this.Uri = uri;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="External"/> class from a CBOR object.
    /// </summary>
    /// <param name="obj">The CBOR object.</param>
    public External(CBORObject obj)
    {
        if (obj["thumb"] is not null)
        {
            this.Thumb = new Image(obj["thumb"]);
        }

        this.Uri = obj["uri"].AsString();
        this.Title = obj["title"].AsString();
        this.Description = obj["description"].AsString();
    }

    /// <summary>
    /// Gets the thumbnail image.
    /// </summary>
    public Image? Thumb { get; }

    /// <summary>
    /// Gets the title of the resource.
    /// </summary>
    public string? Title { get; }

    /// <summary>
    /// Gets the description of the resource.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the URI of the resource.
    /// </summary>
    public string? Uri { get; }
}