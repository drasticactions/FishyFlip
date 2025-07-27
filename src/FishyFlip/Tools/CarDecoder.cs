// <copyright file="CarDecoder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Buffers;
using System.Runtime.InteropServices;

namespace FishyFlip.Tools;

/// <summary>
/// Fires when a car file is decoded.
/// </summary>
/// <param name="e">Car Progress Status Event.</param>
public delegate void OnCarDecoded(CarProgressStatusEvent e);

/// <summary>
/// Decode CAR byte arrays.
/// </summary>
public static class CarDecoder
{
    private const int ATCidV1BytesLength = 36;
    private static readonly ArrayPool<byte> ArrayPool = ArrayPool<byte>.Shared;

    /// <summary>
    /// Decodes CAR ReadOnlySpan.
    /// </summary>
    /// <param name="bytes">Bytes to decode.</param>
    /// <returns>IEnumberable of <see cref="FrameEvent"/>.</returns>
    public static IEnumerable<FrameEvent> DecodeCar(byte[] bytes)
    {
        int bytesLength = bytes.Length;
        var header = DecodeReader(bytes);

        int start = header.Length + header.Value;

        while (start < bytesLength)
        {
#if NET
            var body = DecodeReader(bytes[start..]);
#else
            var body = DecodeReader(new ArraySegment<byte>(bytes, start, bytes.Length - start).ToArray());
#endif
            if (body.Value == 0)
            {
                break;
            }

            start += body.Length;

#if NET
            var cidBytes = bytes[start..(start + ATCidV1BytesLength)];
#else
            var cidBytes = new ArraySegment<byte>(bytes, start, ATCidV1BytesLength).ToArray();
#endif

            var cid = ATCid.Read(cidBytes);

            start += ATCidV1BytesLength;
#if NET
            var bs = bytes[start..(start + body.Value - ATCidV1BytesLength)];
#else
            var bs = new ArraySegment<byte>(bytes, start, body.Value - ATCidV1BytesLength).ToArray();
#endif
            start += body.Value - ATCidV1BytesLength;
#if NET
            yield return new FrameEvent(cid, bs.ToArray());
#else
            yield return new FrameEvent(cid, bs);
#endif
        }
    }

    /// <summary>
    /// Decodes CAR Stream.
    /// </summary>
    /// <param name="stream">Stream containing CAR file.</param>
    /// <param name="progress">Fires when a car file is decoded.</param>
    /// <returns>Task.</returns>
    [Obsolete("Use DecodeCarAsync(Stream) instead.")]
    public static async Task DecodeCarAsync(Stream stream, OnCarDecoded? progress = null)
    {
        var items = DecodeCarAsync(stream);
        await foreach (var item in items)
        {
            progress?.Invoke(new CarProgressStatusEvent(item.Cid, item.Bytes));
        }
    }

    /// <summary>
    /// Decodes Repo and returns ATObjects contained within it.
    /// Skips over FrameNode and FrameEntry items.
    /// </summary>
    /// <param name="bytes">CAR Span.</param>
    /// <returns>IEnumerable of <see cref="ATObject"/>.</returns>
    public static IEnumerable<ATObject> DecodeRepo(byte[] bytes)
    {
        var items = DecodeCar(bytes);
        foreach (var e in items)
        {
            using var blockStream = new MemoryStream(e.Bytes);
            var blockObj = CBORObject.Read(blockStream);
            if (blockObj.IsATObject())
            {
                yield return blockObj.ToATObject();
            }
        }
    }

    /// <summary>
    /// Decodes Repo and returns ATObjects contained within it.
    /// Skips over FrameNode and FrameEntry items.
    /// </summary>
    /// <param name="stream">CAR Stream.</param>
    /// <returns>IAsyncEnumerable of <see cref="ATObject"/>.</returns>
    public static async IAsyncEnumerable<ATObject> DecodeRepoAsync(Stream stream)
    {
        var items = DecodeCarAsync(stream);
        await foreach (var e in items)
        {
            using var blockStream = new MemoryStream(e.Bytes);
            var blockObj = CBORObject.Read(blockStream);
            if (blockObj.IsATObject())
            {
                yield return blockObj.ToATObject();
            }
        }
    }

