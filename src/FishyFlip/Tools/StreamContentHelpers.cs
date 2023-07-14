// <copyright file="StreamContentHelpers.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

public static class StreamContentHelpers
{
    public static StreamContent FromFilePath(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found.", filePath);
        }

        var stream = File.OpenRead(filePath);
        var content = new StreamContent(stream);
        content.Headers.ContentLength = stream.Length;
        content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(filePath));
        return content;
    }
}
