// <copyright file="CBORBlock.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Ipfs;

namespace FishyFlip.Tools.Experimental;

/// <summary>
/// Represents a CBOR block with its value, bytes, and CID.
/// </summary>
public class CBORBlock
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CBORBlock"/> class.
    /// </summary>
    /// <param name="value">The CBOR object value.</param>
    /// <param name="bytes">The byte array representation of the CBOR object.</param>
    /// <param name="cid">The CID of the CBOR object.</param>
    public CBORBlock(CBORObject value, byte[] bytes, Cid cid)
    {
        this.Value = value;
        this.Bytes = bytes;
        this.Cid = cid;
    }

    /// <summary>
    /// Gets or sets the CID of the CBOR block.
    /// </summary>
    public ATCid Cid { get; set; }

    /// <summary>
    /// Gets or sets the byte array representation of the CBOR object.
    /// </summary>
    public byte[] Bytes { get; set; }

    /// <summary>
    /// Gets or sets the CBOR object value.
    /// </summary>
    public CBORObject Value { get; set; }

    /// <summary>
    /// Encodes a CBOR object into a <see cref="CborBlock"/>.
    /// </summary>
    /// <param name="obj">The CBOR object to encode.</param>
    /// <returns>The encoded <see cref="CborBlock"/>.</returns>
    public static CBORBlock Encode(CBORObject obj)
    {
        var buffer = obj.EncodeToBytes();
        var hash = Ipfs.MultiHash.ComputeHash(buffer, "sha2-256");
        var cid = new ATCid() { Hash = hash };
        return new CBORBlock(obj, buffer, cid);
    }

    /// <summary>
    /// Encodes an object implementing <see cref="ICBOREncodable{T}"/> into a <see cref="CborBlock"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object to encode.</typeparam>
    /// <param name="obj">The object to encode.</param>
    /// <returns>The encoded <see cref="CborBlock"/>.</returns>
    public static CBORBlock Encode<T>(ICBOREncodable<T> obj)
    {
        return Encode(obj.ToCBORObject());
    }

    /// <summary>
    /// Decodes a byte array into a <see cref="CBORObject"/>.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    /// <returns>The decoded <see cref="CBORObject"/>.</returns>
    public static CBORObject Decode(byte[] bytes)
    {
        return CBORObject.DecodeFromBytes(bytes);
    }
}
