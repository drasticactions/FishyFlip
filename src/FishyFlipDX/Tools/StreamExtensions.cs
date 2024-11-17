// <copyright file="StreamExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Tools;

/// <summary>
/// Stream Extensions.
/// </summary>
public static class StreamExtensions
{
#if NETSTANDARD
    /// <summary>
    /// Asynchronously reads exactly the specified number of bytes from the stream into the buffer.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="buffer">The buffer to store the read bytes.</param>
    /// <param name="offset">The zero-based byte offset in the buffer at which to begin storing the data read from the stream.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task ReadExactlyAsync(this Stream stream, byte[] buffer, int offset, int count)
    {
        int totalRead = 0;
        while (totalRead < count)
        {
            int bytesRead = await stream.ReadAsync(buffer, offset + totalRead, count - totalRead);
            if (bytesRead == 0)
            {
                throw new EndOfStreamException("End of stream reached before fulfilling read request.");
            }

            totalRead += bytesRead;
        }
    }
#endif
}
