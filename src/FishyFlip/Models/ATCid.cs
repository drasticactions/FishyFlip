// <copyright file="ATCid.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;

namespace FishyFlip.Models;

/// <summary>
/// Content Identifier (CID) implementation for ATProtocol.
/// A CID is a self-describing content-addressed identifier.
/// </summary>
public class ATCid : IEquatable<ATCid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATCid"/> class.
    /// </summary>
    /// <param name="version">CID version.</param>
    /// <param name="codec">Multicodec code.</param>
    /// <param name="hash">Multihash bytes.</param>
    public ATCid(byte version, ulong codec, byte[] hash)
    {
        this.Version = version;
        this.Codec = codec;
        this.Hash = hash ?? throw new ArgumentNullException(nameof(hash));
    }

    /// <summary>
    /// Gets the CID version.
    /// </summary>
    public byte Version { get; }

    /// <summary>
    /// Gets the multicodec code.
    /// </summary>
    public ulong Codec { get; }

    /// <summary>
    /// Gets the multihash bytes.
    /// </summary>
    public byte[] Hash { get; }

    /// <summary>
    /// Decodes a CID from its string representation.
    /// </summary>
    /// <param name="cid">The CID string to decode.</param>
    /// <returns>A new ATCid instance.</returns>
    public static ATCid Decode(string cid)
    {
        if (string.IsNullOrEmpty(cid))
        {
            throw new ArgumentException("CID string cannot be null or empty", nameof(cid));
        }

        try
        {
            // Handle base58btc (starting with 'Qm' for v0) or base32 (starting with 'b' for v1)
            byte[] bytes;
            if (cid.StartsWith("Qm") && cid.Length == 46)
            {
                // CIDv0 - base58btc encoded SHA-256 hash
                bytes = DecodeBase58(cid);
                return new ATCid(0, 0x70, bytes); // 0x70 = dag-pb codec
            }
            else
            {
                // CIDv1 - multibase encoded
                bytes = DecodeMultibase(cid);
                return ReadFromBytes(bytes);
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Invalid CID format: {cid}", nameof(cid), ex);
        }
    }

    /// <summary>
    /// Reads a CID from byte array.
    /// </summary>
    /// <param name="bytes">The bytes to read from.</param>
    /// <returns>A new ATCid instance.</returns>
    public static ATCid Read(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
        {
            throw new ArgumentException("Bytes cannot be null or empty", nameof(bytes));
        }

        return ReadFromBytes(bytes);
    }

    /// <summary>
    /// Reads a CID from a span of bytes.
    /// </summary>
    /// <param name="bytes">The bytes to read from.</param>
    /// <returns>A new ATCid instance.</returns>
    public static ATCid Read(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length == 0)
        {
            throw new ArgumentException("Bytes cannot be empty", nameof(bytes));
        }

        return ReadFromSpan(bytes);
    }

    /// <summary>
    /// Converts the CID to its string representation.
    /// </summary>
    /// <returns>The CID as a string.</returns>
    public override string ToString()
    {
        if (this.Version == 0)
        {
            // CIDv0 uses base58btc encoding
            return EncodeBase58(this.Hash);
        }
        else
        {
            // CIDv1 uses base32 multibase encoding
            var cidBytes = this.ToBytes();
            return "b" + EncodeBase32(cidBytes);
        }
    }

    /// <summary>
    /// Converts the CID to byte array.
    /// </summary>
    /// <returns>The CID as bytes.</returns>
    public byte[] ToBytes()
    {
        var result = new List<byte>();

        if (this.Version == 1)
        {
            result.Add(this.Version);
            result.AddRange(EncodeVarint(this.Codec));
        }

        result.AddRange(this.Hash);
        return result.ToArray();
    }

    /// <inheritdoc/>
    public bool Equals(ATCid? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.Version == other.Version &&
               this.Codec == other.Codec &&
               this.Hash.SequenceEqual(other.Hash);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as ATCid);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hash = default(HashCode);
        hash.Add(this.Version);
        hash.Add(this.Codec);

        foreach (var b in this.Hash)
        {
            hash.Add(b);
        }

        return hash.ToHashCode();
    }

    private static ATCid ReadFromBytes(byte[] bytes)
    {
        return ReadFromSpan(bytes.AsSpan());
    }

    private static ATCid ReadFromSpan(ReadOnlySpan<byte> span)
    {
        var offset = 0;

        // Read version
        var version = span[offset++];

        if (version == 0)
        {
            // CIDv0 - the rest is the hash
            return new ATCid(0, 0x70, span[offset..].ToArray());
        }
        else if (version == 1)
        {
            // CIDv1 - read codec then hash
            var (codec, codecLength) = DecodeVarint(span[offset..]);
            offset += codecLength;

            return new ATCid(version, codec, span[offset..].ToArray());
        }
        else
        {
            throw new NotSupportedException($"CID version {version} is not supported");
        }
    }

    private static byte[] EncodeVarint(ulong value)
    {
        var result = new List<byte>();

        while (value >= 0x80)
        {
            result.Add((byte)(value | 0x80));
            value >>= 7;
        }

        result.Add((byte)value);
        return result.ToArray();
    }

    private static (ulong Value, int Length) DecodeVarint(ReadOnlySpan<byte> bytes)
    {
        ulong result = 0;
        var shift = 0;
        var length = 0;

        foreach (var b in bytes)
        {
            length++;
            result |= (ulong)(b & 0x7F) << shift;

            if ((b & 0x80) == 0)
            {
                break;
            }

            shift += 7;
        }

        return (result, length);
    }

    private static byte[] DecodeBase58(string input)
    {
        // Simplified base58 decoder for CIDv0
        const string alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        var result = new List<byte>();

        // Count leading zeros
        var leadingZeros = 0;
        for (var i = 0; i < input.Length && input[i] == '1'; i++)
        {
            leadingZeros++;
        }

        // Convert from base58
        var num = System.Numerics.BigInteger.Zero;
        var baseValue = new System.Numerics.BigInteger(58);

        for (var i = leadingZeros; i < input.Length; i++)
        {
            var digit = alphabet.IndexOf(input[i]);
            if (digit < 0)
            {
                throw new ArgumentException($"Invalid base58 character: {input[i]}");
            }

            num = (num * baseValue) + digit;
        }

        // Convert to bytes
        var bytes = num.ToByteArray();
        if (bytes[^1] == 0)
        {
            Array.Resize(ref bytes, bytes.Length - 1);
        }

        Array.Reverse(bytes); // BigInteger is little-endian, we want big-endian

        // Add leading zeros
        result.AddRange(new byte[leadingZeros]);
        result.AddRange(bytes);

        return result.ToArray();
    }

    private static string EncodeBase58(byte[] input)
    {
        const string alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        // Count leading zeros
        var leadingZeros = 0;
        for (var i = 0; i < input.Length && input[i] == 0; i++)
        {
            leadingZeros++;
        }

        // Convert to BigInteger (big-endian)
        var bytes = new byte[input.Length + 1];
        Array.Copy(input, 0, bytes, 0, input.Length);
        Array.Reverse(bytes);
        var num = new System.Numerics.BigInteger(bytes);

        // Convert to base58
        var result = new StringBuilder();
        var baseValue = new System.Numerics.BigInteger(58);

        while (num > 0)
        {
            var remainder = (int)(num % baseValue);
            result.Insert(0, alphabet[remainder]);
            num /= baseValue;
        }

        // Add leading ones for leading zeros
        for (var i = 0; i < leadingZeros; i++)
        {
            result.Insert(0, '1');
        }

        return result.ToString();
    }

    private static byte[] DecodeMultibase(string input)
    {
        if (input.Length == 0)
        {
            throw new ArgumentException("Multibase string cannot be empty");
        }

        var prefix = input[0];
        var data = input[1..];

        return prefix switch
        {
            'b' => DecodeBase32(data),
            #if NETSTANDARD
            'f' => DecodeHexString(data),
            #else
            'f' => Convert.FromHexString(data),
            #endif
            'z' => DecodeBase58(data),
            _ => throw new NotSupportedException($"Multibase prefix '{prefix}' is not supported"),
        };
    }

    private static byte[] DecodeBase32(string input)
    {
        // RFC 4648 base32 alphabet
        const string alphabet = "abcdefghijklmnopqrstuvwxyz234567";

        // Remove padding
        input = input.TrimEnd('=');

        var resultFinalLength = input.Length * 5 / 8;
        var result = new byte[resultFinalLength];
        var resultLength = 0;
        var buffer = 0;
        var bufferLength = 0;

        foreach (var c in input.ToLowerInvariant())
        {
            var value = alphabet.IndexOf(c);
            if (value < 0)
            {
                throw new ArgumentException($"Invalid base32 character: {c}");
            }

            buffer = (buffer << 5) | value;
            bufferLength += 5;

            if (bufferLength >= 8)
            {
                result[resultLength++] = (byte)(buffer >> (bufferLength - 8));
                bufferLength -= 8;
            }
        }

        return result;
    }

    private static string EncodeBase32(byte[] input)
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz234567";

        var result = new StringBuilder();
        var buffer = 0;
        var bufferLength = 0;

        foreach (var b in input)
        {
            buffer = (buffer << 8) | b;
            bufferLength += 8;

            while (bufferLength >= 5)
            {
                var index = (buffer >> (bufferLength - 5)) & 0x1F;
                result.Append(alphabet[index]);
                bufferLength -= 5;
            }
        }

        if (bufferLength > 0)
        {
            var index = (buffer << (5 - bufferLength)) & 0x1F;
            result.Append(alphabet[index]);
        }

        return result.ToString();
    }

    private static byte[] DecodeHexString(string hex)
    {
        if (hex.Length % 2 != 0)
        {
            throw new ArgumentException("Hex string must have an even length.");
        }

        var bytes = new byte[hex.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }

        return bytes;
    }
}