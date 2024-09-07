// <copyright file="AspectRatio.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Models;

/// <summary>
/// Aspect Ratio.
/// </summary>
/// <param name="Width">Width.</param>
/// <param name="Height">Height.</param>
public record AspectRatio(int Width, int Height);