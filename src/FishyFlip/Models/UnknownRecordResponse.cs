// <copyright file="UnknownRecordResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Models;

/// <summary>
/// Represents a response for an unknown record.
/// </summary>
public class UnknownRecordResponse : ATFeedTypeAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownRecordResponse"/> class.
    /// </summary>
    /// <param name="uri">The URI of the unknown record.</param>
    /// <param name="cid">The CID of the unknown record.</param>
    /// <param name="value">The value of the unknown record.</param>
    public UnknownRecordResponse(ATUri? uri, ATCid? cid, ATRecord? value)
        : base(uri, cid)
    {
        this.Value = value;
    }

    /// <summary>
    /// Gets the value of the unknown record.
    /// </summary>
    public ATRecord? Value { get; }
}