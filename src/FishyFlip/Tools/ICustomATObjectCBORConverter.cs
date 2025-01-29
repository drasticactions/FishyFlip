// <copyright file="ICustomATObjectCBORConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Custom AT Object CBOR Converter.
/// </summary>
public interface ICustomATObjectCBORConverter
{
    /// <summary>
    /// Gets the list of supported types by the converter.
    /// </summary>
    IReadOnlyList<string> SupportedTypes { get; }

    /// <summary>
    /// Reads and converts the CBOR into an <see cref="ATObject"/> object.
    /// </summary>
    /// <param name="obj">The CBOR text.</param>
    /// <param name="type">The type of ATObject to deserialize.</param>
    /// <returns>An <see cref="ATObject"/> object or null if the conversion is not possible.</returns>
    ATObject? Read(CBORObject obj, string type);
}