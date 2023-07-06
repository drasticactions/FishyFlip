// <copyright file="BlobRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public record UploadBlobResponse(BlobRecord blob);

public class BlobRecord
{
    public string MimeType { get; set; }

    public int Size { get; set; }

    public BlobRef Ref { get; set; }

    [JsonPropertyName("$type")]
    public string Type { get; set; }
}

public class BlobRef
{
    [JsonPropertyName("$link")]
    public string Link { get; set; }
}
