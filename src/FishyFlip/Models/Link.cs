// <copyright file="Link.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools;

namespace FishyFlip.Models;

/// <summary>
/// A Link inside a enriched text.
/// </summary>
public class Link : FacetFeature
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Link"/> class.
    /// Ctor.
    /// </summary>
    /// <param name="uri">A Uri <see cref="Uri"/>.</param>
    public Link(string uri)
    {
        this.Uri = uri.StartsWith("http") ? uri : $"https://{uri}";
    }

    /// <summary>
    /// Gets the resolved Uri of the text
    /// If the value doesnt start with http it will have appended https.
    /// </summary>
    public string Uri { get; }

    public override string Type => Constants.FacetTypes.Link;
}