// <copyright file="CreatePostResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class CreatePostResponse
{
    [JsonConstructor]
    public CreatePostResponse(ATUri? uri, Cid? cid)
    {
        this.Cid = cid;
        this.Uri = uri;
    }

    public Cid? Cid { get; }

    public ATUri? Uri { get; }
}