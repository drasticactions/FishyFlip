// <copyright file="CarDecoder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Decode CAR byte arrays.
/// </summary>
internal static class CarDecoder
{
    private const int CidV1BytesLength = 36;

    /// <summary>
    /// Decodes CAR Byte Array.
    /// </summary>
    /// <param name="bytes">Byte Array.</param>
    /// <returns>Dictionary of CID and byte array.</returns>
    public static Dictionary<Cid, byte[]> DecodeCar(byte[] bytes)
    {
        var blocks = new Dictionary<Cid, byte[]>();

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
            blocks[cid] = bytes[start..(start + body.Value - CidV1BytesLength)];
            start += body.Value - CidV1BytesLength;
        }

        return blocks;
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