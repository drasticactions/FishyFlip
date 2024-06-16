// <copyright file="BlobMetadata.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace WhiteWindLib.Models.WhiteWind;

/// <summary>
/// The blob metadata for a given post.
/// </summary>
/// <param name="Blobref">The reference to the blob.</param>
/// <param name="Name">The name of the blob item.</param>
public record BlobMetadata(BlobRecord Blobref, string Name);