// <copyright file="RepoStrongRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class RepoStrongRef
{
    [JsonConstructor]
    public RepoStrongRef(ATUri uri, Cid? cid)
    {
        this.Uri = uri;
        this.Cid = cid;
        this.Type = "com.atproto.repo.strongRef";
    }

    public ATUri Uri { get; }

    public Cid? Cid { get; }

    [JsonPropertyName("$type")]
    public string Type { get; }
}
