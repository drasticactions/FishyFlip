// <copyright file="UnknownATObject.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Models;

/// <summary>
///  An unknown ATObject.
/// </summary>
public class UnknownATObject : ATObject
{
    /// <summary>
    /// The ATRecord Type.
    /// </summary>
    public const string RecordType = "#unknownATObject";

    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownATObject"/> class.
    /// </summary>
    public UnknownATObject()
    {
        this.Type = "#unknown";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownATObject"/> class.
    /// </summary>
    /// <param name="obj">CBOR object.</param>
    public UnknownATObject(CBORObject obj)
    {
        this.Type = obj["$type"]?.AsString() ?? "#unknown";
        this.CBORObject = obj;
    }

    /// <summary>
    /// Gets or sets the Raw Json from the original object.
    /// </summary>
    [JsonIgnore]
    public string? Json { get; set; }

    /// <summary>
    /// Gets or sets the Raw CBOR from the original object.
    /// </summary>
    [JsonIgnore]
    public CBORObject? CBORObject { get; set; }

    /// <inheritdoc/>
    public override string ToJson()
    {
        return this.Json ?? this.CBORObject?.ToJSONString() ?? string.Empty;
    }
}
