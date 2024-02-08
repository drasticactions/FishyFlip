// <copyright file="Error.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an error with a status code and optional error detail.
/// </summary>
public record Error(int StatusCode, ErrorDetail? Detail = default)
{
}