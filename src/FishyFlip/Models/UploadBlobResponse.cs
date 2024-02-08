// <copyright file="UploadBlobResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the response of an upload blob operation.
/// </summary>
public class UploadBlobResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UploadBlobResponse"/> class.
    /// </summary>
    /// <param name="blob">The uploaded blob record.</param>
    [JsonConstructor]
    public UploadBlobResponse(BlobRecord blob)
    {
        this.Blob = blob;
    }

    /// <summary>
    /// Gets the uploaded blob record.
    /// </summary>
    public BlobRecord Blob { get; }
}
