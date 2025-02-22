// <copyright file="FrameFooter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the footer of a frame in the FishyFlip application.
/// </summary>
public class FrameFooter : ICBOREncodable<FrameFooter>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameFooter"/> class.
    /// </summary>
    /// <param name="obj">The CBOR object containing the frame footer data.</param>
    /// <param name="logger">The optional logger to use for logging.</param>
    public FrameFooter(CBORObject obj, ILogger? logger = default)
    {
        this.Did = ATDid.Create(obj["did"].AsString());
        this.Version = obj["version"].AsInt32();
        this.Prev = obj["prev"].ToATCid(logger);
        this.Data = obj["data"].ToATCid(logger);
        this.Sig = obj["sig"].EncodeToBytes();
    }

    /// <summary>
    /// Gets the ATDid associated with the frame footer.
    /// </summary>
    public ATDid? Did { get; }

    /// <summary>
    /// Gets the version of the frame footer.
    /// </summary>
    public int Version { get; }

    /// <summary>
    /// Gets the previous CID of the frame footer.
    /// </summary>
    public ATCid? Prev { get; }

    /// <summary>
    /// Gets the data CID of the frame footer.
    /// </summary>
    public ATCid? Data { get; }

    /// <summary>
    /// Gets the signature of the frame footer.
    /// </summary>
    public byte[] Sig { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="FrameFooter"/> class from a CBOR object.
    /// </summary>
    /// <param name="blockObj">The CBOR object representing the frame footer.</param>
    /// <param name="logger">The optional logger to use for logging.</param>
    /// <returns>A new instance of the <see cref="FrameFooter"/> class, or null if the CBOR object does not contain a signature.</returns>
    public static FrameFooter? FromCBORObject(CBORObject blockObj, ILogger? logger = default)
    {
        if (blockObj["sig"] is not null)
        {
            return new FrameFooter(blockObj, logger);
        }

        return null;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="FrameFooter"/> class from a CBORObject.
    /// </summary>
    /// <param name="obj">The CBORObject to convert into a FrameFooter instance.</param>
    /// <returns>A new instance of the <see cref="FrameFooter"/> class.</returns>
    public static FrameFooter FromCBORObject(CBORObject obj)
    {
        return new FrameFooter(obj);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject()
    {
        throw new NotImplementedException();
    }
}
