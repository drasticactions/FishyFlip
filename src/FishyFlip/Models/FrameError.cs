// <copyright file="FrameError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame atError.
/// </summary>
public class FrameError : ICBOREncodable<FrameError>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameError"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the atError information.</param>
    public FrameError(CBORObject obj)
    {
        this.Error = obj["error"].AsString();
        this.Message = obj["message"].AsString();
    }

    /// <summary>
    /// Gets the atError.
    /// </summary>
    public string? Error { get; }

    /// <summary>
    /// Gets the atError.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="FrameError"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject to convert into a FrameError instance.</param>
    /// <returns>A new instance of the <see cref="FrameError"/> class.</returns>
    public static FrameError FromCBORObject(CBORObject obj)
    {
        return new FrameError(obj);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}