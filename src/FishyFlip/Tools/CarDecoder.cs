// <copyright file="CarDecoder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using static FishyFlip.Tools.CarDecoder;

namespace FishyFlip.Tools;

/// <summary>
/// Decode CAR byte arrays.
/// </summary>
public static class CarDecoder
{
    private const int CidV1BytesLength = 36;
    private const int BufferSize = 32768;

    /// <summary>
    /// Decodes CAR Byte Array.
    /// </summary>
    /// <param name="bytes">Byte Array.</param>
    internal static void DecodeCar(byte[] bytes, OnCarDecoded? progress = null)
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

            var cidBytes = bytes[start..(start + CidV1BytesLength)];
            var cid = Cid.Read(cidBytes);

            start += CidV1BytesLength;
            var bs = bytes[start..(start + body.Value - CidV1BytesLength)];
            start += body.Value - CidV1BytesLength;
            progress?.Invoke(new CarProgressStatusEvent(cid, bs));
        }
    }

    internal static async Task DecodeCarAsync(Stream stream, OnCarDecoded? progress = null)
    {
        var totalBytesRead = 0;
        var header = DecodeReader(stream);
        totalBytesRead += header.Length + header.Value;
        int start = header.Length + header.Value;
        //System.Diagnostics.Debug.WriteLine($"Header: Value: {header.Value} - Length: {header.Length} - Start: {start}");

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
            //System.Diagnostics.Debug.WriteLine($"body: Value: {body.Value} - Length: {body.Length} - Start: {start}");

            byte[] cidBuffer = new byte[CidV1BytesLength];
            await stream.ReadExactlyAsync(cidBuffer, 0, CidV1BytesLength);
            var cid = Cid.Read(cidBuffer);
            totalBytesRead += CidV1BytesLength;
            //System.Diagnostics.Debug.WriteLine($"cidBytes: {cidBuffer.Length}  - total: {totalBytesRead}");

            byte[] bodyBuffer = new byte[body.Value - CidV1BytesLength];
            await stream.ReadExactlyAsync(bodyBuffer, 0, body.Value - CidV1BytesLength);
            totalBytesRead += bodyBuffer.Length;
            //System.Diagnostics.Debug.WriteLine($"bs: {bodyBuffer.Length}  - total: {totalBytesRead}");
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

        return new DecodedBlock(Decode(a), a.Count);
    }

    private static DecodedBlock DecodeReader(byte[] bytes)
    {
        var a = new List<byte>();

        int i = 0;
        while (true)
        {
            byte b = bytes[i];

            i++;
            a.Add(b);
            if ((b & 0x80) == 0)
            {
                break;
            }
        }

        return new DecodedBlock(Decode(a), a.Count);
    }

    private static int Decode(List<byte> b)
    {
        int r = 0;
        for (int i = 0; i < b.Count; i++)
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

public delegate void OnCarDecoded(CarProgressStatusEvent e);

public class CarProgressStatusEvent
{
    public CarProgressStatusEvent(Cid cid, byte[] bytes)
    {
        this.Cid = cid;
        this.Bytes = bytes;
    }

    public byte[] Bytes { get; }

    public Cid Cid { get; }
}