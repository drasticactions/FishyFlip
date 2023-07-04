// <copyright file="Error.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record Error(int StatusCode, ErrorDetail? Detail = default)
{
}