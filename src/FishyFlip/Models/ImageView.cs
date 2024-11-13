// <copyright file="ImageView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an image view with its thumbnail, fullsize, and alt text.
/// </summary>
public record ImageView(string Thumb, string Fullsize, string Alt, AspectRatio? AspectRatio);
