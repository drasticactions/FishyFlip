// <copyright file="CarDecoder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace FishyFlip.Tools;

/// <summary>
/// Decode CAR byte arrays.
/// </summary>
public static class CarDecoder
{
    private const int CidV1BytesLength = 36;

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
            start += body.Length;

            var cidBytes = bytes[start..(start + CidV1BytesLength)];
            var cid = Cid.Read(cidBytes);

            start += CidV1BytesLength;
            var bs = bytes[start..(start + body.Value - CidV1BytesLength)];
            start += body.Value - CidV1BytesLength;
            progress?.Invoke(new CarProgressStatusEvent(cid, bs));
        }
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
}