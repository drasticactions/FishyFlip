// <copyright file="Image.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Image : ATRecord
{
    [JsonConstructor]
    public Image(string? mimeType, int size, string? type)
        : base(type)
    {
        this.MimeType = mimeType;
        this.Size = size;
    }

    public Image(CBORObject image)
    {
        this.Type = image["$type"]?.AsString() ?? string.Empty;
        this.Size = image["size"]?.AsInt32() ?? 0;
        this.MimeType = image["mimeType"]?.AsString() ?? string.Empty;
        var refObj = image["ref"];
        if (refObj is not null)
        {
            this.Ref = new ImageRef(image["ref"]);
        }
    }

    public string? MimeType { get; }

    public int Size { get; }

    [JsonPropertyName("ref")]
    public ImageRef? Ref { get; set; }
}
