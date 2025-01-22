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
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownATObject"/> class.
    /// </summary>
    /// <param name="json">Json.</param>
    /// <returns>UnknownATObject.</returns>
    public static UnknownATObject FromJson(string json)
    {
        return JsonSerializer.Deserialize<UnknownATObject>(json, (JsonTypeInfo<UnknownATObject>)SourceGenerationContext.Default.UnknownATObject)!;
    }

    /// <inheritdoc/>
    public override string ToJson()
    {
        return JsonSerializer.Serialize<UnknownATObject>(this, (JsonTypeInfo<UnknownATObject>)SourceGenerationContext.Default.UnknownATObject)!;
    }
}
