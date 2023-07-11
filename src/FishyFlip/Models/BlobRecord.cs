// <copyright file="BlobRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class BlobRecord : ATRecord
{
    [JsonConstructor]
    public BlobRecord(string? mimeType, int size, string? type)
        : base(type)
    {
        this.MimeType = mimeType;
        this.Size = size;
    }

    public string? MimeType { get; }

    public int Size { get;  }

    [JsonPropertyName("ref")]
    public Cid? Ref { get; set; }
}
