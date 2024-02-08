// <copyright file="RepoStrongRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a strong reference to a repository.
/// </summary>
public class RepoStrongRef
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RepoStrongRef"/> class.
    /// </summary>
    /// <param name="uri">The URI of the repository.</param>
    /// <param name="cid">The CID (Content Identifier) of the repository.</param>
    [JsonConstructor]
    public RepoStrongRef(ATUri uri, Cid? cid)
    {
        this.Uri = uri;
        this.Cid = cid;
        this.Type = "com.atproto.repo.strongRef";
    }

    /// <summary>
    /// Gets the URI of the repository.
    /// </summary>
    public ATUri Uri { get; }

    /// <summary>
    /// Gets the CID (Content Identifier) of the repository.
    /// </summary>
    public Cid? Cid { get; }

    /// <summary>
    /// Gets the type of the repository strong reference.
    /// </summary>
    [JsonPropertyName("$type")]
    public string Type { get; }
}
