// <copyright file="DidDocExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Extensions for <see cref="DidDoc"/>.
/// </summary>
public static class DidDocExtensions
{
    /// <summary>
    /// Gets the AT Handles from the DID Document.
    /// </summary>
    /// <param name="didDoc">The DID Document.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>The AT Handles.</returns>
    public static List<ATHandle> GetATHandles(this DidDoc didDoc, ILogger? logger = default)
    {
        var handles = new List<ATHandle>();
        if (didDoc.AlsoKnownAs.Any())
        {
            foreach (var alsoKnownAs in didDoc.AlsoKnownAs)
            {
                if (ATUri.TryCreate(alsoKnownAs, out var uri))
                {
                    if (uri!.Identity is not null && ATHandle.TryCreate(uri!.Identity!, out var handle))
                    {
                        handles.Add(handle!);
                    }
                    else
                    {
                        logger?.LogWarning("Invalid AT Handle: {Handle}", alsoKnownAs);
                    }
                }
            }
        }

        return handles;
    }
}