    /// <summary>
    /// Decodes CAR Stream.
    /// </summary>
    /// <param name="stream">Stream containing CAR file.</param>
    /// <returns>IAsyncEnumberable of <see cref="FrameEvent"/>.</returns>
    public static async IAsyncEnumerable<FrameEvent> DecodeCarAsync(Stream stream)
    {
        var header = DecodeReader(stream);

        if (header.Value == -1)
        {
            yield break;
        }

        int start = header.Length + header.Value;

        if (start > 1)
        {
            await ScanStream(stream, start - 1);
        }

        var cidBuffer = ArrayPool.Rent(ATCidV1BytesLength);
        try
        {
            while (true)
            {
                var body = DecodeReader(stream);
                if (body.Value == -1)
                {
                    break;
                }

                await stream.ReadExactlyAsync(cidBuffer, 0, ATCidV1BytesLength);
                var cid = ATCid.Read(cidBuffer.AsSpan(0, ATCidV1BytesLength));

                int bodySize = body.Value - ATCidV1BytesLength;
                var bodyBuffer = ArrayPool.Rent(bodySize);
                try
                {
                    await stream.ReadExactlyAsync(bodyBuffer, 0, bodySize);
                    var bodyBytes = new byte[bodySize];
                    Array.Copy(bodyBuffer, bodyBytes, bodySize);
                    yield return new FrameEvent(cid, bodyBytes);
                }
                finally
                {
                    ArrayPool.Return(bodyBuffer);
                }
            }
        }
        finally
        {
            ArrayPool.Return(cidBuffer);
        }
    }

    private static async Task ScanStream(Stream stream, int length)
    {
        var receiveBuffer = ArrayPool.Rent(length);
        try
        {
            await stream.ReadExactlyAsync(receiveBuffer, 0, length);
        }
        finally
        {
            ArrayPool.Return(receiveBuffer);
        }
    }

    private static DecodedBlock DecodeReader(Stream stream)
    {
        Span<byte> buffer = stackalloc byte[16]; // Most varint encodings are < 16 bytes
        int count = 0;

        while (true)
        {
            int b = stream.ReadByte();
            if (b == -1)
            {
                return new DecodedBlock(-1, -1);
            }

            if (count >= buffer.Length)
            {
                // Fall back to List for very long encodings (rare)
                return DecodeReaderFallback(stream, buffer, count, (byte)b);
            }

            buffer[count++] = (byte)b;
            if ((b & 0x80) == 0)
            {
                return new DecodedBlock(Decode(buffer[..count]), count);
            }
        }
    }

    private static DecodedBlock DecodeReaderFallback(Stream stream, ReadOnlySpan<byte> initialBuffer, int initialCount, byte currentByte)
    {
        var a = new List<byte>(initialBuffer[..initialCount].ToArray()) { currentByte };

        while (true)
        {
            int b = stream.ReadByte();
            if (b == -1)
            {
                return new DecodedBlock(-1, -1);
            }

            a.Add((byte)b);
            if ((b & 0x80) == 0)
            {
                break;
            }
        }

#if NET5_OR_GREATER
        return new DecodedBlock(Decode(CollectionsMarshal.AsSpan(a)), a.Count);
#else
        return new DecodedBlock(Decode(a.ToArray()), a.Count);
#endif
    }

    private static DecodedBlock DecodeReader(ReadOnlySpan<byte> bytes)
    {
        for (int i = 0; i < bytes.Length; i++)
        {
            byte b = bytes[i];
            if ((b & 0x80) == 0)
            {
                var a = bytes.Slice(0, i + 1);
                return new DecodedBlock(Decode(a), a.Length);
            }
        }

        throw new InvalidDataException("Incomplete block.");
    }

    private static int Decode(ReadOnlySpan<byte> b)
    {
        int r = 0;
        for (int i = 0; i < b.Length; i++)
        {
            int e = b[i];
            r = r + ((e & 0x7F) << (i * 7));
        }

        return r;
    }

    private class DecodedBlock
    {
        public DecodedBlock(int value, int length)
        {
            this.Value = value;
            this.Length = length;
        }

        public int Value { get; }

        public int Length { get; }
    }
}
