// <copyright file="UploadBlobResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class UploadBlobResponse
{
    [JsonConstructor]
    public UploadBlobResponse(BlobRecord blob)
    {
        this.Blob = blob;
    }

    public BlobRecord Blob { get; }
}
