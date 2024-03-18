// <copyright file="ATLinkRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// ATProtocol Link Reference.
/// </summary>
public class ATLinkRef
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATLinkRef"/> class with the specified link.
    /// </summary>
    /// <param name="link">The link to the file.</param>
    [JsonConstructor]
    public ATLinkRef(ATCid? link)
    {
        this.Link = link;
    }

    /// <summary>
    /// Gets or sets the link to the image.
    /// </summary>
    [JsonPropertyName("$link")]
    public ATCid? Link { get; set; }
}