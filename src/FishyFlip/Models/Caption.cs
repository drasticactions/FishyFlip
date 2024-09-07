// <copyright file="Caption.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Models;

/// <summary>
/// Captions for video.
/// </summary>
/// <param name="Lang">Language.</param>
/// <param name="File">VTT Subtitle.</param>
public record Caption(string Lang, Blob File);