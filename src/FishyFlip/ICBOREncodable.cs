// <copyright file="ICBOREncodable.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Interface for CBOR encodable objects.
/// </summary>
/// <typeparam name="T">Object.</typeparam>
public interface ICBOREncodable<out T>
{
    /// <summary>
    /// Converts the object to a CBOR object.
    /// </summary>
    /// <returns><see cref="CBORObject"/>.</returns>
    CBORObject ToCBORObject();

#if NET
    /// <summary>
    /// Converts a CBOR object to the specified type.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <returns>From CBOR Object.</returns>
    static abstract T FromCBORObject(CBORObject obj);
#endif
}
