// <copyright file="ATError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an atError with a status code and optional atError detail.
/// </summary>
public record ATError(int StatusCode, ErrorDetail? Detail = default)
{
}