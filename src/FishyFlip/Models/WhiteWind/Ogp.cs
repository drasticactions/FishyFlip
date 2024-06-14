// <copyright file="Ogp.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models.WhiteWind;

/// <summary>
/// Represents the Open Graph protocol (OGP) properties of a webpage.
/// </summary>
/// <param name="Url">The URL of the image that appears when someone shares the content to Facebook.</param>
/// <param name="Width">The number of pixels wide of the image.</param>
/// <param name="Height">The number of pixels high of the image.</param>
public record Ogp(Uri? Url, int? Width = default, int? Height = default);