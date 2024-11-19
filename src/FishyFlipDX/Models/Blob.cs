// <copyright file="Blob.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.Com.Atproto.Repo;
using Google.Protobuf.WellKnownTypes;

namespace FishyFlip.Models;

/// <summary>
/// Represents a blob record.
/// </summary>
public class Blob : ATObject
{
    /// <summary>
    /// Gets the ATRecord Type.
    /// </summary>
    public const string RecordType = "blob";

    /// <summary>
    /// Initializes a new instance of the <see cref="Blob"/> class.
    /// </summary>
    public Blob()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Blob"/> class.
    /// </summary>
    /// <param name="obj">Obj.</param>
    public Blob(CBORObject obj)
    {
        this.MimeType = obj["mimeType"].AsString();
        this.Size = obj["size"].AsInt32();
        this.Ref = new ATLinkRef(obj["ref"]);
    }

    /// <summary>
    /// Gets or sets the MIME type of the blob.
    /// </summary>
    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }

    /// <summary>
    /// Gets or sets the size of the blob in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets the reference to the blob.
    /// </summary>
    [JsonPropertyName("ref")]
    public ATLinkRef? Ref { get; set; }

    /// <summary>
    /// Gets the ATRecord Type.
    /// </summary>
    [JsonPropertyName("$type")]
    public override string Type => "blob";

    /// <summary>
    /// Initializes a new instance of the <see cref="Blob"/> class.
    /// </summary>
    /// <param name="json">Json.</param>
    /// <returns>Blob.</returns>
    public static Blob FromJson(string json)
    {
        return JsonSerializer.Deserialize<Blob>(json, (JsonTypeInfo<Blob>)SourceGenerationContext.Default.Blob)!;
    }

    /// <inheritdoc/>
    public override string ToJson()
    {
        return JsonSerializer.Serialize<Blob>(this, (JsonTypeInfo<Blob>)SourceGenerationContext.Default.Blob)!;
    }
}