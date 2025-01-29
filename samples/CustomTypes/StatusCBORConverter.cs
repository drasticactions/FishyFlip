// <copyright file="StatusCBORConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon;
using FishyFlip.Tools;
using PeterO.Cbor;

namespace CustomTypes;

/// <summary>
/// Status CBOR Converter.
/// </summary>
public class StatusCBORConverter : ICustomATObjectCBORConverter
{
    /// <inheritdoc/>
    public IReadOnlyList<string> SupportedTypes { get; } = new List<string> { Status.RecordType };

    /// <inheritdoc/>
    public ATObject? Read(CBORObject obj, string type)
    {
        return new CustomTypes.Status(obj);
    }
}