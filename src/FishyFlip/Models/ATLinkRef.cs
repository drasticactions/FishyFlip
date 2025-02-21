// <copyright file="ATLinkRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools.Json;

namespace FishyFlip.Models;

/// <summary>
/// ATProtocol Link Reference.
/// </summary>
public class ATLinkRef : ICBOREncodable<ATLinkRef>, IJsonEncodable<ATLinkRef>
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
    /// Initializes a new instance of the <see cref="ATLinkRef"/> class.
    /// </summary>
    /// <param name="obj">Object.</param>
    public ATLinkRef(CBORObject obj)
    {
        if (obj is not null)
        {
            this.Link = obj.ToATCid();
        }
    }

    /// <summary>
    /// Gets or sets the link to the image.
    /// </summary>
    [JsonPropertyName("$link")]
    [JsonConverter(typeof(ATCidJsonConverter))]
    public ATCid? Link { get; set; }

    /// <inheritdoc/>
    public static ATLinkRef FromCBORObject(CBORObject obj)
    {
        return new ATLinkRef(obj);
    }

    /// <inheritdoc/>
    public static ATLinkRef FromJson(string obj)
    {
        return JsonSerializer.Deserialize<ATLinkRef>(obj, SourceGenerationContext.Default.ATLinkRef)!;
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(this.ToJson()));
        return CBORObject.ReadJSON(jsonStream);
    }

    /// <inheritdoc/>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this, SourceGenerationContext.Default.ATLinkRef);
    }

    /// <inheritdoc/>
    public byte[] ToUtf8Json()
    {
        return JsonSerializer.SerializeToUtf8Bytes(this, SourceGenerationContext.Default.ATLinkRef);
    }
}