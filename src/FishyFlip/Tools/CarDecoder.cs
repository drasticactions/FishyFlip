// <copyright file="CarDecoder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    private const int BufferSize = 32768;

    /// <summary>
    /// Decodes CAR Byte Array.
    /// </summary>
    /// <param name="bytes">Byte Array.</param>
    /// <param name="progress">Fires when a car file is decoded.</param>
    public static void DecodeCar(byte[] bytes, OnCarDecoded? progress = null)
    {
        DecodeCar(bytes.AsSpan(), progress);
    }

    /// <summary>
    /// Decodes CAR ReadOnlySpan.
    /// </summary>
    /// <param name="bytes">Bytes to decode.</param>
    /// <param name="progress">Fires when a car file is decoded.</param>
    public static void DecodeCar(ReadOnlySpan<byte> bytes, OnCarDecoded? progress = null)
    {
        int bytesLength = bytes.Length;
        var header = DecodeReader(bytes);

        int start = header.Length + header.Value;

        while (start < bytesLength)
        {
            var body = DecodeReader(bytes[start..]);
            if (body.Value == 0)
            {
                break;
            }

            start += body.Length;

            var cidBytes = bytes[start..(start + ATCidV1BytesLength)];
            var cid = Cid.Read(cidBytes.ToArray());

            start += ATCidV1BytesLength;
            var bs = bytes[start..(start + body.Value - ATCidV1BytesLength)];
            start += body.Value - ATCidV1BytesLength;
            progress?.Invoke(new CarProgressStatusEvent(cid, bs.ToArray()));
        }
    }

    /// <summary>
    /// Decodes CAR Stream.
    /// </summary>
    /// <param name="stream">Stream containing CAR file.</param>
    /// <param name="progress">Fires when a car file is decoded.</param>
    /// <returns>Task.</returns>
    public static async Task DecodeCarAsync(Stream stream, OnCarDecoded? progress = null)
    {
        var totalBytesRead = 0;
        var header = DecodeReader(stream);
        totalBytesRead += header.Length + header.Value;
        int start = header.Length + header.Value;

        // System.Diagnostics.Debug.WriteLine($"Header: Value: {header.Value} - Length: {header.Length} - Start: {start}");
        await ScanStream(stream, start - 1);
        byte[] receiveBuffer = new byte[BufferSize];

        while (true)
        {
            var body = DecodeReader(stream);
            if (body.Value == -1)
            {
                break;
            }

            totalBytesRead += body.Length;
            start += body.Length;

            // System.Diagnostics.Debug.WriteLine($"body: Value: {body.Value} - Length: {body.Length} - Start: {start}");
            byte[] cidBuffer = new byte[ATCidV1BytesLength];
            await stream.ReadExactlyAsync(cidBuffer, 0, ATCidV1BytesLength);
            var cid = Cid.Read(cidBuffer);
            totalBytesRead += ATCidV1BytesLength;

            // System.Diagnostics.Debug.WriteLine($"cidBytes: {cidBuffer.Length}  - total: {totalBytesRead}");
            byte[] bodyBuffer = new byte[body.Value - ATCidV1BytesLength];
            await stream.ReadExactlyAsync(bodyBuffer, 0, body.Value - ATCidV1BytesLength);
            totalBytesRead += bodyBuffer.Length;

            // System.Diagnostics.Debug.WriteLine($"bs: {bodyBuffer.Length}  - total: {totalBytesRead}");
            progress?.Invoke(new CarProgressStatusEvent(cid, bodyBuffer));
        }
    }

    private static async Task ScanStream(Stream stream, int length)
    {
        byte[] receiveBuffer = new byte[length];
        await stream.ReadExactlyAsync(receiveBuffer, 0, length);
    }

    private static DecodedBlock DecodeReader(Stream stream)
    {
        var a = new List<byte>();

        int i = 0;
        while (true)
        {
            int b = stream.ReadByte();
            if (b == -1)
            {
                return new DecodedBlock(-1, -1);
            }

            i++;
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
